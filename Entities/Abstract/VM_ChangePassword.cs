using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
   public class VM_ChangePassword
    {

        [Required(ErrorMessage = "Eski Şifreniz gereklidir")]
        [DataType(DataType.Password)]
        [Display(Name = "Eski Şifreniz :")]
        [MinLength(4, ErrorMessage = "Şifreniz En az 4 karakter olmalı")]
        public string PasswordOld { get; set; }


        [Required(ErrorMessage = "Deşirtirmek istediğiniz şifrenizi girmelisiniz")]
        [DataType(DataType.Password)]
        [Display(Name = " Yeni Şifreniz :")]
        [MinLength(4, ErrorMessage = "Şifreniz En az 4 karakter olmalı")]

        public string PasswordNew { get; set; }

        [Required(ErrorMessage = "Yeni şifre onayı gereklidir")]
        [DataType(DataType.Password)]
        [Display(Name = "Onay Yeni Şifreniz :")]
        [MinLength(4, ErrorMessage = "Şifreniz En az 4 karakter olmalı")]
        [Compare("PasswordNew", ErrorMessage = "Yeni şifreniz ve onay yeni şifreniz birbirinden farklı olmamalıdır")]
        public string PasswordConfirm { get; set; }
    }
}
