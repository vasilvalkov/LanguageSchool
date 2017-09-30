using AutoMapper.QueryableExtensions;
using LanguageSchoolApp.App_Start;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LanguageSchoolApp.Infrastructure
{
    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> MapTo<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}
