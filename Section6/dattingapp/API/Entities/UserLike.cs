using API.Entities;

namespace Section6.dattingapp.API.Entities
{
    public class UserLike
    {
        public AppUser SourceUser {get;set;}
        public int  SourceUserId {get;set;}

        public AppUser LikedUser {get;set;}

        public  int LikedUserID {get;set;}


    }
}