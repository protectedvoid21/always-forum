using AlwaysForum.Api.Database;
using AlwaysForum.Api.Extensions;
using AlwaysForum.Api.Filters;
using AlwaysForum.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResponseBaseFilter>();
});
builder.Services.AddSqlServer<ForumDbContext>(builder.Configuration.GetConnectionString("Main-Db"));
builder.Services.AddIdentity();
builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", config =>
    {
        config
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();