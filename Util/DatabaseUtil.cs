using Microsoft.EntityFrameworkCore;
using MinimalApi.Data.Context;

namespace ToDoList.Api.Util
{
    public class DatabaseUtil
    {
        public static void Create()
        {
            using(DbContext context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}