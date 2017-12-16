using System;
using Faculty.Logger.EF;
using Faculty.Logger.Entities;
using Faculty.Logger.Interfaces;
using Faculty.Logger.Repositories;

namespace Faculty.Logger
{
    public class ExceptionUnitOfWork :IExceptionUnitOfWork
    {
        private ExceptionDetailContext _context;
        private IRepository<ExceptionDetail> _exceptionDetailRepository;
        private IRepository<ActionDetail> _actionDetailRepository;

        public IRepository<ExceptionDetail> ExceptionDetailRepository
        {
            get
            {
                if (_exceptionDetailRepository == null)
                {
                    _exceptionDetailRepository = new ExceptionDetailRepository(_context);
                }
                return _exceptionDetailRepository;
            }
        }

        public IRepository<ActionDetail> ActionDetailRepository
        {
            get
            {
                if (_actionDetailRepository == null)
                {
                    _actionDetailRepository = new ActionDetailRepository(_context);
                }
                return _actionDetailRepository;
            }
        }

        public ExceptionUnitOfWork()
        {
            _context = new ExceptionDetailContext();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}