﻿using System.Linq.Expressions;

namespace Agc.GoodShepherd.Domain.Interfaces
{
    public interface ISpecifications<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        public bool IsPagingEnabled { get; }
    }
}
