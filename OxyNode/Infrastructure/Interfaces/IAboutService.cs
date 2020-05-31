using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// интерфейс для сервиса управляющим страницей About
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IAboutService
    {
        #region CRUD
        // Create
        public Task CreateAbout(About a);


        // Read
        public Task<About> ReadAbout();


        // Update
        public Task UpdateAbout(About a);


        // Delete
        public Task DeleteAbout();

        #endregion
    }
}
