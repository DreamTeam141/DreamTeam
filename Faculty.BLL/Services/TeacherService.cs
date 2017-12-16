using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.BLL.Util;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Faculty.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public TeacherService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public void Create()
        {
            var teacher = new Teacher();
            _database.TeacherRepository.Create(teacher);
            _database.Save();
        }

        public IEnumerable<TeacherDTO> GetTeachers()
        {
            var teachers = _database.TeacherRepository.GetAll();
            var teachersDto = _mapper.Map<List<TeacherDTO>>(teachers);
            return teachersDto;
        }

        public TeacherDTO GetTeacherDtoById(string id)
        {
            var teacher = _database.TeacherRepository.GetByStringId(id);
            var teacherDto = _mapper.Map<TeacherDTO>(teacher);
            return teacherDto;
        }

        public IEnumerable<SelectListItem> GetTeachersAsSelectList()
        {
            var teachers = _database.TeacherRepository.GetAll();
            var teachersList = new List<SelectListItem>();
            foreach (var teacher in teachers)
            {
                teachersList.Add(new SelectListItem()
                {
                    Value = teacher.Id,
                    Text = teacher.ApplicationUser.SecondName + " " + teacher.ApplicationUser.FirstName + " " + teacher.ApplicationUser.Patronymic
                });
            }
            return teachersList;
        }

        public void EditTeacher(TeacherDTO teacherDto)
        {
            var teacher = _mapper.Map<Teacher>(teacherDto);
            _database.TeacherRepository.Update(teacher);
            _database.Save();
        }

        public IEnumerable<SelectListItem> GetTeachersAsListForFilter()
        {
            var teachers = _database.TeacherRepository.GetAll();
            var teachersList = new List<SelectListItem>();
            teachersList.Add(new SelectListItem()
            {
                Value = "all",
                Text = "all"
            });
            foreach (var teacher in teachers)
            {
                teachersList.Add(new SelectListItem()
                {
                    Value = teacher.Id,
                    Text = teacher.ApplicationUser.SecondName + " " + teacher.ApplicationUser.FirstName + " " + teacher.ApplicationUser.Patronymic
                });
            }
            return teachersList;
        }

        public IEnumerable<UserDTO> GetTeachersAsUserDtos()
        {
            var role = _database.RoleManager.FindByName(RoleDistributer.GetTeacherRole());
            var users = _database.UserManager.Users.Where(x => x.Roles.Select(u => u.RoleId).Contains(role.Id)).ToList()
                .OrderBy(x => x.SecondName).ThenBy(x => x.FirstName).ThenBy(x => x.Patronymic);
            var usersDto = _mapper.Map<List<UserDTO>>(users);
            return usersDto;
        }

        public bool IsTeacher(string id)
        {
            var user = _database.UserManager.FindById(id);
            var role = _database.RoleManager.FindByName(RoleDistributer.GetTeacherRole());
            if (user.Roles.First().RoleId == role.Id)
            {
                return true;
            }
            return false;
        }
    }
}