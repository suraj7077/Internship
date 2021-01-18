using System.Collections.Generic;
using System.Threading.Tasks;
using API.data;
using API.Entities;
using API.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Section6.dattingapp.API.data;
using Section6.dattingapp.API.DTOs;

namespace API.Controllers
{
    //[Authorize]//section9 imp keep in mind
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;


        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        
        {
            var users = await _userRepository.GetMembersAsync();
            
            return Ok(users);
        }


        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
            
        }
    }
}