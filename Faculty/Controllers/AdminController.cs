using AutoMapper;
using Faculty.BLL.Interfaces;
using Faculty.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Faculty.Controllers
{
    [ExceptionLogger]
    [ActionLoggerFilter]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        public AdminController(IMapper mapper, IUserService userService, IStudentService studentService, ITeacherService teacherService)
        {
            _mapper = mapper;
            _userService = userService;
            _studentService = studentService;
            _teacherService = teacherService;
        }

        public ActionResult Teachers()
        {
            var usersDto = _teacherService.GetTeachersAsUserDtos();
            var usersViewModel = _mapper.Map<List<UserViewModel>>(usersDto);
            return View(usersViewModel);
        }

        public ActionResult Students()
        {
            var unblockStudentsDto = _studentService.GetUnblockStudents();
            var unblockStudentsViewModel = _mapper.Map<List<UserViewModel>>(unblockStudentsDto);

            var blockStudentsDto = _studentService.GetBlockStudents();
            var blockStudentsViewModel = _mapper.Map<List<UserViewModel>>(blockStudentsDto);

            var model = new BlockUnblockStudentsViewModel()
            {
                UnblockStudents = unblockStudentsViewModel,
                BlockStudents = blockStudentsViewModel
            };
            return View(model);
        }

        public ActionResult Block(string id)
        {
            var userDto = _userService.GetById(id);
            var userViewModel = _mapper.Map<UserViewModel>(userDto);
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Block(UserViewModel model)
        {
            _userService.Block(model.Id);
            return RedirectToAction("Students");
        }

        public ActionResult Unblock(string id)
        {
            var userDto = _userService.GetById(id);
            var userViewModel = _mapper.Map<UserViewModel>(userDto);
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Unblock(UserViewModel model)
        {
            _userService.Unblock(model.Id);
            return RedirectToAction("Students");
        }
    }
}