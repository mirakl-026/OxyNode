using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OxyNode.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxyNode.Services.Identity
{
    public class OxyNodeEntitiesContext : IdentityDbContext<User, Role, string>
    {
    }
}
