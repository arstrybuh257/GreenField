using System.Threading.Tasks;
using GreenField.IoT.Interfaces;
using GreenField.IoT.Services;

namespace GreenField.IoT.Utils
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IAuthService _authService;
        private string _token;

        public TokenProvider(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<string> GetTokenAsync()
        {
            if (string.IsNullOrEmpty(_token))
            {
                _token = await _authService.GetToken(new LoginModel(Constants.Constants.NureAdminEmail,
                    Constants.Constants.NureAdminPassword));
            }
            return _token;
        }

        public void RemoveToken()
        {
            _token = null;
        }
    }
}