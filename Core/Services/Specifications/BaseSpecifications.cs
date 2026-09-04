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
        #region Criteria [Where]
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
       
        public Expression<Func<TEntity, bool>>? Criteria {  get; private set; }
        #endregion

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

        protected void AddIncludes(Expression<Func<TEntity, object>> includeExpressions)
        {
            IncludeExpressions.Add(includeExpressions);
        }
        #endregion

        #region Sorting [OrderBy - OrderBy Descending]
        public Expression<Func<TEntity, object>>? OrderBy {  get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        public Expression<Func<TEntity, object>>? OrderByDescending {  get; private set; }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDescendingExpression)
        {
           OrderByDescending = OrderByDescendingExpression;
        }

        #endregion

        #region Pagination [Skip - Take]
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginted {  get; private set; }

        protected void ApplyingPagination(int PageSize , int PageIndex)
        {
            // 10 2
            IsPaginted = true;
            Take = PageSize; // The Second 10 Products
            Skip = (PageIndex - 1) * PageSize; // (2-1) * 10 = Skip The First 10 Products
        }
        #endregion

    }
}
