using System.Data.Entity;
using CheckoutCart.Data.Model.Core.Abstraction;

namespace CheckoutCart.Data.Model.Core.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private members

        private readonly DbContext _context;

        private bool _disposed;
        //private readonly ILogger _logger;

        #endregion

        #region Public members

        #endregion

        #region Public constructor

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public methods

        public GenericRepository<T> RepositoryFactory<T>() where T : class, new()
        {
            return new GenericRepository<T>(_context);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        #region Implementing IDiosposable...   

        /// <summary>
        ///     Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        /// <summary>
        ///     Dispose method
        /// </summary>
        public void Dispose()
        {
            //Nothing needed here as this should be handled by Unity framework
            //Since we are not using Unity, Dispose the context.
            Dispose(true);
        }

        #endregion

        #endregion
    }
}