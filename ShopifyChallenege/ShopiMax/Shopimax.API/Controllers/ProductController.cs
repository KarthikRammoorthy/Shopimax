using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopimax.API.Data;
using Shopimax.API.Dtos;
using Shopimax.API.Models;

namespace Shopimax.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: ControllerBase
    {
        public readonly IShopiMaxRepository _repo;
        public readonly IMapper _mapper;
        public ProductController(IShopiMaxRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() 
        {
            var products = await _repo.GetProducts();
            var productsToReturn = _mapper.Map<IEnumerable<ProductDisplayDto>>(products);
            return Ok(productsToReturn);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repo.GetProduct(id);
            var productsToReturn = _mapper.Map<ProductDisplayDto>(product);
            return Ok(productsToReturn);
        }

        [HttpPost("edit")]
        public  async Task<IActionResult> EditProduct( Product product)
        {
            var modifiedProduct = await _repo.EditProduct(product);
            return StatusCode(201);
        }

        [HttpPost("add")]
        public  async Task<IActionResult> AddProduct( Product product)
        {
      
            if (await _repo.ProductExists(product.ProductName, product.ShopID))
                return BadRequest("Product Name already exists");

            _repo.Add(product);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public  ActionResult DeleteProduct( int productID)
        {
            var product =  _repo.GetProduct(productID);
            _repo.Delete(product);
            return StatusCode(201);
        }

      
    }
}