using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GreenField.BLL.Services.ImageService
{
    public interface IImageService
    {
        Task SaveImage(IFormFile image, string path);
    }
}