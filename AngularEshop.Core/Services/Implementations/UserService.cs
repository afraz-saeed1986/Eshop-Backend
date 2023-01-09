using AngularEshop.Core.DTOs.Account;
using AngularEshop.Core.Security;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor
        private IGenericRepository<User> userRepository;
        private IPasswordHelper passwordHelper;
        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper)
        {
            this.userRepository = userRepository;
            this.passwordHelper = passwordHelper;
        }

        #endregion

        #region User Section
        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {
            if (IsUserExistsByEmail(register.Email))
                return RegisterUserResult.EmailExists;

            var user = new User
            {
                Email = register.Email.SanitizeText(),
                Address = register.Address.SanitizeText(),
                FirstName = register.FirstName.SanitizeText(),
                LastName = register.LastName.SanitizeText(),
                EmailActiveCode = Guid.NewGuid().ToString(),
                Password = passwordHelper.EncodePasswordMd5(register.Password)
      
            };

           await userRepository.AddEntity(user);
           await userRepository.SaveChanges();

            return RegisterUserResult.Success;
        }

        public bool IsUserExistsByEmail(string email)
        {
            return  userRepository.GetEntitiesQuery().Any(u => u.Email == email.ToLower().Trim());
        }

        public async Task<LoginUserResult> LoginUser(LoginUserDTO login)
        {
            var password = passwordHelper.EncodePasswordMd5(login.Password);
            var user = await userRepository.GetEntitiesQuery()
                .SingleOrDefaultAsync(u => u.Email == login.Email.ToLower().Trim() && u.Password == password);

            if (user == null) return LoginUserResult.IncorrectData;

            if (!user.IsActivated) return LoginUserResult.NotActivated;

            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u => u.Email == email.ToLower().Trim());
        }

        public async Task<User> GetUserByUserId(long userId)
        {
            return await userRepository.GetEntityById(userId);
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            userRepository?.Dispose();
        }
        #endregion

    }
}
