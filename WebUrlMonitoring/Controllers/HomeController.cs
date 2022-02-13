using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUrlMonitoring.Models;

namespace WebUrlMonitoring.Controllers
{
    public class HomeController : Controller
    {

        private IIdentityService _IIdentityService;
        public HomeController(IIdentityService identityService)
        {
            _IIdentityService = identityService;
        }
      
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult LogIn(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();

        }
        [HttpPost]
        public  IActionResult LogIn(Login login)
        {
            if (ModelState.IsValid)
            {
               
                
                var result= _IIdentityService.Login(login);

                if(result.Succsess)
                { 
                    return RedirectToAction("Index", "Member" ,new { result.Data.UserName });
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            return View(login);
        }

        public IActionResult SignUp()
        {
            return View();

        }
        [HttpPost]
        public IActionResult SignUp(User userViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = _IIdentityService.SingUp(userViewModel);

                if(result.Succsess)
                {
                    TempData["Info"] = result.Message;
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var item in result.Data.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(userViewModel);

        }
    }
}
