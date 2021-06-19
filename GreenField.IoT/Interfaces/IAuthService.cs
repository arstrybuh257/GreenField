using System.Threading.Tasks;
using GreenField.IoT.Services;

namespace GreenField.IoT.Interfaces
{
    public interface IAuthService
    {
        Task<string> GetToken(LoginModel model);
    }
}