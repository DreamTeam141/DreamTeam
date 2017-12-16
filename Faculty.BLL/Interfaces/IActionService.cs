using System.Collections.Generic;
using Faculty.BLL.DTO;

namespace Faculty.BLL.Interfaces
{
    public interface IActionService
    {
        IEnumerable<ActionDetailDTO> GetAll();
    }
}