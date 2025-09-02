using Exame.Application.Services;
using Exame.Domain.Entities;
using Exame.Domain.Interfaces;
using Exame.Infrastructure.Data;
using Exame.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ExameDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ExameDb"));
});

// DI
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMovimentoManualRepository, MovimentoManualRepository>();
builder.Services.AddScoped<IMovimentoManualService, MovimentoManualService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
