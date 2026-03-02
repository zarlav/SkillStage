using SkillStage.Service;
using Microsoft.AspNetCore.Authentication;
using SkillStage.Service.IService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("DefaultScheme")
    .AddCookie("DefaultScheme");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

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