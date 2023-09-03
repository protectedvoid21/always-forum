using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.Posts;
using FluentAssertions;
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
    public async Task Get_PostById_ReturnsOnePostWithCertainId()
    {
        var title = "Title";
        var description = "Description";
        var authorId = "authorId";
        var sectionId = 1;
        
        await _repository.AddAsync(new Post
        {
            Title = title,
            Description = description,
            AuthorId = authorId,
            SectionId = sectionId,
            CreatedDate = DateTime.Now
        });

        var postId = (await _dbContext.Posts.FirstAsync()).Id;
        var post = await _repository.GetAsync(postId);

        post.Title.Should().Be(title);
        post.Description.Should().Be(description);
        post.AuthorId.Should().Be(authorId);
        post.SectionId.Should().Be(sectionId);
    }

    [Fact]
    public async Task Get_AllPosts_ReturnsAllPostsWithinSection() {
        await _repository.AddAsync(new Post{ Title = "Title1", Description = "Desc1", AuthorId = "1", SectionId = 1, CreatedDate = DateTime.Now });
        await _repository.AddAsync(new Post{ Title = "Title2", Description = "Desc2", AuthorId = "2", SectionId = 1, CreatedDate = DateTime.Now });
        await _repository.AddAsync(new Post{ Title = "Title3", Description = "Desc3", AuthorId = "1", SectionId = 1, CreatedDate = DateTime.Now });
        await _repository.AddAsync(new Post{ Title = "Title4", Description = "Desc4", AuthorId = "1", SectionId = 2, CreatedDate = DateTime.Now });

        var posts = await _repository.GetForSectionAsync(1);

        posts.Count().Should().Be(3);        
    }

    [Theory]
    [InlineData("SampleTitle", "SampleDescription")]
    [InlineData("LongTitleWithŚĆŚŻ", "")]
    public async Task Add_NewPost_IsVisibleInDatabase(string title, string description) {
        await _repository.AddAsync(new Post { Title = title, Description = description, AuthorId = "1", SectionId = 1, CreatedDate = DateTime.Now});

        _dbContext.Posts.Should().ContainSingle();
    }

    [Theory]
    [InlineData("1", "1", true)]
    [InlineData("1", "2", false)]
    [InlineData("111", "112", false)]
    public async Task Check_PostAndAuthor_ReturnInformationIfIsAuthorOfPost(string authorId, string otherAuthorId, bool expected) {
        await _repository.AddAsync(new Post { Title = "Title", Description = "Description", AuthorId = authorId, SectionId = 1, CreatedDate = DateTime.Now});

        var isAuthor = await _repository.IsAuthorAsync(1, otherAuthorId);

        isAuthor.Should().Be(expected);
    }

    [Fact]
    public async Task Update_AddedPost_PostShouldHaveChangedData() {
        var post = new Post {
            Title = "Title",
            Description = "Description",
            AuthorId = "authorId",
            SectionId = 1,
            CreatedDate = DateTime.Now,
        };

        const string newTitle = "NewTitle";
        const string newDescription = "NewDescription";

        _dbContext.Add(post);
        await _dbContext.SaveChangesAsync();
        
        post.Title = newTitle;
        post.Description = newDescription;
        await _repository.UpdateAsync(post);

        var postId = (await _dbContext.Posts.FirstAsync()).Id;
        var updatedPost = await _dbContext.Posts.FindAsync(postId);

        newTitle.Should().Be(updatedPost.Title);
        newDescription.Should().Be(updatedPost.Description);
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

        _dbContext.Add(post);
        await _dbContext.SaveChangesAsync();
        await _repository.DeleteAsync(post);
        await _dbContext.SaveChangesAsync();

        _dbContext.Posts.Should().BeEmpty();
    }
}