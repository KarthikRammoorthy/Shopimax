using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopimax.API.Data;
using Shopimax.API.Dtos;

namespace Shopimax.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IShopiMaxRepository _repo;
        public readonly IMapper _mapper;
        public UserController(IShopiMaxRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
     

        [HttpGet]
        public async Task<IActionResult> GetUsers() 
        {
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserToDisplay>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("GetOrdersByUsers")]
        public async Task<IActionResult> GetOrdersByUsers() 
        {
            var users = await _repo.GetOrdersByUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUserByID(id);
            var userToReturn = _mapper.Map<UserToDisplay>(user);
            return Ok(userToReturn);
       
        }

        [HttpDelete("{id}")]
        public  ActionResult DeleteUser( int userID)
        {
            var user =  _repo.GetUserByID(userID);
            _repo.Delete(user);
            return StatusCode(201);
        }

        
    }
}