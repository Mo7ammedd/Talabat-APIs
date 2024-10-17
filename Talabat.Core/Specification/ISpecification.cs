﻿using System.Linq.Expressions;
using Talabat.Core.Models;

namespace Talabat.Core.Specification;

public interface ISpecification<T> where T : BaseModel
{

    public Expression<Func<T,bool>> Criteria { get; set; }

    public List<Expression<Func<T,object>>> Includes { get; set; }
}