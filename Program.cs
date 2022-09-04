
using MinimalApi.Data.Context;
using MinimalApi.Data.Repository;
using MinimalApi.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//dependency injection configuration
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

//swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                    $"{builder.Environment.ApplicationName} v1"));
}


//endpoint configuration
app.AddStudentEnpoints();
app.AddTestEnpoints();

app.Run();
