using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionDemo
{
    class SecureStringExample
    {
        public static void SecureAString()
        {
            using (SecureString ss = new SecureString()) // IDisposable implemented
            {
                Console.WriteLine("Please enter a password: ");
                while (true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Enter) break;

                    ss.AppendChar(cki.KeyChar);
                    Console.Write("*");
                }
                Console.WriteLine("\nPassword encrypted and stored succesfully!");
                ConvertToUnsecureString(ss);
                ss.MakeReadOnly();
            }
        }

        public static void ConvertToUnsecureString(SecureString securePassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                Console.WriteLine(Marshal.PtrToStringUni(unmanagedString));
            }
            catch (Exception)
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
