using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Identity
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if(password.ToLower().Contains(user.UserName.ToLower()))
            {
                if (user.Email.ToLower().Contains(user.UserName.ToLower()))
                {
                    
                    if (!password.ToLower().Contains(user.Email.ToLower()))
                    {
                        errors.Add(new IdentityError() { Code = "PasswordContainsUserName", Description = "şifre alanı kullanıcı adı içeremez" });
                    }
                }
            }
                
            if (password.ToLower().Contains("1234"))
            {
                errors.Add(new IdentityError() { Code = "PasswordContains1234", Description = "Şifre alanı ardaşık sayı içeremez " });
            }
            if (password.ToLower().Contains(user.Email.ToLower()))
            {
                errors.Add(new IdentityError() { Code = "PasswordContainsEmail", Description = "Şifre alanı  email içeremez " });
            }

            if (errors.Count==0)
            {
                return Task.FromResult(IdentityResult.Success);

            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
