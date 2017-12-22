using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Faculty.DAL.Entities;
using Faculty.DAL.Identity;
using Faculty.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Faculty.Controllers
{
    [ExceptionLogger]
    [ActionLoggerFilter]
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Name
                });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    ModelState.AddModelError("", "Something was wrong");
                }
            }
            return View(model);
        }
    }
}