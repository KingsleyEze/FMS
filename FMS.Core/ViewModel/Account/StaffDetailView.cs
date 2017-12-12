using System;

namespace FMS.Core.ViewModel.Account
{
    public class StaffDetailView
    {
        public string Title { get; set; }
        public string Rank { get; set; }
        public string GradeLevel { get; set; }
        public DateTime? DateOfFirstAppoint { get; set; }
        public DateTime? DateOfCurrentAppoint { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string Section { get; set; }
        public string Notes { get; set; }
    }
}
