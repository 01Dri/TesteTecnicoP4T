using Business.Exceptions;
using Business.Models.DTOs;
using Business.Services.Interfaces;
using Task = Business.Models.Task;

namespace Business.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<TaskResponseDTO> Get()
    {
        return this._repository.GetAll().Select(x => this.ToResponseDTO(x));
    }

    public TaskResponseDTO Save(TaskRequestDTO taskRequestDto)
    {
        Models.Task task = new Models.Task()
        {
            Id = Guid.NewGuid(),
            Title = taskRequestDto.Title,
            Description = taskRequestDto.Description,
            DueDate = taskRequestDto.DueDate,
            Priority = taskRequestDto.Priority
        };
        _repository.Save(task);
        return this.ToResponseDTO(task);
    }

    public TaskResponseDTO GetById(Guid id)
    {
        var task = this._repository.GetById(id) ?? throw new NotFoundException("Task not found!");
        return this.ToResponseDTO(task);
    }

    public TaskResponseDTO UpdateById(Guid id, TaskUpdateRequestDTO requestDto)
    {
        var task = _repository.GetById(id) ?? throw new NotFoundException("Task not found!");
        task.Title = requestDto.Title;
        task.Description = requestDto.Description;
        task.Priority = requestDto.Priority;
        task.DueDate = requestDto.DueDate;
        _repository.UpdateById(id, task);
        return this.ToResponseDTO(task);
    }

    public bool DeleteTaskById(Guid id)
    {
        return _repository.DeleteById(id);
    }

    private TaskResponseDTO ToResponseDTO(Task task)
    {
        return new TaskResponseDTO(task.Id, task.Title, task.Description, task.DueDate, task.Priority);
    }
}