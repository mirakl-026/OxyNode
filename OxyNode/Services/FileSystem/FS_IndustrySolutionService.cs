using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OxyNode.Infrastructure.Interfaces.FileSystem;

namespace OxyNode.Services.FileSystem
{
    public class FS_IndustrySolutionService : IFileIndustrySolutionService
    {
        public Task AddIndustrySolution(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllIndustrySolutions()
        {
            throw new NotImplementedException();
        }

        public Task DeleteIndustrySolution(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetIndustrySolutionsFilesList()
        {
            throw new NotImplementedException();
        }
    }
}
