using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Services.Specifications
{
    internal abstract class  BaseSpecifications<TEntity, TKey> :
        ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria {  get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

        public void AddIncludes(Expression<Func<TEntity, object>> includeExpressions)
        {
            IncludeExpressions.Add(includeExpressions);
        }
    }
}
