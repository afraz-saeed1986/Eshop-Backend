using AngularEshop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AngularEshop.WebApi.Controllers
{
    public class UsersController : SiteBaseController
    {
        #region constructor
        private IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region users list
        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            return new ObjectResult( await userService.GetAllUsers());
        }
        #endregion
    }
}
