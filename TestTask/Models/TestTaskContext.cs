using Microsoft.EntityFrameworkCore;

namespace TestTask.Models
{
    public class TestTaskContext : DbContext
    {
        public TestTaskContext(DbContextOptions<TestTaskContext> options) : base(options) {}
        
        public DbSet<Task> Tasks { get; set; }
    }
}