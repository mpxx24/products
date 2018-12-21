using System;

namespace MOVO.Products.Features.Products {
    public class ProductDto {
        public Guid Id { get; set; }
        public string ArticleNumber { get; set; }
        public string ArticleName { get; set; }
        public string Description { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public decimal PriceWithVat { get; set; }
    }
}