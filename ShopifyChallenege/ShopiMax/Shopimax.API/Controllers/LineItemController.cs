using System;
using System.Collections.Generic;
using System.Linq;
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
    public class LineItemController : ControllerBase
    {
     
        public readonly IShopiMaxRepository _repo;
        public readonly IMapper _mapper;
        public LineItemController(IShopiMaxRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLineItems() 
        {
            var lineItems = await _repo.GetLineItems();
            var lineItemsToReturn = _mapper.Map<IEnumerable<LineItemDisplayDto>>(lineItems);
            return Ok(lineItemsToReturn);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLineItem(int id)
        {
            var lineItem = await _repo.GetLineItem(id);
             var lineItemToReturn = _mapper.Map<LineItemDisplayDto>(lineItem);
            return Ok(lineItemToReturn);
        }

        [HttpPost("edit")]
        public  async Task<IActionResult> EditLineItem( LineItem lineItem)
        {
            var modifiedLineItem = await _repo.EditLineItem(lineItem);
            return StatusCode(201);
        }

        [HttpPost("add")]
        public  ActionResult AddLineItem( LineItem lineItem)
        {
            _repo.Add(lineItem);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public  ActionResult DeleteLineItem( int lineItemID)
        {
            var lineItem =  _repo.GetLineItem(lineItemID);
            _repo.Delete(lineItem);
            return StatusCode(201);
        }


    }
}
