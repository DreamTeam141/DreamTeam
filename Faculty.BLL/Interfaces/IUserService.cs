using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.BLL.DTO;
using Faculty.BLL.Infrastructure;

namespace Faculty.BLL.Interfaces
{
    public interface IUserService :IDisposable
    {
        OperationDetails CreateStudent(UserDTO userDto);
        OperationDetails CreateTeacher(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        UserDTO GetById(string id);
        void Update(UserDTO userDto);
        void Block(string userId);
        void Unblock(string userId);
        string GetUserRole(string id);
        IEnumerable<UserDTO> GetAll();
    }
}