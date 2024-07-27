namespace Business.Models.DTOs;

public record TaskResponseDTO(Guid Id, string Title,  string Description, DateTime DueDate, int Priority);