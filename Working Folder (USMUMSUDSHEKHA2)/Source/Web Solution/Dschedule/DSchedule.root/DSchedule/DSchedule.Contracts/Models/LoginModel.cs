using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DSchedule.Contracts.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
