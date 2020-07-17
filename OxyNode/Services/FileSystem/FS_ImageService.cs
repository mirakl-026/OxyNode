using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OxyNode.Infrastructure.Interfaces.FileSystem;

namespace OxyNode.Services.FileSystem
{
    public class FS_ImageService : IFileImageService
    {
        public Task AddImage(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllImages()
        {
            throw new NotImplementedException();
        }

        public Task DeleteImage(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetImagesFilesList()
        {
            throw new NotImplementedException();
        }
    }
}
