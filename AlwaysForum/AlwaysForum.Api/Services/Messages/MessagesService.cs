using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Services.Messages;

public class MessagesService : IMessagesService
{
    private readonly ForumDbContext _dbContext;

    public MessagesService(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Message>> GetLastMessages(int messageCount)
    {
        return _dbContext.Messages
            .OrderByDescending(m => m.SendDate)
            .Take(messageCount)
            .OrderBy(m => m.SendDate)
            .Include(m => m.Author);
    }

    public async Task<Message> SendAsync(string userId, string text)
    {
        Message message = new()
        {
            AuthorId = userId,
            Text = text,
            SendDate = DateTime.Now,
            IsDeleted = false
        };
        await _dbContext.Messages.AddAsync(message);
        await _dbContext.SaveChangesAsync();

        return message;
    }

    public async Task DeleteAsync(int id)
    {
        var message = await _dbContext.Messages.FindAsync(id);
        if (message == null)
        {
            return;
        }

        message.IsDeleted = true;
        _dbContext.Update(message);
        await _dbContext.SaveChangesAsync();
    }
}