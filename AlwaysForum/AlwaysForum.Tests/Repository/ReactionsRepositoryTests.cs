using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.Reactions;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Tests.Repository; 

public class ReactionsRepositoryTests {
    private readonly ForumDbContext _dbContext;
    private readonly ReactionsRepository _repository;
    
    public ReactionsRepositoryTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _dbContext = new ForumDbContext(options);

        _repository = new ReactionsRepository(_dbContext);
    }

    [Fact]
    public async Task Add_NewReaction_ReactionIsVisibleInDatabase() {
        await _repository.ReactAsync(1, "1", ReactionType.Like);

        Assert.Single(_dbContext.Reactions);
    }

    [Theory]
    [InlineData(ReactionType.Like, ReactionType.Love)]
    [InlineData(ReactionType.Love, ReactionType.Wow)]
    [InlineData(ReactionType.Wow, ReactionType.Sad)]
    [InlineData(ReactionType.Sad, ReactionType.Angry)]
    public async Task Add_NewReactionOverridingExisting_ExistingReactionShouldBeUpdated(ReactionType existingReactionType, ReactionType expectedReactionType) {
        await _repository.ReactAsync(1, "1", existingReactionType);
        Assert.Single(_dbContext.Reactions);

        await _repository.ReactAsync(1, "1", expectedReactionType);
        Reaction reaction = await _dbContext.Reactions.FirstAsync();

        Assert.Equal(expectedReactionType, reaction.ReactionType);
    }

    [Theory]
    [InlineData(ReactionType.Like)]
    [InlineData(ReactionType.Love)]
    [InlineData(ReactionType.Wow)]
    [InlineData(ReactionType.Sad)]
    [InlineData(ReactionType.Angry)]
    public async Task React_TryAddSameReactionWithSameReactionType_DeleteReaction(ReactionType reactionType) {
        await _repository.ReactAsync(1, "1", reactionType);
        Assert.Single(_dbContext.Reactions);

        await _repository.ReactAsync(1, "1", reactionType);
        Assert.Empty(_dbContext.Reactions);
    }

    [Fact]
    public async Task Get_ByPost_ReturnsAllReactionInPost() {
        IEnumerable<Post> posts = new[] {
            new Post { Title = "Title1", Description = "Desc1", AuthorId = "1", CreatedDate = DateTime.Now, SectionId = 1 },
            new Post { Title = "Title2", Description = "Desc2", AuthorId = "1", CreatedDate = DateTime.Now, SectionId = 1 },
        };
        await _dbContext.AddRangeAsync(posts);

        IEnumerable<Reaction> reactions = new[] {
            new Reaction { PostId = 1, UserId = "1", ReactionType = ReactionType.Like },
            new Reaction { PostId = 1, UserId = "2", ReactionType = ReactionType.Sad },
            new Reaction { PostId = 1, UserId = "3", ReactionType = ReactionType.Love },
            new Reaction { PostId = 1, UserId = "4", ReactionType = ReactionType.Angry },
            new Reaction { PostId = 2, UserId = "1", ReactionType = ReactionType.Wow },
            new Reaction { PostId = 2, UserId = "2", ReactionType = ReactionType.Angry },
            new Reaction { PostId = 2, UserId = "3", ReactionType = ReactionType.Like },
        };

        await _dbContext.AddRangeAsync(reactions);
        await _dbContext.SaveChangesAsync();

        var firstPostReactions = await _repository.GetByPost(1);
        var secondPostReactions = await _repository.GetByPost(2);

        Assert.Equal(4, firstPostReactions.Count());
        Assert.Equal(3, secondPostReactions.Count());
    }

    [Fact]
    public async Task Update_ExistingReaction_ShouldChangeItsType() {
        await _dbContext.Reactions.AddAsync(new Reaction {
            PostId = 1,
            UserId = "1",
            ReactionType = ReactionType.Like
        });
        await _dbContext.SaveChangesAsync();

        Reaction reaction = await _dbContext.Reactions.FirstAsync();

        await _repository.UpdateAsync(reaction.Id, ReactionType.Love);

        Assert.Equal(ReactionType.Love, reaction.ReactionType);
    }

    [Fact]
    public async Task Remove_Reaction_IsNotExistingInDatabase() {
        await _dbContext.Reactions.AddAsync(new Reaction {
            PostId = 1,
            UserId = "1",
            ReactionType = ReactionType.Like
        });
        await _dbContext.SaveChangesAsync();

        Assert.Single(_dbContext.Reactions);

        Reaction reaction = await _dbContext.Reactions.FirstAsync();
        await _repository.RemoveAsync(reaction.Id);

        Assert.Empty(_dbContext.Reactions);
    }
}