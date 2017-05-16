using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunManager.Runner.RunEverything(new DataContractSerializerExample());
            RunManager.Runner.RunEverything(new XMLSerializerDemo());
            RunManager.Runner.RunEverything(new NetDataContractSerializerDemo());
            RunManager.Runner.RunEverything(new XMLObjectSerializerDemo());
            RunManager.Runner.RunEverything(new JavaScriptSerializerDemo());
            RunManager.Runner.RunEverything(new DataContractJsonSerializerDemo());
        }
    }
}
