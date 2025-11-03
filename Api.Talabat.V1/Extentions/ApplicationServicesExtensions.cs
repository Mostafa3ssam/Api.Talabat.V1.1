using Api.Talabat.V1.Helper;
using Talabat.Core.Interfaces;
using Talabat.Repository.Data;
using Talabat.Repository;

namespace Api.Talabat.V1.Extentions
{
    public static class ApplicationServicesExtensions
    {
        public  static IServiceCollection AddApplicationServices(this  IServiceCollection Services)
        {

         
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            Services.AddAutoMapper(typeof(MappingProfiles));
            return Services;
        }
    }
}
