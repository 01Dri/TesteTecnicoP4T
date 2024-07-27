using Microsoft.EntityFrameworkCore;
using Task = Business.Models.Task;

namespace P4P_DataAccess.Repositories;

public class TaskInMemoryEfContext : DbContext
{
    public TaskInMemoryEfContext(DbContextOptions<TaskInMemoryEfContext> options) : base(options)
    {
        
    }
    
    public DbSet<Task> Tasks { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}