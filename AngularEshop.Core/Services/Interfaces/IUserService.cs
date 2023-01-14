using AngularEshop.Core.DTOs.Account;
using AngularEshop.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IUserService: IDisposable
    {
        Task<List<User>> GetAllUsers();
        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        bool IsUserExistsByEmail(string email);
        Task<LoginUserResult> LoginUser(LoginUserDTO login);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUserId (long userId);
        void ActivateUser(User user);
        Task<User> GetUserByEmailActiveCode(string emailActiveCode);
    }
}
