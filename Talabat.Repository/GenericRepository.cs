using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region With spec
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISepcification<T> Spec)
        {
            return await SpecificationEvaloter<T>.GetQuery(_dbContext.Set<T>(), Spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISepcification<T> Spec)
        {
            return await SpecificationEvaloter<T>.GetQuery(_dbContext.Set<T>(),Spec).FirstOrDefaultAsync();
        }

        public Task<int> GetcountAsync(ISepcification<T> Spec)
        {
            return  SpecificationEvaloter<T>.GetQuery(_dbContext.Set<T>(), Spec).CountAsync();
        }
        #endregion
        #region without Spec
        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //  return  await _dbContext.Set<T>().ToListAsync();
        //}

        //public async Task<T> GetByIdAsync(int id)
        //{
        //    return await _dbContext.Set<T>().FindAsync(id);
        //} 
        #endregion

    }
}
