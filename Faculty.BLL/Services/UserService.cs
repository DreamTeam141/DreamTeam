using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Infrastructure;
using Faculty.BLL.Interfaces;
using Faculty.BLL.Util;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Faculty.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow)
        {
            _database = uow;
        }
        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public OperationDetails CreateStudent(UserDTO userDto)
        {
            var userManager = _database.UserManager;
            ApplicationUser user = userManager.FindByEmail(userDto.Email);

            if (user != null)
            {
                return new OperationDetails(false, "User with such an email already exists", "Email");
            }

            var userwithSameNumber = userManager.Users.Where(x => x.PhoneNumber == userDto.PhoneNumber).ToList();
            if (userwithSameNumber.Count > 0)
            {
                return new OperationDetails(false, "User with such phone number already exists", "PhoneNumber");
            }

            user = new ApplicationUser
            {
                Email = userDto.Email,
                UserName = userDto.Email,
                FirstName = userDto.FirstName,
                SecondName = userDto.SecondName,
                Patronymic = userDto.Patronymic,
                PhoneNumber = userDto.PhoneNumber,
                DateOfBirth = userDto.DateOfBirth,
                Photo = userDto.Photo
            };
            var result = _database.UserManager.Create(user, userDto.Password);
            if (result.Errors.Count() > 0)
            {
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
            }
            _database.UserManager.AddToRole(user.Id, RoleDistributer.GetStudentRole());
            _database.Save();
            _database.StudentRepository.Create(new Student() { ApplicationUser = user });
            _database.Save();
            return new OperationDetails(true, "", "");
        }

        public OperationDetails CreateTeacher(UserDTO userDto)
        {
            var userManager = _database.UserManager;
            ApplicationUser user = userManager.FindByEmail(userDto.Email);

            if (user != null)
            {
                return new OperationDetails(false, "User with such an email already exists", "Email");
            }

            user = new ApplicationUser
            {
                Email = userDto.Email,
                UserName = userDto.Email,
                FirstName = userDto.FirstName,
                SecondName = userDto.SecondName,
                Patronymic = userDto.Patronymic,
                PhoneNumber = userDto.PhoneNumber,
                DateOfBirth = userDto.DateOfBirth,
                Photo = userDto.Photo
            };
            var result = _database.UserManager.Create(user, userDto.Password);
            if (result.Errors.Count() > 0)
            {
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
            }
            _database.UserManager.AddToRole(user.Id, RoleDistributer.GetTeacherRole());
            _database.Save();
            _database.TeacherRepository.Create(new Teacher() { ApplicationUser = user });
            _database.Save();
            return new OperationDetails(true, "", "");
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await _database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
            {
                claim = await _database.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                claim.AddClaim(new Claim("FullName", user.FirstName + " " + user.Patronymic));
                claim.AddClaim(new Claim("UserRole", GetUserRole(user.Id)));
            }

            return claim;
        }

        public UserDTO GetById(string id)
        {
            var user = _database.UserManager.FindById(id);
            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public void Update(UserDTO userDto)
        {
            var user = _database.UserManager.FindById(userDto.Id);
            _mapper.Map(userDto, user);
            _database.UserManager.Update(user);
        }

        public void Block(string userId)
        {
            _database.UserManager.RemoveFromRole(userId, RoleDistributer.GetStudentRole());
            _database.UserManager.AddToRole(userId, RoleDistributer.GetBlockRole());
            _database.Save();
        }

        public void Unblock(string userId)
        {
            _database.UserManager.RemoveFromRole(userId, RoleDistributer.GetBlockRole());
            _database.UserManager.AddToRole(userId, RoleDistributer.GetStudentRole());
            _database.Save();
        }

        public string GetUserRole(string id)
        {
            return _database.UserManager.GetRoles(id).First();

        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = _database.UserManager.Users.ToList();
            var usersDto = _mapper.Map<List<UserDTO>>(users);
            return usersDto;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}