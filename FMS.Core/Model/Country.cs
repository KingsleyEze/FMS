using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class Country
    {
        [Key]
        public Guid Id { get; set; }
    }
}
