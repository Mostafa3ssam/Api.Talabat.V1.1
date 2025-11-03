using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltrationForCountSpecification :BaseSepcification<Product> 
    {
        public ProductWithFiltrationForCountSpecification(ProductSpecParams param ) :base
            (
             P => (string.IsNullOrEmpty(param.search) || P.Name.ToLower().Contains(param.search))
             &&(!param.BrandId.HasValue || P.ProductBrandId == param.BrandId.Value)
            && (!param.TypeId.HasValue || P.ProductTypeId == param.TypeId.Value)

            )
        {
            
        }
    }
}
