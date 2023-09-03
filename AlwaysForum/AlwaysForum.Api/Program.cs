using AlwaysForum.Api.Database;
using AlwaysForum.Api.Extensions;
using AlwaysForum.Api.Filters;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();