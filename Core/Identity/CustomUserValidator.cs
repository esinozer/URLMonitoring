using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Identity
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> err = new List<IdentityError>();

            string[] d = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

                if(char.IsNumber(Convert.ToChar(user.UserName.Substring(0,1)) ))      
                {
                err.Add(new IdentityError { Code = "usernot", Description = "Kullanıcının ilk karakteri sayısal değer içeremez" });
                 }

            if (err.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(err.ToArray()));
            }

        }
    }
}
