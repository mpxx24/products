using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MOVO.Products.Features.Products;
using MOVO.Products.Models;

namespace MOVO.Products.Controllers {
    public class ProductsController : Controller {
        private readonly IProductService productService;

        public ProductsController(IProductService productService) {
            this.productService = productService;
        }

        public ActionResult GetAllProducts() {
            var data = this.productService.GetAllProducts();
            return this.View(this.ConvertMultipleDtosesToViewModels(data));
        }

        public ActionResult GetProductsFromCategory(ProductCategory productCategory) {
            var data = this.productService.GetProductsFromCategory(productCategory);
            return this.View(this.ConvertMultipleDtosesToViewModels(data));
        }

        public ActionResult GetSpecificProduct(ProductCategory productCategory, string productName) {
            var data = this.productService.GetSpecificProduct(productCategory, productName);
            return this.View(this.ConvertDtoToViewModel(data));
        }

        public void UpdateProduct(ProductViewModel viewModel) {
            this.productService.UpdateProduct(this.ConvertViewModelToDto(viewModel));
        }

        private ProductViewModel ConvertDtoToViewModel(ProductDto dto) {
            return new ProductViewModel {
                Id = dto.Id,
                ArticleName = dto.ArticleName,
                Description = dto.Description,
                ArticleNumber = dto.ArticleNumber,
                Price = dto.Price,
                Category = dto.Category,
                PriceWithVat = dto.PriceWithVat
            };
        }

        private ProductDto ConvertViewModelToDto(ProductViewModel viewModel) {
            return new ProductDto {
                Id = viewModel.Id,
                ArticleName = viewModel.ArticleName,
                Description = viewModel.Description,
                ArticleNumber = viewModel.ArticleNumber,
                Price = viewModel.Price,
                Category = viewModel.Category
            };
        }

        private IEnumerable<ProductViewModel> ConvertMultipleDtosesToViewModels(IEnumerable<ProductDto> dtos) {
            return dtos.Select(this.ConvertDtoToViewModel);
        }
    }
}