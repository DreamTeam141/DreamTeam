using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.BLL.Util;
using Faculty.Models;
using Microsoft.AspNet.Identity;

namespace Faculty.Controllers
{
    [ExceptionLogger]
    [ActionLoggerFilter]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISubjectService _subjectService;
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public CourseController(ISubjectService subjectService, ICourseService courseService, ITeacherService teacherService, IMapper mapper)
        {
            _subjectService = subjectService;
            _courseService = courseService;
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult Index(string order = "asc", string teacherSortId = "all", int subjectSortId = 0)
        {
            var coursesDto = _courseService.GetCourses(order, teacherSortId, subjectSortId);
            if (!User.Identity.IsAuthenticated)
            {
                coursesDto = _courseService.GetNotStartedCourses(coursesDto);
            }
            if (User.IsInRole(RoleDistributer.GetStudentRole()))
            {
                coursesDto = _courseService.CheckSubscribe(User.Identity.GetUserId(), coursesDto);
                coursesDto = _courseService.GetNotStartedCourses(coursesDto);
            }
            var coursesViewModel = _mapper.Map<List<CourseViewModel>>(coursesDto);
            SortCoursesPanelViewModel sortCoursesPanelViewModel = new SortCoursesPanelViewModel()
            {
                OrderParams = _courseService.GetParametersAsListForFilter().ToList(),
                SubjectSortParams = _subjectService.GetSubjectsAsListForFilter().ToList(),
                TeacherSortParams = _teacherService.GetTeachersAsListForFilter().ToList()
            };
            var coursesList = new CourseListViewModel()
            {
                Course = new CourseViewModel(),
                Courses = coursesViewModel,
                PanelViewModel = sortCoursesPanelViewModel
            };

            if (User.IsInRole(RoleDistributer.GetAdminRole()))
            {
                return View("AdminIndex", coursesList);
            }
            return View("StudentIndex", coursesList);
        }
        
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            CourseViewModel model = new CourseViewModel
            {
                Subject = _subjectService.GetSubjectsAsSelectList().ToList(),
                Teachers = _teacherService.GetTeachersAsSelectList().ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Photo")]CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Subject = _subjectService.GetSubjectsAsSelectList().ToList();
                model.Teachers = _teacherService.GetTeachersAsSelectList().ToList();
                return View(model);
            }
            byte[] imageData = null;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase poImgFile = Request.Files["Photo"];

                using (var binary = new BinaryReader(poImgFile.InputStream))
                {
                    imageData = binary.ReadBytes(poImgFile.ContentLength);
                }
            }

            var courseDto = _mapper.Map<CourseDTO>(model);
            courseDto.Photo = imageData;
            _courseService.Create(courseDto);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var courseDto = _courseService.GetCourseDtoById(id);
            var courseViewModel = _mapper.Map<CourseViewModel>(courseDto);
            courseViewModel.CourseStatus = _courseService.GetCourseStatusAsSelectList().ToList();
            return View(courseViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Photo")]CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            byte[] imageData = null;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase poImgFile = Request.Files["Photo"];

                using (var binary = new BinaryReader(poImgFile.InputStream))
                {
                    imageData = binary.ReadBytes(poImgFile.ContentLength);
                }
            }
            model.Photo = imageData;
            var courseDto = _mapper.Map<CourseDTO>(model);
            _courseService.EditCourse(courseDto);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var courseDto = _courseService.GetCourseDtoById(id);
            var courseDetailsViewModel = _mapper.Map<CourseDetailsViewModel>(courseDto);
            courseDetailsViewModel.CourseStatus = _courseService.GetCourseStatus(id);
            return View(courseDetailsViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var courseDto = _courseService.GetCourseDtoById(id);
            var courseDetailsViewModel = _mapper.Map<CourseDetailsViewModel>(courseDto);
            courseDetailsViewModel.CourseStatus = _courseService.GetCourseStatus(id);
            return View(courseDetailsViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(CourseDetailsViewModel model)
        {
            _courseService.DeleteCourse(model.Id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ChangeCourseStatus(int id)
        {
            _courseService.ChangeCourseStatus(id);
            return RedirectToAction("Edit", new {id = id});
        }

        [Authorize(Roles = "student")]
        public ActionResult Subscribe(int id)
        {
            var courseDto = _courseService.GetCourseDtoById(id);
            var courseDetailsViewModel = _mapper.Map<CourseDetailsViewModel>(courseDto);
            courseDetailsViewModel.CourseStatus = _courseService.GetCourseStatus(id);
            return View(courseDetailsViewModel);
        }

        [Authorize(Roles = "student")]
        [HttpPost]
        public ActionResult Subscribe(CourseDetailsViewModel model)
        {
            var userId = User.Identity.GetUserId();
            _courseService.Subscribe(userId, model.Id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public FileContentResult CoursePhoto(int id)
        {
            var courseDto = _courseService.GetCourseDtoById(id);
            if (courseDto.Photo.Length == 0)
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
                return new FileContentResult(courseDto.Photo, "image/jpeg");
            }
        }
    }
}