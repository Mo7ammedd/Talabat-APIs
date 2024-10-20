using System.Linq.Expressions;
using Talabat.Core.Models;

namespace Talabat.Core.Specification;

public class BaseSpecifications<T> : ISpecification<T> where T : BaseModel
{
    public Expression<Func<T, bool>> Criteria { get; set; } = null;
    public List<Expression<Func<T, object>>> Includes { get; set; } = new();

    public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
    {
        Criteria = criteriaExpression;
    }

    public BaseSpecifications()
    {
        // Criteria = null;
    }
}