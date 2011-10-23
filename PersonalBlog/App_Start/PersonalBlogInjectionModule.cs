using System.Configuration;
using System.Web;
using Ninject;
using Ninject.Syntax;
using Ninject.Modules;
using PersonalBlog.Services;
using NHibernate;

namespace PersonalBlog.App_Start {
    public class PersonalBlogInjectionModule : NinjectModule {

        public PersonalBlogInjectionModule() {}

        public override void Load() {
            Kernel.Settings.AllowNullInjection = true;

            Bind<ConfigSettingsService>().ToConstant(new ConfigSettingsService()).InSingletonScope();
            Bind<DbSessionFactory>().To<DbSessionFactory>().InSingletonScope();
            Bind<ISession>().ToMethod(x => Kernel.Get<DbSessionFactory>().Session).InRequestScope();
            Bind<ContentFileService>().To<ContentFileService>().InRequestScope();
            Bind<BlogPostService>().To<BlogPostService>().InRequestScope();
        }
    }
}