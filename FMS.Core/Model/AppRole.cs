using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class AppRole  : IdentityRole
    {
        public AppRole() : base()
        {
        }
        public AppRole(string roleName, string description) : base(roleName)
        {
            Description = description;
        }
        public string Description { get; set; }

    }
}
