using TaskManager.Api.Models;

namespace TaskManager.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Check if the database already contains any tasks
            if (context.TaskItems.Any())
            {
                return;   // DB has been seeded
            }

            var tasks = new TaskItem[]
            {
                new Models.TaskItem { Title = "Complete project proposal", Description = "Draft and finalize the project proposal document", DueDate = DateTime.Now.AddDays(7), IsCompleted = false, CreatedAt = DateTime.Now },
                new Models.TaskItem { Title = "Review code changes", Description = "Review and provide feedback on recent code changes", DueDate = DateTime.Now.AddDays(3), IsCompleted = false, CreatedAt = DateTime.Now },
                new Models.TaskItem { Title = "Prepare for team meeting", Description = "Gather updates and prepare agenda for the weekly team meeting", DueDate = DateTime.Now.AddDays(1), IsCompleted = false, CreatedAt = DateTime.Now }
            };

            context.TaskItems.AddRange(tasks);
            context.SaveChanges();
        }
    }
}