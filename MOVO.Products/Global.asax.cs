using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using MOVO.Products.Contracts.Products;
using MOVO.Products.Core;
using MOVO.Products.Features.Products;

namespace MOVO.Products {
    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var vatRate = ConfigurationManager.AppSettings["vatRate"];
            decimal.TryParse(vatRate, out var vatRateAsDecimal);
            IoC.Initialize(new Module[] {new ApplicationModule(vatRateAsDecimal) });

            //hacky way to add initial data
            this.AddData();
        }

        private void AddData() {
            var repository = IoC.Resolve<IRepository<Product>>();

            var doesAnyRecordExist = repository.GetAll().Any();
            if (doesAnyRecordExist) {
                return;
            }

            var product1 = new Product {
                Id = Guid.NewGuid(),
                ArticleNumber = "39533028",
                ArticleName = "Elcykel Allegro",
                Description = "En smart och tillförlitlig elcykel för dam från Batavus utmärkt både för långa och kortare turer.",
                Category = ProductCategory.Sport,
                Price = 100
            };

            var product2 = new Product {
                Id = Guid.NewGuid(),
                ArticleNumber = "40266837",
                ArticleName = "Lapierre Overvolt Urban 300",
                Description = "Praktisk och bekväm elcykel med upprätt körställning",
                Category = ProductCategory.Sport,
                Price = 200
            };

            var product3 = new Product {
                Id = Guid.NewGuid(),
                ArticleNumber = "p35372817",
                ArticleName = "Chrome cast 2nd generationen",
                Description = "Visar film eller annan media från mobilen på TV:n",
                Category = ProductCategory.Electronics,
                Price = 300
            };

            var product4 = new Product {
                Id = Guid.NewGuid(),
                ArticleNumber = "35552289",
                ArticleName = "Apple TV 64GB 4th generation",
                Description = "Nu kommer App Store till din tv. Du kan förvänta dig massor av spännande spel.",
                Category = ProductCategory.Electronics,
                Price = 400
            };

            var product5 = new Product {
                Id = Guid.NewGuid(),
                ArticleNumber = "40151785",
                ArticleName = "Big Foot truck 2wd",
                Description = "Det här är bilen som startade alltihop och skapade den idag enorma monster-truck trenden.",
                Category = ProductCategory.Toys,
                Price = 500
            };

            repository.Save(product1);
            repository.Save(product2);
            repository.Save(product3);
            repository.Save(product4);
            repository.Save(product5);
        }
    }
}