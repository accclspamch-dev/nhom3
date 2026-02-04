using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SudokuServer.Api.Hubs;
using SudokuServer.Core.Managers;
using SudokuServer.Core.Services;
using SudokuServer.Data;
using GameSudoku1;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddSignalR();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GameSudoku API",
        Version = "v1",
        Description = "API for GameSudoku"
    });
});

// DbContext SSQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=sudoku.db"));

// Core services
builder.Services.AddScoped<ISudokuGenerator, BacktrackingGenerator>();
builder.Services.AddScoped<ISudokuSolver, BacktrackingSolver>();
builder.Services.AddScoped<GameRoomManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameSudoku API v1"));
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapHub<GameHub>("/gamehub");

app.Run();
var allowedOrigins = "_allowedOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

app.UseCors(allowedOrigins);


