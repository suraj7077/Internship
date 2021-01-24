using System.Collections.Generic;
using System.Threading.Tasks;
using Section6.dattingapp.API.DTOs;
using Section6.dattingapp.API.Entities;
using Section6.dattingapp.API.Helpers;

namespace Section6.dattingapp.API.interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername,string recipientUsername);
        Task<bool> SaveAllAsync();
    }
}