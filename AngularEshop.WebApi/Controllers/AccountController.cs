using AngularEshop.Core.DTOs.Account;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.Core.Utilities.Extensions.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.WebApi.Controllers
{

    [EnableCors("EnableCors")]
    public class AccountController : SiteBaseController
    {
        #region constructor 
        private IUserService userService;
        private IConfiguration configuration;
        public AccountController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        #endregion

        #region Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO register)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.RegisterUser(register);

                switch (res)
                {
                    case RegisterUserResult.EmailExists:
                        return JsonResponseStatus.Error(new { info = "EmailExist" });
                }
            }

            return JsonResponseStatus.Success();
        }
        #endregion

        #region Login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.LoginUser(login);
                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return JsonResponseStatus.NotFound(new {message = "کاربری با مشخصات وارد شده یافت نشد"});
                    case LoginUserResult.NotActivated:
                        return JsonResponseStatus.Error(new {message = "حساب کاربری شما فعال نشده است"});
                    case LoginUserResult.Success:
                        var user = await userService.GetUserByEmail(login.Email);

                        var jwtKey = configuration.GetValue<string>("JWT:Key");
                        var issuerKey = configuration.GetValue<string>("JWT:ValidIssuer");

                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEshopJwtBearer"));
                        var signingCredencials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOptions = new JwtSecurityToken(
                            issuer: "https://localhost:7043/",
                            claims: new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,user.Email),
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                            },
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: signingCredencials
                            );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                        return JsonResponseStatus.Success(new {
                            token = tokenString, 
                            expireTime= 30, 
                            firstName = user.FirstName, 
                            lastName = user.LastName, 
                            userId = user.Id, 
                            address = user.Address});

                }
            } 

            return  JsonResponseStatus.Error();
        }
        #endregion

        #region Check User Authentication
        [EnableCors("EnableCors")]
        [Authorize]
        [HttpPost("checked-auth")]
        public async Task<IActionResult> CheckUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userService.GetUserByUserId(User.GetuserId());
                return JsonResponseStatus.Success(new
                {
                    userId = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    address = user.Address,
                    email = user.Email
                });
                //var user = userService.GetUserByUserId()
            }

            return JsonResponseStatus.Error();
        }

        #endregion

        #region SignOut
        [HttpGet("sign-out")]
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return JsonResponseStatus.Success();
            }

            return JsonResponseStatus.Error();
        }
        #endregion
    }
}
