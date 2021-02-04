using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Audit.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Нэвтрэх нэр оруулна уу!")]
        [Display(Name = "Нэвтрэх нэр")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Нууц үг оруулна уу!")]
        [Display(Name = "Нууц үг")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}