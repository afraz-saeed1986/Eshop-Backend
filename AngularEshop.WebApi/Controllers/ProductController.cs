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

            await Task.Delay(4000);

            return JsonResponseStatus.Success(products);
        }
        #endregion
    }
}
