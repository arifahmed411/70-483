using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollection
{
    class ParallelProgrammingDemo
    {

        public static void example1()
        {
            Action<Action> measure = (body) =>
                {
                    var startTime = DateTime.Now;
                    body();
                    Console.WriteLine("{0} {1}", DateTime.Now - startTime,
                        Thread.CurrentThread.ManagedThreadId);
                };

            Action calcJob = () => { for (int i = 0; i < 350000000; i++) ; };
            Action ioJob = () => { Thread.Sleep(1000); };

            measure(() =>
                {
                    var tasks = Enumerable.Range(1, 10)
                                .Select(_ => Task.Factory.StartNew(() => measure(ioJob)))
                                .ToArray();
                    //new[]
                    //{
                    //    Task.Factory.StartNew(() => measure(calcJob)),
                    //    Task.Factory.StartNew(() => measure(calcJob)),
                    //    Task.Factory.StartNew(() => measure(calcJob)),
                    //    Task.Factory.StartNew(() => measure(calcJob)),
                    //    Task.Factory.StartNew(() => measure(calcJob))
                    //};

                    Task.WaitAll(tasks);
                });



        }
    }
}
