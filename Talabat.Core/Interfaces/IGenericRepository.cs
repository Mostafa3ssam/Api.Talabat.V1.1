using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Specifications;

namespace Talabat.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region MyRegion without Spec
        //Task<IEnumerable<T>> GetAllAsync();
        //Task<T> GetByIdAsync(int id);

        #endregion With Spec
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISepcification<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISepcification<T> Spec);
        Task<int> GetcountAsync(ISepcification<T> Spec);
    }
}
