using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Services.CommentReports;
using AlwaysForum.Api.Services.Comments;
using AlwaysForum.Api.Services.CommentUpVotes;
using AlwaysForum.Api.Services.Messages;
using AlwaysForum.Api.Services.PostReports;
using AlwaysForum.Api.Services.Posts;
using AlwaysForum.Api.Services.Reactions;
using AlwaysForum.Api.Services.ReportTypes;
using AlwaysForum.Api.Services.Sections;
using AlwaysForum.Api.Services.Tags;
using AlwaysForum.Api.Services.Users;
using Microsoft.AspNetCore.Identity;

namespace AlwaysForum.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<ForumUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ForumDbContext>();
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTransient<IUsersService, UsersService>()
            .AddTransient<ISectionsService, SectionsService>()
            .AddTransient<IPostsService, PostsService>()
            .AddTransient<IReactionsService, ReactionsService>()
            .AddTransient<ICommentsService, CommentsService>()
            .AddTransient<ICommentVotesService, CommentVotesService>()
            .AddTransient<IMessagesService, MessagesService>()
            .AddTransient<IReportTypesService, ReportTypesService>()
            .AddTransient<IPostReportsService, PostReportsRepository>()
            .AddTransient<ITagsRepository, TagsRepository>()
            .AddTransient<ICommentReportsService, CommentReportsService>();
    }
}