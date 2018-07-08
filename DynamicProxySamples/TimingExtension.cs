using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Castle.DynamicProxy.Generators;

namespace DynamicProxySamples
{
    public static class TimingExtension
    {
        private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();
        private static IWriter writer;

        public static T AddTiming<T>(this T target) where T : class
        {
            return AddTiming(target, (string)null);
        }

        public static T AddTiming<T>(this T target, Expression<Action<T>> expression) where T : class
        {
            string methodIntercepted = (expression?.Body as MethodCallExpression)?.Method?.Name;
            return AddTiming(target, methodIntercepted);
        }

        public static T AddTiming<T>(this T target, string methodIntercepted) where T : class
        {
            var proxyGenerationOptions = new ProxyGenerationOptions();
            if (!string.IsNullOrEmpty(methodIntercepted))
                proxyGenerationOptions.Hook = new TimingProxyGenerationHook(methodIntercepted);
            var proxy = proxyGenerator.CreateClassProxyWithTarget(target,
                        proxyGenerationOptions,
                        new TimingInterceptor(writer));
            return (T)proxy;
        }
    
       

 

        public static void Configure(IWriter timingWriter)
        {
            writer = timingWriter;
        }
    }
}
