using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GreenField.BLL.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string> SignInAsync(string email, string password);
    }
}