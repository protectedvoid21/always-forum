using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.CommentReports;
using AlwaysForum.Api.Repositories.Comments;
using AlwaysForum.Api.Repositories.CommentUpVotes;
using AlwaysForum.Api.Repositories.Messages;
using AlwaysForum.Api.Repositories.PostReports;
using AlwaysForum.Api.Repositories.Posts;
using AlwaysForum.Api.Repositories.Reactions;
using AlwaysForum.Api.Repositories.ReportTypes;
using AlwaysForum.Api.Repositories.Sections;
using AlwaysForum.Api.Repositories.Tags;
using AlwaysForum.Api.Repositories.Users;
using AlwaysForum.Api.Services.Posts;
using AlwaysForum.Api.Services.Sections;
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

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTransient<IUsersService, UsersService>()
            .AddTransient<ISectionsRepository, SectionsRepository>()
            .AddTransient<IPostsRepository, PostsRepository>()
            .AddTransient<IReactionsRepository, ReactionsRepository>()
            .AddTransient<ICommentsRepository, CommentsRepository>()
            .AddTransient<ICommentVotesRepository, CommentVotesRepository>()
            .AddTransient<IMessagesRepository, MessagesRepository>()
            .AddTransient<IReportTypesRepository, ReportTypesRepository>()
            .AddTransient<IPostReportsRepository, PostReportsRepository>()
            .AddTransient<ITagsRepository, TagsRepository>()
            .AddTransient<ICommentReportsRepository, CommentReportsRepository>();
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTransient<ISectionsService, SectionsService>()
            .AddTransient<IPostsService, PostsService>();
    }
}