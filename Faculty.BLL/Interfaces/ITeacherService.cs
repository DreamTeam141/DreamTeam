using System.Web.Mvc;
using Faculty.BLL.DTO;
using System.Collections.Generic;

namespace Faculty.BLL.Interfaces
{
    public interface ITeacherService
    {
        void Create();
        IEnumerable<TeacherDTO> GetTeachers();
        TeacherDTO GetTeacherDtoById(string id);
        IEnumerable<SelectListItem> GetTeachersAsSelectList();
        void EditTeacher(TeacherDTO teacherDto);
        IEnumerable<SelectListItem> GetTeachersAsListForFilter();
        IEnumerable<UserDTO> GetTeachersAsUserDtos();
        bool IsTeacher(string id);
    }
}