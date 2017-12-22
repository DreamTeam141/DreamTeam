using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Faculty.BLL.Interfaces;
using Faculty.Models;

namespace Faculty.Controllers
{
    [ExceptionLogger]
    [ActionLoggerFilter]
    [Authorize(Roles = "admin")]
    public class LoggerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IExceptionService _exceptionService;
        private readonly IActionService _actionService;

        public LoggerController(IMapper mapper, IExceptionService exceptionService, IActionService actionService)
        {
            _mapper = mapper;
            _exceptionService = exceptionService;
            _actionService = actionService;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Exceptions()
        {
            var exceptionDetailsDto = _exceptionService.GetAll();
            var exceptionDetailsViewModel = _mapper.Map<List<ExceptionDetailViewModel>>(exceptionDetailsDto);
            return View(exceptionDetailsViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            var exceptionDetailDto = _exceptionService.Get(id);
            var exceptionDetailViewModel = _mapper.Map<ExceptionDetailViewModel>(exceptionDetailDto);
            return View(exceptionDetailViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Actions()
        {
            var actionDetailsDto = _actionService.GetAll();
            var actionDetailsViewModel = _mapper.Map<List<ActionDetailViewModel>>(actionDetailsDto);
            return View(actionDetailsViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Exception()
        {
            throw new Exception("Tese Exception");
        }
    }
}