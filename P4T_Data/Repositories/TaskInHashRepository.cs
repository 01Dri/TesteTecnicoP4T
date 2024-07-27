using Business.Services.Interfaces;
using Task = Business.Models.Task;

namespace P4P_DataAccess.Repositories;

public class TaskInHashRepository : ITaskRepository
{
    private readonly IDictionary<Guid, Task> _data;

    public TaskInHashRepository()
    {
        _data = new Dictionary<Guid, Task>();
    }

    public Task Save(Task task)
    {
        _data[task.Id] = task;
        return task;
    }

    public Task? GetById(Guid id)
    {
        try
        {
            return _data[id];
        }
        catch (Exception)
        {
            return null;

        }
    }

    public IEnumerable<Task> GetAll()
    {
        return this._data.Values.ToList();
    }


    public Task UpdateById(Guid id, Task task)
    {
        _data[id] = task;
        return task;
    }

    public bool DeleteById(Guid id)
    {
        return _data.Remove(id);
    }
}