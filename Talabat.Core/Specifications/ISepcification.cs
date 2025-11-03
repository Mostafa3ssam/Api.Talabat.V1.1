using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specifications
{
    public interface ISepcification<T> where T : BaseEntity
    {
        Expression<Func<T,bool>> Critria { get; }
        List<Expression<Func<T,object>>> Includes { get; }
        Expression<Func<T ,object>> OrderBy { get; }
        Expression<Func<T ,object>> OrderByDesc { get; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }


    }
}
