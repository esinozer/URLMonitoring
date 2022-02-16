using Core.Entities.Concrete;
using Core.Utilities.Result;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Business.Abstract
{
    public interface IIdentityService
    {
        IDataResult<AppUser> Login(VM_Login vM_Login);
        IDataResult<IdentityResult> SingUp(VM_User vM_User);
        IDataResult<IdentityResult> ChangePassword (VM_ChangePassword VM_ChangePassword , AppUser appUser);
        IDataResult<AppUser> FindByName(string name);
        IDataResult<AppUser> FindById(string UserId);
        IDataResult<AppUser> EditUser(VM_User vM_User, IFormFile userPicture,string  userIdentityName);
        IResult SignOut();
        IResult SignIn(AppUser appUser);

    }
}
