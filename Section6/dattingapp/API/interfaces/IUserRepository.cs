
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using Section6.dattingapp.API.DTOs;
using Section6.dattingapp.API.Helpers;

namespace API.interfaces
{
    public interface IUserRepository
    {
        void  Update(AppUser  user);

        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUsernameAsync(string username);

        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);

        Task<MemberDto> GetMemberAsync(string username);

        
    }
}