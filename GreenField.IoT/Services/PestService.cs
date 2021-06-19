using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GreenField.IoT.Constants;
using GreenField.IoT.Interfaces;
using GreenField.IoT.Models;

namespace GreenField.IoT.Services
{
    public class PestService : IPestService
    {
        private ITokenProvider _tokenProvider;
        
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public PestService(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }
        
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var token = _tokenProvider.GetTokenAsync().Result;
            client.DefaultRequestHeaders.Add("Authorization", token);
            return client;
        }
        public async Task<List<Pest>> GetPestsAsync()
        {
            HttpClient client = GetClient();
          
            string result = (await client.GetStringAsync(Endpoints.GetAllPests));
            return JsonSerializer.Deserialize<List<Pest>>(result, options);
        }
    }
}