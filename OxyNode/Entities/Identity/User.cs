using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxyNode.Entities.Identity
{
    public class User : IdentityUser
    {
        public string Description { get; set; }
    }
}
