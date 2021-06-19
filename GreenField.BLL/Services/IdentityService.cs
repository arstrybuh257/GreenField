using System.Threading.Tasks;
using GreenField.BLL.Authentication;
using GreenField.BLL.Services.Interfaces;
using GreenField.Common;
using GreenField.DAL.Repositories.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GreenField.BLL.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly PasswordManager _passwordManager;
        
        public IdentityService(IUserRepository userRepository, ITokenService tokenService, PasswordManager passwordManager)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordManager = passwordManager;
        }

        
        public async Task<string> SignInAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new GreenFieldException("invalid_credentials", "Invalid username or password");
            }

            var result = _passwordManager.ValidatePassword(user, password);

            if (!result)
            {
                throw new GreenFieldException("invalid_credentials", "Invalid username or password");
            }
            
            var token = _tokenService.CreateToken(user);

            return token;
        }
    }
}