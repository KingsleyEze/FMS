using System.ComponentModel.DataAnnotations;

namespace FMS.Core.ViewModel.Account
{
    public class LoginView
    {
        [Required]
        [UIHint("username")]
        public string UserName { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
