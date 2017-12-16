using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.BLL.Mapper;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Faculty.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public SubjectService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public void Create(SubjectDTO subjecteDto)
        {
            var subject = _mapper.Map<Subject>(subjecteDto);
            _database.SubjectRepository.Create(subject); 
            _database.Save();
        }

        public IEnumerable<SubjectDTO> GetSubjects()
        {
            var subjects = _database.SubjectRepository.GetAll().Where(x => x.IsDeleted == false).ToList();
            var subjectsDTO = _mapper.Map<List<SubjectDTO>>(subjects);
            return subjectsDTO;
        }

        public SubjectDTO GetSubjectDtoById(int id)
        {
            var subject = _database.SubjectRepository.Get(id);
            var subjectDto = _mapper.Map<SubjectDTO>(subject);
            return subjectDto;
        }

        public void EditSubject(SubjectDTO subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            _database.SubjectRepository.Update(subject);
            _database.Save();
        }

        public void DeleteSubject(int id)
        {
            _database.SubjectRepository.Delete(id);
            _database.Save();
        }

        public IEnumerable<SelectListItem> GetSubjectsAsSelectList()
        {
            var subjects = _database.SubjectRepository.GetAll().Where(x => x.IsDeleted == false).ToList();
            var subjectList = new List<SelectListItem>();
            foreach (var subject in subjects)
            {
                subjectList.Add(new SelectListItem()
                {
                    Value = subject.Id.ToString(),
                    Text = subject.Title
                });
            }
            return subjectList;
        }

        public IEnumerable<SelectListItem> GetSubjectsAsListForFilter()
        {
            var subjects = _database.SubjectRepository.GetAll().Where(x => x.IsDeleted == false).ToList();
            var subjectList = new List<SelectListItem>();

            subjectList.Add(new SelectListItem()
            {
                Value = 0.ToString(),
                Text = "all"
            });

            foreach (var subject in subjects)
            {
                subjectList.Add(new SelectListItem()
                {
                    Value = subject.Id.ToString(),
                    Text = subject.Title
                });
            }
            return subjectList;
        }
    }
}