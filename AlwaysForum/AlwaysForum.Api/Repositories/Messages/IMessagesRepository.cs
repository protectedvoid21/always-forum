using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.Messages;

public interface IMessagesRepository
{
    Task<IEnumerable<Message>> GetLastMessages(int messageCount);

    Task<Message> SendAsync(string userId, string text);

    Task DeleteAsync(int id);
}