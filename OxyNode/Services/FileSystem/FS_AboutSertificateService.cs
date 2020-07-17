using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OxyNode.Infrastructure.Interfaces.FileSystem;

namespace OxyNode.Services.FileSystem
{
    public class FS_AboutSertificateService : IFileAboutSertificateService
    {
        public Task AddAboutSertificate(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAboutSertificate(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllAboutSertificates()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAboutSertificatesFilesList()
        {
            throw new NotImplementedException();
        }
    }
}
