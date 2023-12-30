using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Utilities
{
    public static class ImageBaseClass
    {
        public static async Task<string> Upload(IFormFile file, string webRootPath)
        {
            using (var image = Image.Load(file.OpenReadStream()))
            {
                var extension = Path.GetExtension(file.FileName);

                var newName = Guid.NewGuid() + extension;
                string localLocation = Path.Combine(webRootPath, "Assets/images/", newName);
                string location = Path.Combine("https://localhost:7025/Assets/images/", newName);

                image.Mutate(x => x.Resize(1920, 0));
                await image.SaveAsync(localLocation);
                return location;
            }
        }
    }
}

