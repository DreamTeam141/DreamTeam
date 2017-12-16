using System.Web.Mvc;
using Faculty.Logger.EF;
using Faculty.Logger.Entities;
using Microsoft.AspNet.Identity;

namespace Faculty
{
    public class ActionLoggerFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionDetail actionDetail = new ActionDetail()
            {
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                ActionName = filterContext.ActionDescriptor.ActionName,
                UserId = filterContext.HttpContext.User.Identity.GetUserId(),
                UserName = filterContext.HttpContext.User.Identity.Name,
                Date = filterContext.HttpContext.Timestamp
            };

            using (ExceptionDetailContext db = new ExceptionDetailContext())
            {
                db.ActionDetails.Add(actionDetail);
                db.SaveChanges();
            }

            OnActionExecuting(filterContext);
        }
    }
}