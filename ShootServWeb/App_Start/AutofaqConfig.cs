using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Serilog;

namespace ShootServ
{
    public static class AutofaqConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterInstance(Log.Logger).As<ILogger>();
            
            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();
 
            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}