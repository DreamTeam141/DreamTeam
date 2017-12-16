using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Faculty.BLL.Util;
using Faculty.Models;

namespace Faculty.Helpers
{
    public static class CustomRender
    {
        public static MvcHtmlString RenderLayout(this HtmlHelper helper, bool isAuthorize, string role)
        {
            StringBuilder builder = new StringBuilder();
            if (isAuthorize)
            {
                if (role == RoleDistributer.GetAdminRole())
                {
                    builder.Append(helper.Partial("_AdminLayout"));
                }
                else if (role == RoleDistributer.GetStudentRole())
                {
                    builder.Append(helper.Partial("_StudentLayout"));
                }
                else if (role == RoleDistributer.GetTeacherRole())
                {
                    builder.Append(helper.Partial("_TeacherLayout"));
                }
                else if (role == RoleDistributer.GetBlockRole())
                {
                    builder.Append(helper.Partial("_BlockedLayout"));
                }
            }
            else
            {
                builder.Append(helper.Partial("_NotAuthorizeLayout"));
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString RenderStudentProfile(this HtmlHelper helper, ProfileCourseViewModel model)
        {
            StringBuilder builder = new StringBuilder();
            if (model.NotStartedCourses.ToList().Count > 0)
            {
                builder.Append(helper.Partial("_NotStartedCoursesProfile", model.NotStartedCourses));
            }
            if (model.InProcessCourses.ToList().Count > 0)
            {
                builder.Append(helper.Partial("_InProcessCourseProfile", model.InProcessCourses));
            }
            if (model.FinishedCourses.ToList().Count > 0)
            {
                builder.Append(helper.Partial("_FinishedCoursesProfile", model.FinishedCourses));
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString RenderStudAdmin(this HtmlHelper helper, BlockUnblockStudentsViewModel model)
        {
            StringBuilder builder = new StringBuilder();
            if (model.UnblockStudents.ToList().Count > 0)
            {
                builder.Append(helper.Partial("_UnblockedUsers", model.UnblockStudents));
            }
            if (model.BlockStudents.ToList().Count > 0)
            {
                builder.Append(helper.Partial("_BlockedUsers", model.BlockStudents));
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString RenderJournalIndex(this HtmlHelper helper, JournalIndexViewModel model)
        {
            StringBuilder builder = new StringBuilder();
            if (model.NotUpdatedCourses.ToList().Count > 0)
            {
                builder.Append(helper.Partial("_NotUpdatedCourses", model.NotUpdatedCourses));
                builder.Append("<br />");
            }
            return MvcHtmlString.Create(builder.ToString());
        }
    }
}