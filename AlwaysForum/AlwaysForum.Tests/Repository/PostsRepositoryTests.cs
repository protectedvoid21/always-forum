using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.Posts;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Tests.Repository; 

public class PostsRepositoryTests {
    private readonly ForumDbContext _dbContext;
    private readonly PostsRepository _repository;
    
    public PostsRepositoryTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _dbContext = new ForumDbContext(options);

        _repository = new PostsRepository(_dbContext);
    }

    [Fact]
    public async Task Get_PostById_ReturnsOnePostWithCertainId() {
        await _repository.AddAsync("Title", "Description", "authorId", 1);

        var postId = (await _dbContext.Posts.FirstAsync()).Id;
        var post = await _repository.GetById(postId);

        Assert.Equal("Title", post.Title);
        Assert.Equal("Description", post.Description);
        Assert.Equal("authorId", post.AuthorId);
        Assert.Equal(1, post.SectionId);
    }

    [Fact]
    public async Task Get_AllPosts_ReturnsAllPostsWithinSection() {
        await _repository.AddAsync("Title1", "Desc1", "1", 1);
        await _repository.AddAsync("Title2", "Desc2", "2", 1);
        await _repository.AddAsync("Title3", "Desc3", "1", 1);
        await _repository.AddAsync("Title4", "Desc4", "1", 2);

        var posts = await _repository.GetBySection(1);

        Assert.Equal(3, posts.Count());
    }

    [Theory]
    [InlineData("SampleTitle", "SampleDescription")]
    [InlineData("LongTitleWithŚĆŚŻ", "")]
    public async Task Add_NewPost_IsVisibleInDatabase(string title, string description) {
        await _repository.AddAsync(title, description, "authorId", 1);

        Assert.Single(_dbContext.Posts);
    }

    [Theory]
    [InlineData("1", "1", true)]
    [InlineData("1", "2", false)]
    [InlineData("111", "112", false)]
    public async Task Check_PostAndAuthor_ReturnInformationIfIsAuthorOfPost(string authorId, string otherAuthorId, bool expected) {
        await _repository.AddAsync("Title", "Desc", authorId, 1);

        var isAuthor = await _repository.IsAuthor(1, otherAuthorId);

        Assert.Equal(isAuthor, expected);
    }

    [Fact]
    public async Task Get_CommentCount_GetCommentCountUnderPost() {
        var post = new Post {
            AuthorId = "1",
            SectionId = 1,
            CreatedDate = DateTime.Now,
            Description = "Desc",
            Title = "Title"
        };
        await _dbContext.AddAsync(post);
        await _dbContext.SaveChangesAsync();

        var postId = (await _dbContext.Posts.FirstAsync()).Id;

        Comment[] comments = {
            new Comment { AuthorId = "2", Description = "Desc", CreatedTime = DateTime.Now, PostId = postId },
            new Comment { AuthorId = "3", Description = "Desc", CreatedTime = DateTime.Now, PostId = postId },
            new Comment { AuthorId = "4", Description = "Desc", CreatedTime = DateTime.Now, PostId = postId },
            new Comment {
                AuthorId = "5", Description = "Desc", CreatedTime = DateTime.Now, PostId = postId - 1 //different post id
            }, 
        };

        await _dbContext.AddRangeAsync(comments);
        await _dbContext.SaveChangesAsync();

        var commentCount = await _repository.GetCommentCount(postId);

        Assert.Equal(3, commentCount);
    }

    [Fact]
    public async Task Update_AddedPost_PostShouldHaveChangedData() {
        Post post = new() {
            Title = "Title",
            Description = "Description",
            AuthorId = "authorId",
            SectionId = 1,
            CreatedDate = DateTime.Now,
        };

        const string newTitle = "NewTitle";
        const string newDescription = "NewDescription";

        await _dbContext.AddAsync(post);
        await _dbContext.SaveChangesAsync();

        var postId = (await _dbContext.Posts.FirstAsync()).Id;
        await _repository.UpdateAsync(postId, newTitle, newDescription);

        var updatedPost = await _dbContext.Posts.FindAsync(postId);

        Assert.Equal(newTitle, updatedPost.Title);
        Assert.Equal(newDescription, updatedPost.Description);
    }

    [Fact]
    public async Task Delete_AddedPost_PostShouldNotBeExisting() {
        var post = new Post {
            Title = "Title",
            Description = "Description",
            AuthorId = "authorId",
            SectionId = 1,
            CreatedDate = DateTime.Now,
        };

        await _dbContext.AddAsync(post);
        await _dbContext.SaveChangesAsync();

        Assert.Single(_dbContext.Posts);

        var postId = (await _dbContext.Posts.FirstAsync()).Id;
        await _repository.DeleteAsync(postId);

        Assert.Empty(_dbContext.Posts);
    }
}