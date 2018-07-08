using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace DynamicProxySamples
{
    public class TimingInterceptor:IInterceptor
    {
        private readonly IWriter writer;
        private readonly Stopwatch stopwatch = new Stopwatch();

        public TimingInterceptor(IWriter writer)
        {
            this.writer = writer;
        }

        public void Intercept(IInvocation invocation)
        {
            this.writer.Write($"{invocation.TargetType.Name}.{invocation.Method.Name} - start invocation");
            stopwatch.Restart();

            invocation.Proceed();

            stopwatch.Stop();
            this.writer.Write($"{invocation.TargetType.Name}.{invocation.Method.Name} - {stopwatch.ElapsedMilliseconds} ms - end invocation");
        }
    }
}
