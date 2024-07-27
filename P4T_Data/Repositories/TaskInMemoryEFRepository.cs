using Business.Services.Interfaces;
using Task = Business.Models.Task;

namespace P4P_DataAccess.Repositories;

public class TaskInMemoryEFRepository : ITaskRepository
{
    private readonly TaskInMemoryEfContext _context;

    public TaskInMemoryEFRepository(TaskInMemoryEfContext context)
    {
        _context = context;
    }


    public Task Save(Task task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return task;
    }

    public Task? GetById(Guid id)
    {
        return  _context.Tasks.Where(t => t.Id == id).FirstOrDefault();
    }

    public IEnumerable<Task> GetAll()
    {
        return _context.Tasks.ToList();
    }

    public Task UpdateById(Guid id, Task task)
    {
        
        _context.Update(task);
        _context.SaveChanges();
        return task;
    }

    public bool DeleteById(Guid id)
    {
        try
        {
            _context.Remove(this.GetById(id));
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}