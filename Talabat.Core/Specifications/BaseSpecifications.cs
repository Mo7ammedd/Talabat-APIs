using System.Linq.Expressions;
using Talabat.Core.Models;

namespace Talabat.Core.Specification;

public class BaseSpecifications<T> : ISpecification<T> where T : BaseModel
{
    public Expression<Func<T, bool>> Criteria { get; set; } = null;
    public List<Expression<Func<T, object>>> Includes { get; set; } = new();
    public Expression<Func<T, object>> OrderBy { get; set; } 
    public Expression<Func<T, object>> OrderByDesc { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
    public bool IsPagingEnabled { get; set; }

    protected BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
    {
        Criteria = criteriaExpression;
    }

    protected BaseSpecifications()
    {
        // Criteria = null;
    }

    public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    public void AddOrderByDesc(Expression<Func<T, object>> orderByExpressionDesc)
    {
        OrderByDesc = orderByExpressionDesc;
    }

    public void ApplyPagination(int skip, int take)
    {
        IsPagingEnabled = true;
        Skip = skip;
        Take = take;
        
    }
}