using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
   public class VM_Login
    {

        [Required(ErrorMessage = "Mail Adresi boş geçilemez")]
        [EmailAddress(ErrorMessage = "Email doğru formatta değil")]
        [Display(Name = "Email Adresiniz :")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Şifre Alanı boş geçilemez")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifreniz :")]
        [MinLength(4, ErrorMessage = "Şifreniz En az 4 karakter olmalı")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
