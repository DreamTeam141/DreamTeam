using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Infrastructure;
using Faculty.BLL.Interfaces;
using Faculty.BLL.Util;
using Faculty.DAL.Identity;
using Faculty.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.VisualBasic.ApplicationServices;

namespace Faculty.Controllers
{
    [ExceptionLogger]
    [ActionLoggerFilter]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;

        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }

        private IUserService UserService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IUserService>(); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public AccountController(IMapper mapper, IUserService userService, ICourseService courseService,
            ITeacherService teacherService)
        {
            _mapper = mapper;
            _userService = userService;
            _courseService = courseService;
            _teacherService = teacherService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO {Email = model.Email, Password = model.Password};
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong email or password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Profile(string id)
        {
            string userId;
            if (User.IsInRole(RoleDistributer.GetAdminRole()) && id != null)
            {
                userId = id;
            }
            else
            {
                userId = User.Identity.GetUserId();
            }
            var userDto = _userService.GetById(userId);
            var userViewModel = _mapper.Map<UserViewModel>(userDto);

            if (User.IsInRole(RoleDistributer.GetStudentRole()) || User.IsInRole(RoleDistributer.GetBlockRole())
                || User.IsInRole(RoleDistributer.GetAdminRole())
                && User.Identity.GetUserId() != userId
                && !_teacherService.IsTeacher(userId))
            {
                var notStartedCoursesDto = _courseService.GetNotStartedCourses(userId);
                var notStartedCoursesViewModel = _mapper.Map<List<CourseViewModel>>(notStartedCoursesDto);

                var inProcessCoursesDto = _courseService.GetInProcessCourses(userId);
                var inProcessCoursesViewModel = _mapper.Map<List<CourseViewModel>>(inProcessCoursesDto);

                var finishedCoursesDto = _courseService.GetFinishedCourses(userId);
                var finishedCoursesViewModel = _mapper.Map<List<CourseViewModel>>(finishedCoursesDto);

                var profile = new ProfileCourseViewModel()
                {
                    UserViewModel = userViewModel,
                    NotStartedCourses = notStartedCoursesViewModel,
                    InProcessCourses = inProcessCoursesViewModel,
                    FinishedCourses = finishedCoursesViewModel
                };
                return View("StudentProfile", profile);
            }
            return View(userViewModel);
        }

        [Authorize]
        public FileContentResult UserPhoto(string id)
        {
            string userId;
            if (User.IsInRole(RoleDistributer.GetAdminRole()) && id != null)
            {
                userId = id;
            }
            else
            {
                userId = User.Identity.GetUserId();
            }

            var userDto = _userService.GetById(userId);
            if (userDto.Photo.Length == 0)
            {
                string fileName = HttpContext.Server.MapPath(@"~/Content/Images/noImage.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                return File(imageData, "image/png");
            }
            else
            {
                return new FileContentResult(userDto.Photo, "image/jpeg");
            }
        }
    }
}



