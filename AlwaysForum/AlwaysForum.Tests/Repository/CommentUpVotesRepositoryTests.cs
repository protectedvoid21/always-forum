using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.CommentUpVotes;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Tests.Repository;

public class CommentUpVotesRepositoryTests {
    private readonly ForumDbContext _dbContext;
    private readonly CommentVotesRepository _repository;

    public CommentUpVotesRepositoryTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _dbContext = new ForumDbContext(options);

        _repository = new CommentVotesRepository(_dbContext);
    }

    [Fact]
    public async Task Add_NewVote_AddsVoteToDatabaseAndItIsSeenInDb() {
        await _repository.VoteAsync(1, "1", true);

        Assert.Single(_dbContext.CommentUpVotes);
    }

    [Fact]
    public async Task TryAdd_ExistingVote_VoteIsDeletedFromDatabase() {
        await _repository.VoteAsync(1, "1", true);

        await _repository.VoteAsync(1, "1", true);

        Assert.Empty(_dbContext.CommentUpVotes);
    }

    [Fact]
    public async Task TryAdd_ExistingVoteWithOtherType_VoteHasChangedItsIsUpVoteProperty() {
        await _repository.VoteAsync(1, "1", true);

        await _repository.VoteAsync(1, "1", false);

        var vote = await _dbContext.CommentUpVotes.FirstAsync();

        Assert.Single(_dbContext.CommentUpVotes);
        Assert.False(vote.IsUpVote);
    }

    [Fact]
    public async Task Get_VoteCount_ReturnsUpVotesMinusDownVotes() {
        CommentVote[] votes = {
            new() { CommentId = 1, AuthorId = "1", IsUpVote = true },
            new() { CommentId = 1, AuthorId = "2", IsUpVote = true },
            new() { CommentId = 1, AuthorId = "3", IsUpVote = false },
            new() { CommentId = 1, AuthorId = "4", IsUpVote = false },
            new() { CommentId = 1, AuthorId = "5", IsUpVote = true },
            new() { CommentId = 1, AuthorId = "6", IsUpVote = true },
            new() { CommentId = 2, AuthorId = "1", IsUpVote = true },
            new() { CommentId = 2, AuthorId = "2", IsUpVote = true },
        };

        await _dbContext.CommentUpVotes.AddRangeAsync(votes);
        await _dbContext.SaveChangesAsync();

        var voteCount = await _repository.GetVoteCount(1);

        Assert.Equal(2, voteCount);
    }
}