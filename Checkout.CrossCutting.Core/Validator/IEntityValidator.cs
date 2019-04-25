using System;
using System.Collections.Generic;

namespace Checkout.CrossCutting.Core.Validator
{
    public interface IEntityValidator
    {
        bool IsValid<TEntity>(TEntity item)
            where TEntity : class;

        ICollection<String> GetInvalidMessages<TEntity>(TEntity item)
            where TEntity : class;
    }
}