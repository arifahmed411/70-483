using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RunManager;

namespace EncryptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner.RunEverything(new Program());
            Runner.RunEverything(new AESEncryption());
            Runner.RunEverything(new RSACSPSample());
            Runner.RunEverything(new StoreKey());
            Runner.RunEverything(new SHA256Demo());
            //Runner.RunEverything(new DigitalSignatureDemo());
            Runner.RunEverything(new SecureStringExample());
        }


    }
}
