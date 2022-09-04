 public static class TestMapExtensions
 {
    public static void AddTestEnpoints(this WebApplication app)
    {
        app.MapGet("Test/", () => {
            return Results.Ok("This is a test for the GetAll method of Test. Note that, to work correctly in Swagger, the '.WithName()' clause of each endpoint must be unique.");
        })
            .Produces(StatusCodes.Status200OK)
            .WithName("GetAllTest")
            .WithTags("Test");
    }

}

