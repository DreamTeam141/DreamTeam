using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.BLL.Util;
using Faculty.DAL.Entities;
using Faculty.DAL.Identity;
using Faculty.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Faculty.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public void Create()
        {
            var student = new Student();
            _database.StudentRepository.Create(student);
            _database.Save();
        }

        public IEnumerable<StudentDTO> GetStudents()
        {
            var students = _database.StudentRepository.GetAll();
            var studentsDto = _mapper.Map<List<StudentDTO>>(students);
            return studentsDto;
        }

        public StudentDTO GetStudentDtoById(string id)
        {
            var student = _database.StudentRepository.GetByStringId(id);
            var studentDto = _mapper.Map<StudentDTO>(student);
            return studentDto;
        }

        public void EditStudent(StudentDTO studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            _database.StudentRepository.Update(student);
            _database.Save();
        }

        public IEnumerable<UserDTO> GetUnblockStudents()
        {
            var role = _database.RoleManager.FindByName(RoleDistributer.GetStudentRole());
            var users = _database.UserManager.Users.Where(x => x.Roles.Select(u => u.RoleId).Contains(role.Id)).ToList()
                .OrderBy(x => x.SecondName).ThenBy(x => x.FirstName).ThenBy(x => x.Patronymic);
            var usersDto = _mapper.Map<List<UserDTO>>(users);
            return usersDto;
        }

        public IEnumerable<UserDTO> GetBlockStudents()
        {
            var role = _database.RoleManager.FindByName(RoleDistributer.GetBlockRole());
            var users = _database.UserManager.Users.Where(x => x.Roles.Select(u => u.RoleId).Contains(role.Id)).ToList()
                .OrderBy(x => x.SecondName).ThenBy(x => x.FirstName).ThenBy(x => x.Patronymic);
            var usersDto = _mapper.Map<List<UserDTO>>(users);
            return usersDto;
        }
    }
}