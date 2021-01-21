using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.data;
using API.Entities;
using API.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Section6.dattingapp.API.data;
using Section6.dattingapp.API.DTOs;
using Section6.dattingapp.API.Extensions;
using Section6.dattingapp.API.Helpers;
using Section6.dattingapp.API.interfaces;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;


        }

        [HttpGet()]
        //[AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            var user= await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            userParams.CurrentUsername=user.UserName;
            if(string.IsNullOrEmpty(userParams.Gender))
                userParams.Gender=user.Gender =="male" ? "female":"male";
            var users = await _userRepository.GetMembersAsync(userParams);
            Response.AddPagiantionHeader(users.CurrentPage,users.PageSize,
                                        users.TotalCount,users.TotalPages);

            return Ok(users);
        }

        //[Authorize]
        [HttpGet("{username}",Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);

        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {

            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            _mapper.Map(memberUpdateDto, user);
            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update");
        }

        [Authorize]
        [HttpPost("add-photo")]

        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var result = await _photoService.AddPhotoAsync(file);
            if(result.Error != null) return  BadRequest(result.Error.Message); 
            var photo =new Photo
            {
                Url=result.SecureUrl.AbsoluteUri,
                PublicId=result.PublicId
            };
            if(user.Photos.Count==0)
            {
                photo.IsMain=true;
            }
            user.Photos.Add(photo);
            if(await _userRepository.SaveAllAsync())
            {
                //return _mapper.Map<PhotoDto>(photo);
                return  CreatedAtRoute("GetUser",new{username=user.UserName},_mapper.Map<PhotoDto>(photo));
            }
             
            return BadRequest("Problem ADDING photo");
        }

        [Authorize]
        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult>  SetMainPhoto(int photoId)
        {
            var user=await  _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var photo=user.Photos.FirstOrDefault( x  =>x.Id == photoId);
            if(photo.IsMain) return BadRequest("this is already your main photo");

            var currentMain=user.Photos.FirstOrDefault(x => x.IsMain);
            if(currentMain !=null) currentMain.IsMain =false;
            photo.IsMain=true;

            if(await _userRepository.SaveAllAsync())  return NoContent();
            return BadRequest("Failed to set main photo");
        }

        [Authorize]
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user= await  _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var photo =user.Photos.FirstOrDefault(x =>x.Id == photoId);
            if(photo ==null) return NotFound();
            if(photo.IsMain) return BadRequest("You can not delete your main photo");
            if(photo.PublicId != null)
            {
                var result=await _photoService.DeletePhotoAsync(photo.PublicId);
                if(result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);
            if(await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete ");
        }
    }
}