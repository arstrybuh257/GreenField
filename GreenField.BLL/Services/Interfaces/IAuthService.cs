using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        public string CreateToken(UserDto user);
    }
}