using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AttributeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunManager.Runner.RunEverything(new SampleAttributeDemo());

            RunManager.Runner.RunEverything(new DataContractAttributeExample.Test());
        }
    }
}