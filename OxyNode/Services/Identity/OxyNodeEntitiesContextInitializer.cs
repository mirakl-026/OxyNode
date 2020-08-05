using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OxyNode.Services.Identity
{
    public class OxyNodeEntitiesContextInitializer
    {
        private readonly OxyNodeEntitiesContext _db;
        public OxyNodeEntitiesContextInitializer(OxyNodeEntitiesContext db) => _db = db;

        public async Task InitializeAsync()
        {
            //if(await _db.Database.EnsureDeletedAsync())
            //{
            //    // БД существовала и была успешно удалена
            //}

            // проверка что бд существует
            //await _db.Database.EnsureCreatedAsync();

            // автоматическое создание и миграция БД до последней версии
            await _db.Database.MigrateAsync();

        }
    }
}
