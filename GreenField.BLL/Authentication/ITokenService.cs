using GreenField.DAL.Entities;

namespace GreenField.BLL.Authentication
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}