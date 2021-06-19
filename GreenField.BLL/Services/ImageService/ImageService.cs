using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GreenField.BLL.Services.ImageService
{
    public class ImageService : IImageService
    {
        private const string ImagePathBase = "";
        public async Task SaveImage(IFormFile image, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            await using (FileStream fs = File.Create(path))
            {
                await image.CopyToAsync(fs);
                fs.Flush();
            }
        }
    }
}