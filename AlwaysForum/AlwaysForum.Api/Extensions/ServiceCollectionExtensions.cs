using System.Text;
using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Dtos.Accounts;
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
using AlwaysForum.Api.Services.Accounts;
using AlwaysForum.Api.Services.Comments;
using AlwaysForum.Api.Services.Posts;
using AlwaysForum.Api.Services.Sections;
using AlwaysForum.Api.Services.Token;
using AlwaysForum.Api.Utils.Authentication;
using AlwaysForum.Api.Utils.Settings;
using AlwaysForum.Api.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AlwaysForum.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ForumUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ForumDbContext>();
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value!))
            };
        });
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.AddTransient<ITokenService, TokenService>();
        services.AddCurrentUser();
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IUsersRepository, UsersRepository>()
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

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<ISectionsService, SectionsService>()
            .AddTransient<IPostsService, PostsService>()
            .AddTransient<ICommentsService, CommentsService>()
            .AddTransient<IAccountsService, AccountsService>();
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;

        return services
            .AddTransient<IValidator<CreatePostRequest>, CreatePostRequestValidator>()
            .AddTransient<IValidator<LoginDto>, LoginDtoValidator>()
            .AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
    }
}