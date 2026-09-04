using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace persistence
{
    internal static class SpecificationEvaluator
    {

        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery
            ,ISpecifications<TEntity,TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = inputQuery;
            if(specifications.Criteria is not null)
                Query = Query.Where(specifications.Criteria);


            if (specifications.OrderBy is not null)
                Query = Query.OrderBy(specifications.OrderBy);


            if(specifications.OrderByDescending is not null)
                Query = Query.OrderByDescending(specifications.OrderByDescending);



            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count() > 0)
            {
                Query = specifications.IncludeExpressions.Aggregate(Query, (currentquery, expression) => currentquery.Include(expression));
            }

            if (specifications.IsPaginted)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }
    }
}
