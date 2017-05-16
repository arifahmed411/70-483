using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RunManager
{
    public class Runner
    {

        public delegate void del();

        public static void run(del d)
        {
            Console.WriteLine(d.Method.Name);
            Console.WriteLine();
            d.Invoke();
            Console.WriteLine();
        }

        public static Type getType(Object runObject)
        {
            //string fullAssemblyName = Assembly.GetExecutingAssembly().FullName;
            //string fullClassName = MethodBase.GetCurrentMethod().DeclaringType.FullName;

            string fullAssemblyName = runObject.GetType().Assembly.FullName;
            string fullClassName = runObject.GetType().FullName;

            Assembly assembly = Assembly.Load(fullAssemblyName);

            //Type type = Type.GetType(fullClassName);
            Type type = assembly.GetType(fullClassName);
            return type;
        }

        public static IEnumerable<del> getDelegates(Type type)
        {

            var delegates = type.GetMethods()
                                .Select(m => (del)Delegate.CreateDelegate(typeof(del), m, false))
                                .Where(m => m != null);

            return delegates;
        }

        public static void RunEverything(Object runObject)
        {
            Type type = getType(runObject);
            var delegates = getDelegates(type);

            foreach (var method in delegates)
            {
                run(method);
            }
        }

        public static void RunAllExamples(Object runObject)
        {

            Type type = getType(runObject);
            var delegates = getDelegates(type)
                .Where(d => d.GetMethodInfo().Name.ToLower().Contains("example"));

            foreach (var method in delegates)
            {
                run(method);
            }
        }

    }
}
