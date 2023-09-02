using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.Messages;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Tests.Repository; 

public class MessagesRepositoryTests {
    private readonly ForumDbContext _dbContext;
    private readonly MessagesRepository _repository;
    
    public MessagesRepositoryTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _dbContext = new ForumDbContext(options);

        _repository = new MessagesRepository(_dbContext);
    }

    [Fact]
    public async Task Send_Message_MessageIsSeenInDatabase() {
        await _repository.SendAsync("1", "Sample text");

        var message = await _dbContext.Messages.FirstAsync();

        Assert.Single(_dbContext.Messages);
        Assert.Equal("1", message.AuthorId);
        Assert.Equal("Sample text", message.Text);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(4, 2)]
    [InlineData(5, 3)]
    public async Task Get_Messages_ReturnsSpecifiedCountOfMessages(int wantedMessageCount, int expectedCount) {
        List<Message> messageList = new();

        ForumUser user = new();
        await _dbContext.AddAsync(user);

        for (var i = 0; i < expectedCount; i++) {
            messageList.Add(new Message {
                AuthorId = user.Id,
                SendDate = DateTime.Now,
                Text = i.ToString(),
            });
        }

        await _dbContext.AddRangeAsync(messageList);
        await _dbContext.SaveChangesAsync();

        var messages = await _repository.GetLastMessages(wantedMessageCount);

        Assert.Equal(expectedCount, messages.Count());
    }

    [Fact]
    public async Task Remove_Message_MessageIsMarkedAsDeleted() {
        await _repository.SendAsync("1", "Sample text");

        var message = await _dbContext.Messages.FirstAsync();

        Assert.False(message.IsDeleted);

        await _repository.DeleteAsync(message.Id);

        Assert.True(message.IsDeleted);
    }
}