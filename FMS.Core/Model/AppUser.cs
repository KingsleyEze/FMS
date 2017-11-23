using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace FMS.Core.Model
{
    public class AppUser : IdentityUser
    {

        public AppUser(string username) : base(username){ IsActive = true; }
        public AppUser() : base() { IsActive = true; }
        public string UserType { get; set; }
        public string PicturePixUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public override string ToString() => $"{FirstName} {LastName}";


    }
}
