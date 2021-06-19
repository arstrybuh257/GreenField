using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GreenField.IoT.Constants;
using GreenField.IoT.Interfaces;
using GreenField.IoT.Models;
using GreenField.IoT.Utils;

namespace GreenField.IoT.Services
{
    public class FieldService : IFieldService
    {
        private ITokenProvider _tokenProvider;
        
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public FieldService(ITokenProvider tokenProvider)
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
        
        public async Task<Field> GetFieldAsync(Guid fieldId)
        {
            HttpClient client = GetClient();

            string result = await client.GetStringAsync(Path.Combine(Endpoints.GetField, fieldId.ToString()));
            return JsonSerializer.Deserialize<Field>(result, options);
        }

        public async Task<List<Field>> GetFieldsAsync()
        {
            HttpClient client = GetClient();
          
            string result = (await client.GetStringAsync(Endpoints.GetAllFields));
            return JsonSerializer.Deserialize<List<Field>>(result, options);
        }
    }
}