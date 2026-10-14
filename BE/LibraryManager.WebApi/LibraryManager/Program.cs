using Librarymanager.Infrastructure.context;
using Librarymanager.Infrastructure.Repositories;
using LibraryManager.ApplicationCore.Automapper;
using LibraryManager.ApplicationCore.Features.Ping;
using LibraryManager.ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//Connection string for the database
builder.Services.AddDbContext<LmDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//MediatR Service
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(PingQuery).Assembly);

});

//AutoMapper Service

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<BookProfile>();
});
//Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
