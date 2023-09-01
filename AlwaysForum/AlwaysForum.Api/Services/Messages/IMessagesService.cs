using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Services.Messages;

public interface IMessagesService
{
    Task<IEnumerable<Message>> GetLastMessages(int messageCount);

    Task<Message> SendAsync(string userId, string text);

    Task DeleteAsync(int id);
}