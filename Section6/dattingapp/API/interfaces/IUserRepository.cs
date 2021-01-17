
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using Section6.dattingapp.API.DTOs;

namespace API.interfaces
{
    public interface IUserRepository
    {
        void  Update(AppUser  user);

        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUsernameAsync(string username);

        Task<IEnumerable<MemberDto>> GetMembersAsync();

        Task<MemberDto> GetMemberAsync(string username);

        
    }
}