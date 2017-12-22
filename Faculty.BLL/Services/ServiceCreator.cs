using Faculty.BLL.Interfaces;
using Faculty.DAL;

namespace Faculty.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService()
        {
            return new UserService(new UnitOfWork());
        }
    }
}