using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Faculty.Controllers
{
    [ExceptionLogger]
    [ActionLoggerFilter]
    public class ErrorController : Controller
    {
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ViewResult InternalError()
        {
            Response.StatusCode = 500;
            return View();
        }

        public ViewResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}