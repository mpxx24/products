using System;
using MOVO.Products.Features.Products;

namespace MOVO.Products.Contracts.Products {
    public class Product {
        public virtual Guid Id { get; set; }
        public virtual string ArticleNumber { get; set; }
        public virtual string ArticleName { get; set; }
        public virtual string Description { get; set; }
        public virtual ProductCategory Category { get; set; }
        public virtual decimal Price { get; set; }
    }
}