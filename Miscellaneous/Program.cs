using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miscellaneous
{
    class Program
    {
        static void Main(string[] args)
        {
            RunManager.Runner.RunAllExamples(new Program());
        }

        public static void ExampleTryParse()
        {
            String[] values = { null, "160519", "9432.0", "16,667",
                          "   -322   ", "+4302", "(100);", "01FA" };
            foreach (var value in values)
            {
                int number;

                bool result = Int32.TryParse(value, out number);
                if (result)
                {
                    Console.WriteLine("Converted '{0}' to {1}.", value, number);
                }
                else
                {
                    //            if (value == null) value = ""; 
                    Console.WriteLine("Attempted conversion of '{0}' failed.\tOutput: {1}",
                                       value == null ? "<null>" : value, number);

                }
            }
        }


    }
}
