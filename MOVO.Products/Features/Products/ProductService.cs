using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using MOVO.Products.Contracts.Products;
using MOVO.Products.Core;
using NLog;

namespace MOVO.Products.Features.Products {
    public class ProductService : IProductService {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly IRepository<Product> productRepository;

        private readonly decimal vatRate;

        public ProductService(IRepository<Product> productRepository, decimal vatRate) {
            this.productRepository = productRepository;
            this.vatRate = vatRate;
        }

        public IEnumerable<ProductDto> GetProductsFromCategory(ProductCategory category) {
            try {
                var productsFromCategory = this.productRepository.Filter<Product>(x => x.Category == category);

                return productsFromCategory == null
                    ? new List<ProductDto>()
                    : this.ConvertProductsToDtoses(productsFromCategory);
            }
            catch (Exception e) {
                //this.logger.Debug($"Failed to retrieve products from category '{category}'! {e}")
                throw;
            }
        }

        public ProductDto GetSpecificProduct(ProductCategory productCategory, string name) {
            try {
                //assuming it's only 1 product
                var productWithSpecificName = this.productRepository.Filter<Product>(x => x.Category == productCategory && x.ArticleName == name).FirstOrDefault();

                return productWithSpecificName == null
                    ? null
                    : this.ConvertProductToDto(productWithSpecificName);
            }
            catch (Exception e) {
                this.logger.Debug($"Failed to retrieve products with name '{name}'! {e}");
                throw;
            }
        }

        public IEnumerable<ProductDto> GetAllProducts() {
            try {
                var allProducts = this.productRepository.GetAll();

                return allProducts == null
                    ? new List<ProductDto>()
                    : this.ConvertProductsToDtoses(allProducts);
            }
            catch (Exception e) {
                this.logger.Debug($"Failed to retrieve products! {e}");
                throw;
            }
        }

        public void UpdateProduct(ProductDto dto) {
            try {
                var product = this.productRepository.Get(dto.Id);

                if (product == null) {
                    throw new ObjectNotFoundException($"Could not find a product with id {dto.Id}");
                }

                product.ArticleName = dto.ArticleName;
                product.ArticleNumber = dto.ArticleNumber;
                product.Category = dto.Category;
                product.Description = dto.Description;
                product.Price = dto.Price;


                this.productRepository.Update(product);
                this.logger.Info($"Updated product with id {dto.Id}");
            }
            catch (Exception e) {
                this.logger.Debug($"Failed to update product with id '{dto.Id}'! {e}");
                throw;
            }
        }

        private ProductDto ConvertProductToDto(Product product) {
            return new ProductDto {
                Id = product.Id,
                ArticleNumber = product.ArticleNumber,
                ArticleName = product.ArticleName,
                Description = product.Description,
                Category = product.Category,
                Price = product.Price,
                PriceWithVat = decimal.Round(product.Price * (1 + (this.vatRate/100)), 2)
            };
        }

        private IEnumerable<ProductDto> ConvertProductsToDtoses(IEnumerable<Product> products) {
            return products.Select(this.ConvertProductToDto);
        }
    }
}