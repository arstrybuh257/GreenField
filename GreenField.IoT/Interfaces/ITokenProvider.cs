using System.Threading.Tasks;

namespace GreenField.IoT.Interfaces
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync();
        void RemoveToken();
    }
}