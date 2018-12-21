using Autofac;
using Autofac.Integration.Mvc;
using MOVO.Products.Features.Products;
using NHibernate;

namespace MOVO.Products.Core {
    public class ApplicationModule : Module {
        private readonly decimal vatRate;

        public ApplicationModule(decimal vatRate) {
            this.vatRate = vatRate;
        }

        protected override void Load(ContainerBuilder builder) {
            builder.Register(x => NhibernateHelper.OpenSession()).As<ISession>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            
            builder.RegisterType<ProductService>().As<IProductService>()
                   .WithParameter(new NamedParameter("vatRate", this.vatRate));

            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();
            builder.RegisterModule<AutofacWebTypesModule>();
        }
    }
}