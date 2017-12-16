using System.Collections.Generic;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.Logger.Interfaces;

namespace Faculty.BLL.Services
{
    public class ActionService : IActionService
    {
        private readonly IExceptionUnitOfWork _database;
        private readonly IMapper _mapper;

        public ActionService(IExceptionUnitOfWork database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public IEnumerable<ActionDetailDTO> GetAll()
        {
            var actionDetails = _database.ActionDetailRepository.GetAll();
            var actionDetailsDto = _mapper.Map<List<ActionDetailDTO>>(actionDetails);
            return actionDetailsDto;
        }
    }
}