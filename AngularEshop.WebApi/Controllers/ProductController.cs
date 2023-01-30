using AngularEshop.Core.DTOs.Products;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularEshop.WebApi.Controllers
{

    [EnableCors("EnableCors")]
    public class ProductController : SiteBaseController
    {
        #region constructor
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }


        #endregion

        #region products
        [AllowAnonymous]
        [HttpGet("filter-products")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsDTO filter)
        {
            var products = await productService.FilterProducts(filter);

            //await Task.Delay(4000);

            return JsonResponseStatus.Success(products);
        }
        #endregion

        #region get products categories
        [AllowAnonymous]
        [HttpGet("product-active-categories")]
        public async Task<IActionResult> GetProductsCategories()
        {
            return JsonResponseStatus.Success(await productService.GetAllActiveProductCategories());
        }

        #endregion

        #region get single product
        [AllowAnonymous]
        [HttpGet("single-product/{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var product = await productService.GetProductById(id);
            var productGalleries = await productService.GetProductActiveGalleries(id);


            if(product != null)
            return JsonResponseStatus.Success(new {product = product, galleries = productGalleries });

            return JsonResponseStatus.NotFound();
        }
        #endregion

        #region related products
        [HttpGet("related-products/{id}")]
        public async Task<IActionResult> GetRelatedProducts(long id)
        {
            var relatedProduct = await productService.GetRelatedProducts(id);
            return JsonResponseStatus.Success(relatedProduct);
        }
        #endregion
    }
}
