using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using DynamicProxySamples.Test.Utils;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace DynamicProxySamples.Test
{
    public class TimingExtensionTest
    {
        [Fact]
        public void When_AddTimingToObject_IsProxy_Test()
        {
            User user = new User().AddTiming();
            Assert.True(ProxyUtil.IsProxy(user));
        }

        [Fact]
        public void When_AddTimingToObject_AddAnInterceptor_Test()
        {
            var proxyTarget = (IProxyTargetAccessor) new User().AddTiming();
            Assert.True(proxyTarget.GetInterceptors().Any());
        }

        [Fact]
        public void WriterIsUsed_WhenA_MethodIsInvoke_Test()
        {
            Mock<IWriter> writerMoq = new Mock<IWriter>();
            TimingExtension.Configure(writerMoq.Object);
            User user = new User().AddTiming();
            
            user.Greet();

            writerMoq.Verify(w => w.Write(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void ConfigureSingleMethod_When_AddTiming_Test()
        {
            Mock<IWriter> writerMoq = new Mock<IWriter>();
            TimingExtension.Configure(writerMoq.Object);
            User user = new User().AddTiming(nameof(User.SayGoodbye));

            user.Greet();
            writerMoq.Verify(w => w.Write(It.IsAny<string>()), Times.Never);

            user.SayGoodbye();
            writerMoq.Verify(w => w.Write(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void ConfigureSingleMethod_When_AddTiming_With_LambdaExpression_Test()
        {
            Mock<IWriter> writerMoq = new Mock<IWriter>();
            TimingExtension.Configure(writerMoq.Object);
            User user = new User().AddTiming(u => u.SayGoodbye());

            user.Greet();
            writerMoq.Verify(w => w.Write(It.IsAny<string>()), Times.Never);

            user.SayGoodbye();
            writerMoq.Verify(w => w.Write(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        [Trait("Issues", "Issues")]
        public void ConfigureMethod_NotTakeIntoAccountParameters_Test()
        {
            Mock<IWriter> writerMoq = new Mock<IWriter>();
            TimingExtension.Configure(writerMoq.Object);
            User user = new User().AddTiming(u => u.Greet("foo"));

            user.Greet("bar");
            writerMoq.Verify(w => w.Write(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        [Trait("Issues", "Issues")]
        public void ConfigureMethod_NotDistinguishMethodsOverloads_Test()
        {
            Mock<IWriter> writerMoq = new Mock<IWriter>();
            TimingExtension.Configure(writerMoq.Object);
            User user = new User().AddTiming(u => u.Greet());

            user.Greet("bar");
            writerMoq.Verify(w => w.Write(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void ThrowsException_When_ConfigureNonVirtualMethod_Test()
        {
            ClassWithNonVirtualMethod instance = new ClassWithNonVirtualMethod();

            Assert.ThrowsAny<Exception>(() => instance.AddTiming(c => c.NonVirtualMethod()));
        }
        
    }
}
