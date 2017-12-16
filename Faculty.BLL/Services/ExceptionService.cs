using System.Collections.Generic;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;
using Faculty.Logger.Entities;
using Faculty.Logger.Interfaces;

namespace Faculty.BLL.Services
{
    public class ExceptionService : IExceptionService
    {
        private readonly IExceptionUnitOfWork _database;
        private readonly IMapper _mapper;

        public ExceptionService(IExceptionUnitOfWork database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public IEnumerable<ExceptionDetailDTO> GetAll()
        {
            var exceptionDetails = _database.ExceptionDetailRepository.GetAll();
            var exceptionDetailsDto = _mapper.Map<List<ExceptionDetailDTO>>(exceptionDetails);
            return exceptionDetailsDto;
        }

        public ExceptionDetailDTO Get(int id)
        {
            var exceptionDetail = _database.ExceptionDetailRepository.Get(id);
            var exceptionDetailDto = _mapper.Map<ExceptionDetailDTO>(exceptionDetail);
            return exceptionDetailDto;
        }
    }
}