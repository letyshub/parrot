using Autofac;
using Autofac.Core;
using Letys.Parrot.Core;
using Letys.Parrot.Model;
using System;

namespace Letys.Parrot.Bootstrap
{
    public static class BootStrapper
    {
        private static ILifetimeScope rootScope;

        public static void Start()
        {
            if (rootScope != null)
            {
                return;
            }

            var builder = new ContainerBuilder();
            builder.RegisterType<ApplicationRepository>().As<IApplicationRepository>().SingleInstance();
            builder.RegisterType<CategoriesModel>().As<ICategoriesModel>().SingleInstance();
            builder.RegisterType<TestSetsModel>().As<ITestSetsModel>().SingleInstance();
            builder.RegisterType<ScoreModel>().As<IScoreModel>().SingleInstance();

            rootScope = builder.Build();
        }

        public static void Stop()
        {
            rootScope.Dispose();
        }

        public static T Resolve<T>()
        {
            if (rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return rootScope.Resolve<T>();
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            if (rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return rootScope.Resolve<T>(parameters);
        }
    }
}
