using System.IO;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Infrastructure;
using Faculty.BLL.Interfaces;
using Faculty.Models;
using Microsoft.AspNet.Identity.Owin;

namespace Faculty.Controllers
{
    [ExceptionLogger]
    [ActionLoggerFilter]
    public class TeacherController : Controller
    {
        private readonly IMapper _mapper;


        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public TeacherController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Register([Bind(Exclude = "Photo")]RegisterViewModel model)
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
            Mapper.Initialize(cfg => cfg.CreateMap<RegisterViewModel, UserDTO>());
            var userDTO = Mapper.Map<UserDTO>(model);
            OperationDetails operationDetails = UserService.CreateTeacher(userDTO);
            if (operationDetails.Succedeed)
            {
                return RedirectToAction("Teachers", "Admin");
            }
            else
            {
                ModelState.AddModelError("", operationDetails.Message);
                return View(model);
            }
        }
    }
}