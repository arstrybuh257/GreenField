using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GreenField.IoT.Constants;
using GreenField.IoT.Interfaces;
using GreenField.IoT.Utils;

namespace GreenField.IoT.Services
{
    public class AuthService : IAuthService
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            return client;
        }

        public async Task<string> GetToken(LoginModel model)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Endpoints.AuthUrlPath, new StringContent(JsonSerializer.Serialize(model, options), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return token;
            }

            return null;
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}