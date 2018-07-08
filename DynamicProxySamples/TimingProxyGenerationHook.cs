using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace DynamicProxySamples
{
    public class TimingProxyGenerationHook : IProxyGenerationHook
    {
        private readonly string methodIntercepted;

        public TimingProxyGenerationHook(string methodIntercepted)
        {
            this.methodIntercepted = methodIntercepted;
        }


        public void MethodsInspected()
        {
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            if (this.IsInterceptable(memberInfo as MethodInfo))
                throw new InvalidOperationException($"{memberInfo.Name} is not interceptable member");
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            return IsInterceptable(methodInfo);
        }

        private bool IsInterceptable(MethodInfo methodInfo)
        {
            return string.Equals(methodIntercepted, methodInfo?.Name);
        }
    }
}
