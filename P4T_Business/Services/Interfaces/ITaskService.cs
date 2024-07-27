using Business.Models.DTOs;

namespace Business.Services.Interfaces;

public interface ITaskService
{

    IEnumerable<TaskResponseDTO> Get();
    TaskResponseDTO Save(TaskRequestDTO taskRequestDto);
    TaskResponseDTO GetById(Guid id);
    TaskResponseDTO UpdateById(Guid id, TaskUpdateRequestDTO requestDto);
    bool DeleteTaskById(Guid id);


}