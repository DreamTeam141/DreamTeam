using System;

namespace Faculty.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Photo { get; set; }
    }
}