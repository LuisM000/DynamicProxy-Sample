using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DynamicProxySamples.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            TimingExtension.Configure(new ConsoleWriter());
            var sleepyhead = new Sleepyhead().AddTiming();
            sleepyhead.Sleep();
            System.Console.ReadLine();
        }
    }

    public class Sleepyhead
    {
        public virtual void Sleep()
        {
            Thread.Sleep(500);
        }
    }

    class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            System.Console.WriteLine(text);
        }
    }
}
