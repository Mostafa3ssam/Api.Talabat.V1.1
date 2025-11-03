using Api.Talabat.V1.Dtos;
using Api.Talabat.V1.Error;
using Api.Talabat.V1.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.Core.Entity;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;

namespace Api.Talabat.V1.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> ProductRepo ,IMapper mapper)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams param)
        {
            var Spec = new ProductAndTypeSpec(param);
            var products = await _productRepo.GetAllWithSpecAsync(Spec);
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var CountSpec = new ProductWithFiltrationForCountSpecification(param);
            var Count = await _productRepo.GetcountAsync(CountSpec);

            return Ok(new Pagination<ProductToReturnDto>(param.PageIndex , param.PageSize ,Count, Data) );
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetById(int id)
        {
            var Spec = new ProductAndTypeSpec(id);
            var products = await _productRepo.GetByIdWithSpecAsync(Spec);
            if (products == null) return NotFound(new ApiResponse(404));
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(products);
            return Ok(MappedProduct);
        }
    }
}
