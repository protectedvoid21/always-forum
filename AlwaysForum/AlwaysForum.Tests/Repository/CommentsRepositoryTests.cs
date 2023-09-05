using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.Comments;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Tests.Repository; 

public class CommentsRepositoryTests {
    private readonly ForumDbContext _dbContext;
    private readonly CommentsRepository _repository;

    public CommentsRepositoryTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _dbContext = new ForumDbContext(options);

        _repository = new CommentsRepository(_dbContext);
    }

    [Fact]
    public async Task Add_CommentToPost_ShouldBeVisibleInDatabase() {
        await _repository.AddAsync("Description", 1, "authorId");

        Assert.Single(_dbContext.Comments);
    }

    [Fact]
    public async Task Get_Comments_ReturnsAllCommentsFromOnePost() {
        var user = new ForumUser();
        await _dbContext.AddAsync(user);

        await _repository.AddAsync("Desc1", 1, user.Id);
        await _repository.AddAsync("Desc2", 1, user.Id);
        await _repository.AddAsync("Desc3", 2, user.Id);

        var comments = await _repository.GetForPostAsync(1);

        Assert.Equal(2, comments.Count());
    }

    [Theory]
    [InlineData(true, "simpleId", "simpleId")]
    [InlineData(false, "simpleId", "otherId")]
    [InlineData(false, "12345", "otherId")]
    [InlineData(true, "otherId", "otherId")]
    public async Task Check_Comment_ReturnsTrueIfUserIsAuthorOfComment(bool expected, string authorId, string otherId) {
        var user = new ForumUser {
            Id = authorId
        };
        await _dbContext.AddAsync(user);
        await _repository.AddAsync("Desc", 1, authorId);

        var comment = await _dbContext.Comments.FirstAsync();
        
        Assert.Equal(expected, await _repository.IsAuthorAsync(comment.Id, otherId));
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(7)]
    public async Task Get_CommentCount_ReturnsCommentCountForPost(int commentCount) {
        Random random = new();
        var randomCommentCount = random.Next(2, 5);

        var comments = new Comment[commentCount + randomCommentCount];
        for (var i = 0; i < commentCount; i++) {
            comments[i] = new() {
                Description = $"Desc{i}",
                AuthorId = $"authorId{i}",
                PostId = 1,
            };
        }

        //other post id to make sure it doesn't count every comment
        for (var i = 0; i < randomCommentCount; i++) {
            comments[i + commentCount] = new() {
                Description = $"Desc{i}",
                AuthorId = $"authorId{i}",
                PostId = 2,
            };
        }

        await _dbContext.AddRangeAsync(comments);
        await _dbContext.SaveChangesAsync();

        var commentCountFromService = await _repository.GetCountForPostAsync(1);
        Assert.Equal(commentCount, commentCountFromService);
    }

    [Fact]
    public async Task Update_CertainComment_ChangesShouldBeVisible() {
        await _repository.AddAsync("Desc1", 1, "authorId");

        Assert.Single(_dbContext.Comments);

        var commentId = (await _dbContext.Comments.FirstAsync()).Id;
        await _repository.UpdateAsync(commentId, "UpdatedDesc");

        var updatedComment = await _dbContext.Comments.FirstAsync();
        Assert.Equal("UpdatedDesc", updatedComment.Description);
    }

    [Fact]
    public async Task Delete_Comment_DatabaseShouldHaveNoticeDeletion() {
        await _repository.AddAsync("Desc", 1, "authorId");

        Assert.Single(_dbContext.Comments);

        var commentId = (await _dbContext.Comments.FirstAsync()).Id;
        await _repository.DeleteAsync(commentId);

        Assert.Empty(_dbContext.Comments);
    }
}