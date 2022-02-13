using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Abstract;
using Entities.Concrete;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebUrlMonitoring.Models;

namespace WebUrlMonitoring.Controllers
{
    public class MemberController : Controller
    {
        private IIdentityService _IIdentityService;
        public IScheduleService _IScheduleService;
        public MemberController(IIdentityService identityService, IScheduleService scheduleService)
        {
            _IIdentityService = identityService;
            _IScheduleService = scheduleService;

        }
        public void LogOut()
        {
            _IIdentityService.SignOut();

        }
        public IActionResult Index(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return RedirectToAction("login", "Home");
            }

            var vm_user = _IIdentityService.FindByName(User.Identity.Name).Data;


            User user = vm_user.Adapt<User>();



            return View(user);
        }

        public IActionResult ScheduleSetting()
        {
            var UserId = _IIdentityService.FindByName(User.Identity.Name).Data.Id;

           List<ScheduleSettings> scheduleSetting = _IScheduleService.GetByUserId(UserId).Data;

            List<ScheduleSettingViewModel> ScheduleSettingViewModel = scheduleSetting.Adapt<List<ScheduleSettingViewModel>>();

            return View(ScheduleSettingViewModel);
        }
        public IActionResult CreateScheduleSetting()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateScheduleSetting(ScheduleSettingViewModel scheduleSettingViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _IIdentityService.FindByName(User.Identity.Name).Data;

                scheduleSettingViewModel.UserId = user.Id;
                scheduleSettingViewModel.UserEmail = user.Email;
                var result = _IScheduleService.Add(scheduleSettingViewModel);
                if (result.Succsess)
                {
                    return RedirectToAction("ScheduleSetting");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }

            }

            return View(scheduleSettingViewModel);
        }

        public IActionResult UpdateScheduleSetting(int Id)
        {
            ScheduleSettingViewModel scheduleSettingViewModel = _IScheduleService.GetById(Id).Data.Adapt<ScheduleSettingViewModel>();

            return View(scheduleSettingViewModel);
        }
        [HttpPost]
        public IActionResult UpdateScheduleSetting(ScheduleSettingViewModel scheduleSettingViewModel)
        {

            if (ModelState.IsValid)
            {

                ScheduleSettings scheduleSetting = new ScheduleSettings
                {
                    Id = scheduleSettingViewModel.Id,
                    UserId = scheduleSettingViewModel.UserId,
                    UserEmail = scheduleSettingViewModel.UserEmail,
                    Interval = scheduleSettingViewModel.Interval,
                    Name = scheduleSettingViewModel.Name,
                    Url = scheduleSettingViewModel.Url

                };

                var result = _IScheduleService.Update(scheduleSetting);

                if (result.Succsess)
                {
                    TempData["Info"] = result.Message;

                    return RedirectToAction("ScheduleSetting");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }


            }

            return View(scheduleSettingViewModel);
        }

        [HttpPost]

        public IActionResult DeleteScheduleSetting(int id)
        {

            ScheduleSettings scheduleSettings = _IScheduleService.GetById(id).Data;

            if (scheduleSettings != null)
            {
                var result = _IScheduleService.Delete(scheduleSettings);

                if (result.Succsess)
                {
                    TempData["Info"] = result.Message;

                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            return RedirectToAction("ScheduleSetting");
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordChange(ChangePassword changePassword)
        {

            if (ModelState.IsValid)
            {
                AppUser user = _IIdentityService.FindByName(User.Identity.Name).Data;

                var result = _IIdentityService.ChangePassword(changePassword, user);
                if (result.Succsess)
                {
                    TempData["Info"] = result.Message;

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }

            }
            return View(changePassword);
        }

        public IActionResult UserEdit()
        {
            AppUser appUser = _IIdentityService.FindByName(User.Identity.Name).Data;

            User user = appUser.Adapt<User>();
            return View(user);
        }

        [HttpPost]
        public  IActionResult UserEdit(User user, IFormFile userPicture)
        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                string avaibleUserName =  _IIdentityService.FindByName(User.Identity.Name).Data.UserName;

              var result=  _IIdentityService.EditUser(user.Adapt<VM_User>(), userPicture, avaibleUserName);
                if(result.Succsess)
                {
                    TempData["Info"] = result.Message;

                    _IIdentityService.SignOut();
                    _IIdentityService.SignIn(result.Data);
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }

            }

           
            return View(user);


        }
    }
}
