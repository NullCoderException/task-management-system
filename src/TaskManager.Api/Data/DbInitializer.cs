using TaskManager.Api.Models;

namespace TaskManager.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Check if the database already contains any tasks
            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }

            var tasks = new Models.Task[]
            {
                new Models.Task { Title = "Complete project proposal", Description = "Draft and finalize the project proposal document", DueDate = DateTime.Now.AddDays(7), IsCompleted = false, CreatedAt = DateTime.Now },
                new Models.Task { Title = "Review code changes", Description = "Review and provide feedback on recent code changes", DueDate = DateTime.Now.AddDays(3), IsCompleted = false, CreatedAt = DateTime.Now },
                new Models.Task { Title = "Prepare for team meeting", Description = "Gather updates and prepare agenda for the weekly team meeting", DueDate = DateTime.Now.AddDays(1), IsCompleted = false, CreatedAt = DateTime.Now }
            };

            context.Tasks.AddRange(tasks);
            context.SaveChanges();
        }
    }
}