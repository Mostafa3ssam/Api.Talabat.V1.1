using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specifications
{
    public class ProductAndTypeSpec:BaseSepcification<Product>
    {
        public ProductAndTypeSpec(ProductSpecParams param) :base(

            P=>
             (string.IsNullOrEmpty(param.search) || P.Name.ToLower().Contains(param.search))
             && ( !param.BrandId.HasValue ||  P.ProductBrandId == param.BrandId.Value)
            
            && (!param.TypeId.HasValue || P.ProductTypeId == param.TypeId.Value)
            
            )
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
            if (!string.IsNullOrEmpty(param.Sort))
            {
                switch (param.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                        
                }


            }
            ApplyPagination(param.PageSize * (param.PageIndex - 1), param.PageSize);

        }
        public ProductAndTypeSpec(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }


    }
}
