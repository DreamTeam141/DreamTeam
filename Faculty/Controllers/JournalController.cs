using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.Models;
using Microsoft.AspNet.Identity;

namespace Faculty.Controllers
{
    [ExceptionLogger]
    [ActionLoggerFilter]
    [Authorize(Roles = "teacher")]
    public class JournalController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IJournalService _journalService;
        private readonly ICourseService _courseService;

        public JournalController(IMapper mapper, IJournalService journalService, ICourseService courseService)
        {
            _mapper = mapper;
            _journalService = journalService;
            _courseService = courseService;
        }

        [Authorize(Roles = "teacher")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var isUpdatedCoursesDto = _courseService.GetTeacherIsUpdatedCourses(userId);
            var isUpdatedCoursesViewModel = _mapper.Map<List<CourseViewModel>>(isUpdatedCoursesDto);

            var notUpdatedCoursesDto = _courseService.GetTeacherNotUpdatedCourses(userId);
            var notUpdatedCoursesViewModel = _mapper.Map<List<CourseViewModel>>(notUpdatedCoursesDto);

            var journalViewModel = new JournalIndexViewModel()
            {
                IsUpdatedCoursesViewModels = isUpdatedCoursesViewModel,
                NotUpdatedCourses = notUpdatedCoursesViewModel
            };


            return View(journalViewModel);
        }

        [Authorize(Roles = "teacher")]
        public ActionResult Info(int id, int? set)
        {
            var courseDto = _courseService.GetCourseDtoById(id);

            if (set == null)
            {
                set = courseDto.CurrentSet;
            }

            if (courseDto.Teacher.Id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var courseStudents = courseDto.CourseStudents.Where(x => x.Set == set);
            JournalViewModel journalViewModel = new JournalViewModel()
            {
                CourseId = courseDto.Id,
                CourseStudentViewModels = _mapper.Map<List<CourseStudentViewModel>>(courseStudents),
                StudentSet = courseDto.CurrentSet,
                CheckedSet = set.Value
            };

            journalViewModel.CourseStudentViewModels = journalViewModel.CourseStudentViewModels
                .OrderBy(x => x.Student.ApplicationUser.SecondName).ToList();
            return View(journalViewModel);
        }

        [Authorize(Roles = "teacher")]
        public ActionResult Edit(int id, int? set)
        {
            var courseDto = _courseService.GetCourseDtoById(id);

            if (set == null)
            {
                set = courseDto.CurrentSet;
            }

            var courseStudents = courseDto.CourseStudents.Where(x => x.Set == set);
            var courseStudentsViewModel = _mapper.Map<List<CourseStudentViewModel>>(courseStudents);
            JournalViewModel journalViewModel = new JournalViewModel()
            {
                CourseStudentViewModels = courseStudentsViewModel
            };
            return View(journalViewModel);
        }

        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult Edit(JournalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var courseStudentsDto = _mapper.Map<List<CourseStudentDTO>>(model.CourseStudentViewModels);
            foreach (var item in courseStudentsDto)
            {
                item.Student = null;
                _journalService.UpdateCourseStudent(item);
            }
            return RedirectToAction("Info", new { id = model.CourseStudentViewModels[0].CourseId });
        }

        [Authorize(Roles = "teacher")]
        public ActionResult Delete(string id, int courseid)
        {
            var courseStudentDto = _journalService.GetCourseStudent(id, courseid);
            _journalService.Delete(courseStudentDto);
            return RedirectToAction("Edit", new RouteValueDictionary(new { id = courseid }));
        }


    }
}