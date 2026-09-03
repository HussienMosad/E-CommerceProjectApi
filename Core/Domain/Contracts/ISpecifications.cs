using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        //Where
        public Expression<Func<TEntity , bool>> Criteria { get; }

        //Include
        public List<Expression<Func<TEntity , object>>> IncludeExpressions { get;  }
    }
}
