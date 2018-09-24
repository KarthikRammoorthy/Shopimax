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
    public class ShopsController : ControllerBase
    {
        public readonly IShopiMaxRepository _repo;
        public readonly IMapper _mapper;
        public ShopsController(IShopiMaxRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
     

        [HttpGet]
        public async Task<IActionResult> GetShops() 
        {
            var shops = await _repo.GetShops();
            var shopsToReturn = _mapper.Map<IEnumerable<ShopDto>>(shops);
            return Ok(shopsToReturn);
        }

        [HttpGet("GetProductsByShops")]
        public async Task<IActionResult> GetProductsByShops() 
        {
            var shops = await _repo.GetProductsByShops();
            return Ok(shops);
        }

        [HttpGet("GetOrdersByShops")]
        public async Task<IActionResult> GetOrdersByShops() 
        {
            var shops = await _repo.GetOrdersByShops();
            return Ok(shops);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShop(int id)
        {
            var shop = await _repo.GetShop(id);
            var shopToReturn = _mapper.Map<ShopDto>(shop);
            return Ok(shopToReturn);
       
        }

        [HttpPost("edit")]
        public  async Task<IActionResult> EditShop( Shop shop)
        {
            var modifiedShop = await _repo.EditShop(shop);
            return StatusCode(201);
        }

        [HttpPost("add")]
        public  async Task<IActionResult> Addshop( Shop shop)
        {
      
            if (await _repo.ShopExists(shop.ShopName))
                return BadRequest("Shop Name already exists");

            _repo.Add(shop);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public  ActionResult DeleteShop( int shopID)
        {
            var shop =  _repo.GetShop(shopID);
            _repo.Delete(shop);
            return StatusCode(201);
        }




    }
}