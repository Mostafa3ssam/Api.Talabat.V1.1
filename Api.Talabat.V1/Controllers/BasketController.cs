using Api.Talabat.V1.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entity;
using Talabat.Core.Interfaces;

namespace Api.Talabat.V1.Controllers
{

    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository )
        {
            _basketRepository = basketRepository;
        }

        //Get or ReCreate
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string BasketId)
        {

            var Basket = await _basketRepository.GetCustomerBasketAsync(BasketId);
            if (Basket is null) return new CustomerBasket(BasketId);
            return Ok(Basket);
        }


        //Create new basket or Update
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasket basket)
        {
            var CreateOrUpdateBasket = await _basketRepository.UpdateBasketAsync(basket);
            if (CreateOrUpdateBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(CreateOrUpdateBasket);

        }
        //Delete 
        [HttpDelete]
        public async Task<ActionResult <bool>> DeleteBasket(string BasketId)
        {
            return await _basketRepository.DeleteBasketAsync(BasketId);
        }
    }
}
