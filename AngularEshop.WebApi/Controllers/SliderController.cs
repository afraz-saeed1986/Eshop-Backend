using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AngularEshop.WebApi.Controllers
{
    [EnableCors("EnableCors")]
    public class SliderController : SiteBaseController
    {
        #region constructor
        private ISliderService sliderService;
        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }
        #endregion

        #region all active sliders
        [AllowAnonymous]
        [HttpGet("GetActiveSliders")]
        public async Task<IActionResult> GetActiveSliders()
        {
            var sliders = await sliderService.GetActiveSliders();
            return JsonResponseStatus.Success(sliders);
        }
        #endregion

    }
}
