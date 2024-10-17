using Microsoft.EntityFrameworkCore;
using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Repository;

internal static class SpecificationsEvaluator<TModel> where TModel : BaseModel
{
    public static IQueryable<TModel> GetQuery(IQueryable<TModel> inputQuery,ISpecification<TModel> Spec)
    {
        var query = inputQuery; //dbContext.set<Product>

        if (Spec.Criteria is not null) // p => p.Id == id
        {
            query = query.Where(Spec.Criteria);
        }
        
        query = Spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        

        return query;
    }
}