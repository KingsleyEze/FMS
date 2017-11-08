
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class AppUserBank
    {
        [Key]
        public Guid Id { get; set; }
        public AppUser AppUser { get; set; }
        public string AccountNumber  { get; set; }
        public string AccountName { get; set; }
        public string BankBranchCode { get; set; }
        public string BVN { get; set; }
        public string TIN { get; set; }
    }
}
