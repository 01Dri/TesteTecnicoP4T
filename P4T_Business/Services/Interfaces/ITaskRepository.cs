namespace Business.Services.Interfaces;

public interface ITaskRepository
{
    Business.Models.Task Save(Business.Models.Task task);
    Business.Models.Task? GetById(Guid id);
    
    IEnumerable<Business.Models.Task> GetAll();

    Business.Models.Task UpdateById(Guid id, Business.Models.Task task);
    
    bool DeleteById(Guid id);

}