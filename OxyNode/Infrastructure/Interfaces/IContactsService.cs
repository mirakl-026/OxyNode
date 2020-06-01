using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// интерфейс для драйвера - управление содержимым "Контакты"

namespace OxyNode.Infrastructure.Interfaces
{

    public interface IContactsService
    {
        #region CRUD
        // Create
        public Task CreateContacts(Contacts c);

        // Read
        public Task<Contacts> ReadContacts();

        // Update
        public Task UpdateContacts(Contacts c);

        // Delete
        public Task DeleteContacts();
        #endregion
    }
}
