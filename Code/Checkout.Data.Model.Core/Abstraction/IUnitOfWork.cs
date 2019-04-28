using System;
using CheckoutCart.Data.Model.Core.Implementation;

namespace CheckoutCart.Data.Model.Core.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<T> RepositoryFactory<T>() where T : class, new();
        int Save();

     

    }
}
