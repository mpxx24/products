using System;
using MOVO.Products.Features.Products;

namespace MOVO.Products.Models {
    public class ProductViewModel {
        public Guid Id { get; set; }
        public string ArticleNumber { get; set; }
        public string ArticleName { get; set; }
        public string Description { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public decimal PriceWithVat { get; set; }
    }
}