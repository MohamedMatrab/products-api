using System.Linq.Expressions;
using Products.Domain.Entities.Common;
using Products.Domain.Enums;

namespace Products.Domain.Common;

public static class ExpressionBuilder
{
    public static Expression<Func<T, bool>>? ConstructAndExpressionTree<T>(List<ExpressionFilter> filters)
    {
        if (filters.Count == 0)
            return null;

        var param = Expression.Parameter(typeof(T), "t");
        Expression? exp;

        if (filters.Count == 1)
        {
            exp = GetExpression(param, filters[0]);
        }
        else
        {
            exp = GetExpression(param, filters[0]);
            for (var i = 1; i < filters.Count; i++)
            {
                exp = Expression.Or(exp, GetExpression(param, filters[i]));
            }
        }
        return Expression.Lambda<Func<T, bool>>(exp, param);
    }

    private static Expression GetExpression(ParameterExpression param, ExpressionFilter filter)
    {
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!;
        var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) })!;
        if (filter.PropertyName is null)
            return Expression.Empty();
        
        var member = Expression.Property(param, filter.PropertyName);
        var constant = Expression.Constant(filter.Value);

        return filter.Comparison switch
        {
            Comparison.Equal => Expression.Equal(member, constant),
            Comparison.GreaterThan => Expression.GreaterThan(member, constant),
            Comparison.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
            Comparison.LessThan => Expression.LessThan(member, constant),
            Comparison.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
            Comparison.NotEqual => Expression.NotEqual(member, constant),
            Comparison.Contains => Expression.Call(member, containsMethod, constant),
            Comparison.StartsWith => Expression.Call(member, startsWithMethod, constant),
            Comparison.EndsWith => Expression.Call(member, endsWithMethod, constant),
            _ => Expression.Empty()
        };
    }
    
    public static Expression<Func<T, object>> GetOrderByExpression<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var conversion = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(conversion, parameter);
    }
}