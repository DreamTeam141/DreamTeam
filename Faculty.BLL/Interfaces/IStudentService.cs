using System.Collections.Generic;
using System.Web.Mvc;
using Faculty.BLL.DTO;

namespace Faculty.BLL.Interfaces
{
    public interface IStudentService
    {
        void Create();
        IEnumerable<StudentDTO> GetStudents();
        StudentDTO GetStudentDtoById(string id);
        void EditStudent(StudentDTO studentDto);
        IEnumerable<UserDTO> GetUnblockStudents();
        IEnumerable<UserDTO> GetBlockStudents();
    }
}