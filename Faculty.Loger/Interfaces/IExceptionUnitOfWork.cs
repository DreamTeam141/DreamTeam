using System;
using Faculty.Logger.Entities;

namespace Faculty.Logger.Interfaces
{
    public interface IExceptionUnitOfWork : IDisposable
    {
        IRepository<ExceptionDetail> ExceptionDetailRepository { get; }
        IRepository<ActionDetail> ActionDetailRepository { get; }
        void Save();
    }
}