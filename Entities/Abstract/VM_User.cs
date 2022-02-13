using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
    public class VM_User
    {
        [Required(ErrorMessage = "Ad Alanı boş Geçilemez")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }

        [Display(Name = "Tel No:")]
        [RegularExpression(@"^(0(\d{3}) (\d{3}) (\d{2}) (\d{2}))$", ErrorMessage = "telefon numarası uygun formatta değil")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mail Adresi boş geçilemez")]
        [EmailAddress(ErrorMessage = "Email doğru formatta değil")]
        [Display(Name = "Email Adresiniz :")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Şifre Alanı boş geçilemez")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifreniz :")]
        public string Password { get; set; }

        [Display(Name = "İl :")]
        public string City { get; set; }

        [Display(Name = "Resim :")]
        public string Picture { get; set; }


        [Display(Name = "Doğum Tarihiniz :")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

  
    }
}
