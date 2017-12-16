using System.Collections.Generic;
using Faculty.BLL.DTO;

namespace Faculty.BLL.Interfaces
{
    public interface IExceptionService
    {
        IEnumerable<ExceptionDetailDTO> GetAll();
        ExceptionDetailDTO Get(int id);
    }
}