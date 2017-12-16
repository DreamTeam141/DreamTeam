using System.Collections.Generic;
using System.Web.Mvc;
using Faculty.BLL.DTO;

namespace Faculty.BLL.Interfaces
{
    public interface ISubjectService
    {
        void Create(SubjectDTO subjectDto);
        IEnumerable<SubjectDTO> GetSubjects();
        SubjectDTO GetSubjectDtoById(int id);
        IEnumerable<SelectListItem> GetSubjectsAsSelectList();
        void EditSubject(SubjectDTO subjectDto);
        void DeleteSubject(int id);
        IEnumerable<SelectListItem> GetSubjectsAsListForFilter();
    }
}