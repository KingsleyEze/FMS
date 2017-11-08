using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class AppUserRole
    {
        [Key]
        public Guid Id { get; set; }
    }
}
