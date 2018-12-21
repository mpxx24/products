using System.Collections.Generic;

namespace MOVO.Products.Features.Products {
    public interface IProductService {
        IEnumerable<ProductDto> GetProductsFromCategory(ProductCategory category);
        ProductDto GetSpecificProduct(ProductCategory productCategory, string name);
        IEnumerable<ProductDto> GetAllProducts();
        void UpdateProduct(ProductDto dto);
    }
}