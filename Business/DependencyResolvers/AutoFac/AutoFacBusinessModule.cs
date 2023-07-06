using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.AutoFac
{
    public class AutoFacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AuthenticateManager>().As<IAuthenticateService>();

            builder.RegisterType<BrokerManager>().As<IBrokerService>();
            builder.RegisterType<EfBrokerDal>().As<IBrokerDal>();

            builder.RegisterType<EfLoadDal>().As<ILoadDal>();

            builder.RegisterType<ShipManager>().As<IShipService>();
            builder.RegisterType<EfShipDal>().As<IShipDal>();

            //builder.RegisterType<ShipManager>().As<IShipService>();
            builder.RegisterType<EfLoadDal>().As<ILoadDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfAspNetUserDal>().As<IAspNetUserDal>();

            builder.RegisterType<ContactUsFormManager>().As<IContactUsFormService>();
            builder.RegisterType<EfContactUsFormDal>().As<IContactUsFormDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
