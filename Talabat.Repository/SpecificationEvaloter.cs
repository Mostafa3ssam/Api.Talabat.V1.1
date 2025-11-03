using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaloter<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISepcification<T> Spec)
        {

            var Query = InputQuery;
            if (Spec.Critria is not null)
            {
                Query = Query.Where(Spec.Critria);
            }
            if (Spec.OrderBy is not null)
            {
                Query = Query.OrderBy(Spec.OrderBy);
            }
            if (Spec.OrderByDesc is not null) 
            {
                Query =  Query.OrderByDescending(Spec.OrderByDesc);
            }
              
            if (Spec.IsPaginationEnabled)
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }
            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeEsperssion) => CurrentQuery.Include(IncludeEsperssion));

            return Query;
        }

    }
}
