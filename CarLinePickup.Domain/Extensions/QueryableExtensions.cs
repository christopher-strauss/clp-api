using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CarLinePickup.Domain.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                orderBy = "createdDate OrderByDescending";

            var orderByClause = orderBy.Split(' ');
            var propertyName = orderByClause.FirstOrDefault().Replace(" ", "");
            var commandName = orderByClause.LastOrDefault().ToLower();

            string command = commandName == "desc" ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));

            return (IOrderedQueryable<TEntity>)(source.Provider.CreateQuery<TEntity>(resultExpression));
        }
    }
}
