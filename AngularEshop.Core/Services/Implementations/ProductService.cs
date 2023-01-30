using AngularEshop.Core.DTOs.Paging;
using AngularEshop.Core.DTOs.Products;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Extensions.Paging;
using AngularEshop.DataLayer.Entities.Product;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region constructor
        private IGenericRepository<Product> productRepository;
        private IGenericRepository<ProductCategory> productCategoryRepository;
        private IGenericRepository<ProductGallery> productGalleryRepository;
        private IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository;
        private IGenericRepository<ProductVisit> productVisitRepository;

        public ProductService(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductCategory> productCategoryRepository,
            IGenericRepository<ProductGallery> productGalleryRepository,
            IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository,
            IGenericRepository<ProductVisit> productVisitRepository)
        {
            this.productRepository = productRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.productGalleryRepository = productGalleryRepository;
            this.productSelectedCategoryRepository = productSelectedCategoryRepository;
            this.productVisitRepository = productVisitRepository;
        }
        #endregion

        #region product
        public async Task AddProduct(Product product)
        {
           await productRepository.AddEntity(product);
           await productRepository.SaveChanges();
        }
        public async Task UpdateProduct(Product product)
        {
            productRepository.UpdateEntity(product);
            await productRepository.SaveChanges();
        }

        public async Task<FilterProductsDTO> FilterProducts(FilterProductsDTO filter)
        {
            var productsQuery = productRepository.GetEntitiesQuery().AsQueryable();

            switch (filter.OrderBy)
            {
                case productOrderBy.PriceAsc:
                    productsQuery = productsQuery.OrderBy(p => p.Price);
                    break;
                case productOrderBy.PriceDec:
                    productsQuery = productsQuery.OrderByDescending(p => p.Price);
                    break;
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(filter.Title));
            }

            if(filter.StartPrice != 0)
            {
                productsQuery = productsQuery.Where(p => p.Price >= filter.StartPrice);
            }
            if (filter.EndPrice != 0)
            {
                productsQuery = productsQuery.Where(p => p.Price <= filter.EndPrice);
            }

            productsQuery = productsQuery.Where(p => p.Price >= filter.StartPrice);

            if (filter.Categories != null && filter.Categories.Any())
            {
                productsQuery = productsQuery.
                    SelectMany(p => p.ProductSelectedCategories.
                    Where(c => filter.Categories.Contains(c.ProductCategoryId)).
                    Select(s => s.Product));
            }

            if (filter.EndPrice != 0)
                productsQuery = productsQuery.Where(p => p.Price <= filter.EndPrice);

            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProducts(products).SetPaging(pager);
        }

        public async Task<Product> GetProductById(long productId)
        {
            return await productRepository.GetEntityById(productId);
        }

        public async Task<List<Product>> GetRelatedProducts(long productId)
        {
            var product = await productRepository.GetEntityById(productId);
            if (product == null) return null;

            var productCategoriesList = await productSelectedCategoryRepository.GetEntitiesQuery()
                .Where(s => s.ProductId == productId).Select(f => f.ProductCategoryId).ToListAsync();

            var relatedProducts = await productRepository.GetEntitiesQuery().SelectMany(s =>
                s.ProductSelectedCategories.Where(f => productCategoriesList.Contains(f.ProductCategoryId)).Select(t => t.Product))
                .Where(s => s.Id != productId)
                .OrderByDescending(s => s.CreateDate).Take(4).ToListAsync();

            return relatedProducts;
        }
        #endregion

        #region product categories

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await productCategoryRepository.GetEntitiesQuery().Where(c => !c.IsDelete).ToListAsync();
        }

        #endregion

        #region products gallery
        public async Task<List<ProductGallery>> GetProductActiveGalleries(long productId)
        {
            return await productGalleryRepository.GetEntitiesQuery()
                .Where(g => g.ProductId == productId && !g.IsDelete)
                .Select(g => new ProductGallery
                {
                    ProductId = g.ProductId,
                    Id = g.Id,
                    ImageName = g.ImageName,
                    CreateDate = g.CreateDate
                })
                .ToListAsync();
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            productRepository?.Dispose();
            productCategoryRepository?.Dispose();
            productGalleryRepository?.Dispose();
            productSelectedCategoryRepository?.Dispose();
            productVisitRepository?.Dispose();
        }
        #endregion
    }
}
