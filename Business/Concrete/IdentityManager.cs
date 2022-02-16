using Business.Abstract;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Linq;

namespace Business.Concrete
{
    public class IdentityManager : IIdentityService
    {

        private UserManager<AppUser> _UserManager { get; }

        private SignInManager<AppUser> _SignInManager { get; }
        private RoleManager<AppRole> _RoleManager { get; }

        public IdentityManager(UserManager<AppUser> userManager = null, SignInManager<AppUser> signInManager = null, RoleManager<AppRole> roleManager = null)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _RoleManager = roleManager;

        }

        public AppUser FindByEmail(string Email)
        {
            return _UserManager.FindByEmailAsync(Email).Result;
        }

        public bool IsLockedOut(AppUser appUser)
        {
            return _UserManager.IsLockedOutAsync(appUser).Result;
        }

        //[LogAspect(typeof(FileLogger))]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<AppUser> Login(VM_Login vM_Login)
        {
            AppUser user = FindByEmail(vM_Login.Email);

            if (user != null)
            {

                Microsoft.AspNetCore.Identity.SignInResult signInResult = _SignInManager.PasswordSignInAsync(user, vM_Login.Password, vM_Login.RememberMe, false).Result;
                if (signInResult.Succeeded)
                {

                    return new SuccessDataResult<AppUser>(user, "GirişBaşarılı");
                }
                else
                {

                    return new ErrorDataResult<AppUser>(user, "Kullanıcı şifre veya mail yanlış");

                }


            }
            else
            {
                return new ErrorDataResult<AppUser>(user, "Bu email adresine kayıtlı kullanıcı bulunamamıştır");

            }

            // return new ErrorDataResult<AppUser>(user);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<IdentityResult> SingUp(VM_User vM_User)
        {
            if (_UserManager.Users.Any(u => u.PhoneNumber == vM_User.PhoneNumber))
            {
                //ModelState.AddModelError("", "Bu telefon numarası kayıtlıdır");

                return new ErrorDataResult<IdentityResult>("Bu telefon numarası kayıtlıdır");
            };


            AppUser appUser = new AppUser();
            appUser.UserName = vM_User.UserName;
            appUser.Email = vM_User.Email;
            appUser.PhoneNumber = vM_User.PhoneNumber;

            IdentityResult result = _UserManager.CreateAsync(appUser, vM_User.Password).Result;

            if (result.Succeeded)
            {
                return new SuccessDataResult<IdentityResult>("Üyeliğiniz oluşmuştur.Gişir Yapabilirsiniz");
            }
            else
            {

                return new ErrorDataResult<IdentityResult>(result);
            }
        }

        public IDataResult<AppUser> FindByName(string name)
        {
            AppUser user = _UserManager.FindByNameAsync(name).Result;

            if (user == null)
            {
                return new ErrorDataResult<AppUser>("Kullanıcı Bulunamadı");
            }

            return new SuccessDataResult<AppUser>(user);


        }

        public IResult SignOut()
        {
            _SignInManager.SignOutAsync();

            return new SuccessResult("Çıkış yapılmıştır");
        }

        public IResult SignIn( AppUser appUser)
        {
            _SignInManager.SignInAsync(appUser,true);

            return new SuccessResult("Giriş yapılmıştır");
        }

        public IDataResult<AppUser> FindById(string userId)
        {
            var User = _UserManager.FindByIdAsync(userId).Result;
            return new SuccessDataResult<AppUser>(User);
        }

        public IDataResult<IdentityResult> ChangePassword(VM_ChangePassword VM_ChangePassword, AppUser appUser)
        {
            bool exist = _UserManager.CheckPasswordAsync(appUser, VM_ChangePassword.PasswordOld).Result;


            if (exist)
            {
                IdentityResult result = _UserManager.ChangePasswordAsync(appUser, VM_ChangePassword.PasswordOld, VM_ChangePassword.PasswordNew).Result;


                if (result.Succeeded)
                {
                    
                    _UserManager.UpdateSecurityStampAsync(appUser);

                    _SignInManager.SignOutAsync();
                    _SignInManager.PasswordSignInAsync(appUser, VM_ChangePassword.PasswordNew, true, false);

                    return new SuccessDataResult<IdentityResult>(result, "Şifreniz Yenilendmiştir.");
                }
                else
                {
                    return new ErrorDataResult<IdentityResult>(result);
                }
            }
            else
            {

                return new ErrorDataResult<IdentityResult>("Eski şifreniz yanlış");
               
            }
        }

        public IDataResult<AppUser> EditUser(VM_User vM_User, IFormFile userPicture, string userIdentityName)
        {
            AppUser user = FindByName(userIdentityName).Data;

            string phone = _UserManager.GetPhoneNumberAsync(user).Result;

            if (phone != vM_User.PhoneNumber)
            {
                if (_UserManager.Users.Any(u => u.PhoneNumber == vM_User.PhoneNumber))
                {
                    return new ErrorDataResult<AppUser>(user, "Bu telefon numarası kayıtlıdır");
                };

            }

            if (userPicture != null && userPicture.Length > 0)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserImage", filename);
                using (var stream = new FileStream(path, FileMode.Create))
                {

                    userPicture.CopyToAsync(stream);

                    user.Picture = "Images/UserImage/" + filename;
                }
            }
            user.UserName = vM_User.UserName;
            user.Email = vM_User.Email;
            user.PhoneNumber = vM_User.PhoneNumber;
            user.City = vM_User.City;
            user.BirthDate = vM_User.BirthDate;

            IdentityResult result = _UserManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                _UserManager.UpdateSecurityStampAsync(user);

                 _SignInManager.SignOutAsync();

                 _SignInManager.SignInAsync(user, true);

                return new SuccessDataResult<AppUser>(user,"Bilgileriniz güncellenmiştir.");

            }

            return new  ErrorDataResult<AppUser>("hata var");
        }
    }
}
