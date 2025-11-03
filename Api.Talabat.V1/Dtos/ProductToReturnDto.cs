using Talabat.Core.Entity;

namespace Api.Talabat.V1.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        #region Fk
        public int? ProductBrandId { get; set; }
        public int? ProductTypeId { get; set; }
        #endregion


        #region Navication Prop
        public String ProductBrand { get; set; }
        public string ProductType { get; set; }
        #endregion
    }
}
