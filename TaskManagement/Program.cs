using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManagement.Core.Interfaces.Repositories;
using TaskManagement.Core.Interfaces.Services;
using TaskManagement.Core.Services;
using TaskManagement.Infrastructure.Database.Contexts;
using TaskManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Logging.AddConsole();
builder.Services.AddControllers();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API V1");
});

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DatabaseContext>();
    context.Database.EnsureCreated();
}


app.Run();
