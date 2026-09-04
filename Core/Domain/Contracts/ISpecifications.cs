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
        public Expression<Func<TEntity , bool>>? Criteria { get; }

        //Include
        public List<Expression<Func<TEntity , object>>> IncludeExpressions { get;  }

        // Order BY  Asc - Desc
        public Expression<Func<TEntity,object>>? OrderBy { get;}
        public Expression<Func<TEntity, object>>? OrderByDescending { get; }

        //Pagintion [Skip - Take] ints

        public int Skip { get; }

        public int Take { get;  }

        public bool IsPaginted { get; }
    }
}
