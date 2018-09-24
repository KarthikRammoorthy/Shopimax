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
    public class OrderController: ControllerBase
    {
        public readonly IShopiMaxRepository _repo;
        public readonly IMapper _mapper;
        public OrderController(IShopiMaxRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders() 
        {
            var orders = await _repo.GetOrders();
            var ordersToReturn = _mapper.Map<IEnumerable<OrderDisplayDto>>(orders);
            return Ok(ordersToReturn);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var orders = await _repo.GetOrder(id);
            var ordersToReturn = _mapper.Map<OrderDisplayDto>(orders);
            return Ok(ordersToReturn);
        }

        [HttpGet("GetLineItemsbyOrders")]
        public async Task<IActionResult> GetLineItemsbyOrders() 
        {
            var orders = await _repo.GetLineItemsbyOrders();
            return Ok(orders);
        }

        [HttpPost("edit")]
        public  async Task<IActionResult> EditOrder( Order order)
        {
            var modifiedOrder = await _repo.EditOrder(order);
            return StatusCode(201);
        }

        [HttpPost("add")]
        public  ActionResult AddOrder( Order order)
        {
            _repo.Add(order);
            return StatusCode(201);
        }
        
        [HttpDelete("{id}")]
        public  ActionResult DeleteOrder( int orderID)
        {
            var order =  _repo.GetOrder(orderID);
            _repo.Delete(order);
            return StatusCode(201);
        }
    }
}