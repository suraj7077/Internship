using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using Section6.dattingapp.API.DTOs;
using Section6.dattingapp.API.Entities;
using Section6.dattingapp.API.Helpers;

namespace Section6.dattingapp.API.interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int  sourceUserId,int likedUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}