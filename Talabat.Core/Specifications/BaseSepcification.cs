using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specifications
{
    public class BaseSepcification<T> : ISepcification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Critria { get; }

        public List<Expression<Func<T, object>>> Includes {  get; } = new List<Expression<Func<T, object>>> ();

        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get ; set ; }
        public int Take { get ; set; }
        public bool IsPaginationEnabled { get; set; }

        //get all 
        public BaseSepcification()
        {
            
        }
        //get by id
        public BaseSepcification( Expression<Func<T, bool>> CritriaExpression)
        {
            Critria = CritriaExpression;    
        }
        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression )
        {
            OrderBy = OrderByExpression;
        } public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression )
        {
            OrderByDesc = OrderByDescExpression;
        }

        public void ApplyPagination( int skip , int take )
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        
        }

    }
}
