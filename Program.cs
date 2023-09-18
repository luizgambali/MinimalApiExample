
using MinimalApi.Data.Context;
using MinimalApi.Data.Repository;
using MinimalApi.Interfaces;
using ToDoList.Api.Util;

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

//to create the database
DatabaseUtil.Create();

app.MapGet("Students/", (IStudentRepository repository) =>  {
    var result = repository.GetAll();
    return Results.Ok(result);

}).Produces<List<Student>>(StatusCodes.Status200OK)
  .WithName("GetAllStudents")
  .WithTags("Students");

app.MapGet("Students/{id}", (IStudentRepository repository, Guid id) => {
    var student = repository.GetById(id);

    if (student == null)
        return Results.NotFound();

    return Results.Ok(student);

}).Produces<Student>(StatusCodes.Status200OK)
  .Produces(StatusCodes.Status404NotFound)
  .WithName("GetStudentById")
  .WithTags("Students");
  

app.MapPost("Students/", (IStudentRepository repository, Student Student) =>
{
    if (!Student.Validate())
        return Results.BadRequest("Invalid data");
    var response = repository.AddNew(Student);
    if (!response)
        return Results.StatusCode(500);
    else
        return Results.Created("", Student);
}).Produces<Student>(StatusCodes.Status200OK)
  .Produces(StatusCodes.Status400BadRequest)
  .Produces(StatusCodes.Status500InternalServerError)
  .WithName("AddNewStudent")
  .WithTags("Students");

app.MapPut("Students/{id}", (IStudentRepository repository, Guid id, Student Student) =>
{
    if (id != Student.Id)
        return Results.BadRequest("The id passed as a parameter and the Student id are different");
    if (!Student.Validate())
        return Results.BadRequest("Invalid data");
    var result = repository.GetById(id);
    if (result == null)
        return Results.NotFound();
    var response = repository.Update(Student);
    if (!response)
        return Results.StatusCode(500);
    else
        return Results.NoContent();
}).Produces(StatusCodes.Status204NoContent)
  .Produces(StatusCodes.Status404NotFound)
  .Produces(StatusCodes.Status400BadRequest)
  .Produces(StatusCodes.Status500InternalServerError)
  .WithName("UpdateStudent")
  .WithTags("Students");

app.MapDelete("Students/{id}", (IStudentRepository repository, Guid id) =>
{
    var Student = repository.GetById(id);
    
    if (Student == null)
        return Results.NotFound();
    var response = repository.Delete(Student);
    if (!response)
        return Results.StatusCode(500);
    else
        return Results.NoContent();
}).Produces(StatusCodes.Status204NoContent)
  .Produces(StatusCodes.Status404NotFound)
  .Produces(StatusCodes.Status500InternalServerError)
  .WithName("DeleteStudent")
  .WithTags("Students");


app.Run();
