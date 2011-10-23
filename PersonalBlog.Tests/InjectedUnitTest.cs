using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ninject;
using PersonalBlog.App_Start;

namespace PersonalBlog.Tests {
    public abstract class InjectedUnitTest {

        protected IKernel kernel;

        [TestFixtureSetUp]
        public void Setup() {
            kernel = new StandardKernel(new PersonalBlogInjectionModule());
        }

        protected T Get<T>() {
            return kernel.Get<T>();
        }
    }
}
