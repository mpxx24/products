using System.Web.Mvc;
using System.Web.Routing;
using MOVO.Products.Features.Products;

namespace MOVO.Products {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "",
                new {controller = "Products", action = "GetAllProducts"}
            );

            routes.MapRoute(
                "toys",
                "toys",
                new {controller = "Products", action = "GetProductsFromCategory", productCategory = ProductCategory.Toys}
            );

            routes.MapRoute(
                "sport",
                "sport",
                new {controller = "Products", action = "GetProductsFromCategory", productCategory = ProductCategory.Sport}
            );

            routes.MapRoute(
                "by productName",
                "sports/{productName}",
                new {controller = "Products", action = "GetSpecificProduct", productCategory = ProductCategory.Sport, productName = ""}
            );
        }
    }
}