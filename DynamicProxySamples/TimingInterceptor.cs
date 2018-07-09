using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace DynamicProxySamples
{
    public class TimingInterceptor : StandardInterceptor
    {
        private readonly IWriter writer;
        private readonly Stopwatch stopwatch = new Stopwatch();

        public TimingInterceptor(IWriter writer)
        {
            this.writer = writer;
        }

        protected override void PreProceed(IInvocation invocation)
        {
            this.writer.Write($"{invocation.TargetType.Name}.{invocation.Method.Name} - start invocation");
            stopwatch.Restart();
        }

        protected override void PostProceed(IInvocation invocation)
        {
            stopwatch.Stop();
            this.writer.Write($"{invocation.TargetType.Name}.{invocation.Method.Name} - {stopwatch.ElapsedMilliseconds} ms - end invocation");
        }
        
    }
}
