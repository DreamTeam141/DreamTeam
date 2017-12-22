using System;
using System.Web.Mvc;
using Faculty.Logger.EF;
using Faculty.Logger.Entities;

namespace Faculty
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            ExceptionDetail exceptionDetail = new ExceptionDetail()
            {
                ExceptionMessage = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now
            };
            using (ExceptionDetailContext db = new ExceptionDetailContext())
            {
                db.ExceptionDetails.Add(exceptionDetail);
                db.SaveChanges();
            }
            //filterContext.ExceptionHandled = true;
        }
    }
}