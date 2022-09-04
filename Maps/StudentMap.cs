using MinimalApi.Interfaces;

public static class StudentMapExtensions
{
    public static void AddStudentEnpoints(this WebApplication app)
    {
        #region GetAll
        app.MapGet("Students/", (IStudentRepository repository) => {

            var result = repository.GetAll();
            return Results.Ok(result);

        })
        .Produces<List<Student>>(StatusCodes.Status200OK)
        .WithName("GetAllStudents")
        .WithTags("Students");
        #endregion

        #region GetById
        app.MapGet("Students/{id}", (IStudentRepository repository, Guid id) => {

            var student = repository.GetById(id);

            if (student == null)
                return Results.NotFound();

            return Results.Ok(student);

        })
        .Produces<Student>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithName("GetStudentById")
        .WithTags("Students");
        #endregion


        #region Add
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
        #endregion

        #region Update
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
        #endregion


        #region Delete
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

        #endregion

    }
}