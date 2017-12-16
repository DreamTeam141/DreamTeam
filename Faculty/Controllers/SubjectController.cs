using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.Models;

namespace Faculty.Controllers
{
    [ExceptionLogger]
    [ActionLoggerFilter]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var subjectDtos = _subjectService.GetSubjects();
            var subjectsViewModel = _mapper.Map<List<SubjectViewModel>>(subjectDtos);
            return View(subjectsViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(SubjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var subjectDto = _mapper.Map<SubjectDTO>(model);
            _subjectService.Create(subjectDto);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var subjectDto = _subjectService.GetSubjectDtoById(id);
            var subjectViewModel = _mapper.Map<SubjectViewModel>(subjectDto);
            return View(subjectViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(SubjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var subjectDto = _mapper.Map<SubjectDTO>(model);
            _subjectService.EditSubject(subjectDto);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var subjectDto = _subjectService.GetSubjectDtoById(id);
            var subjectViewModel = _mapper.Map<SubjectViewModel>(subjectDto);
            return View(subjectViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(SubjectViewModel model)
        {
            _subjectService.DeleteSubject(model.Id);
            return RedirectToAction("Index");
        }
    }
}