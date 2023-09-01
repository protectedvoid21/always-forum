using AlwaysForum.Api.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Database;

public class ForumDbContext : IdentityDbContext<ForumUser, IdentityRole, string>
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }

    public DbSet<Section> Sections { get; set; } = null!;

    public DbSet<Post> Posts { get; set; } = null!;

    public DbSet<Tag> Tags { get; set; } = null!;

    public DbSet<PostTag> PostTags { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;

    public DbSet<CommentVote> CommentUpVotes { get; set; } = null!;

    public DbSet<Reaction> Reactions { get; set; } = null!;

    public DbSet<Message> Messages { get; set; } = null!;

    public DbSet<PostReport> PostReports { get; set; } = null!;

    public DbSet<CommentReport> CommentReports { get; set; } = null!;

    public DbSet<ReportType> ReportTypes { get; set; } = null!;
}