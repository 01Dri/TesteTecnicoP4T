using System.ComponentModel.DataAnnotations;

namespace Business.Models.DTOs;

public class TaskRequestDTO
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(20, ErrorMessage = "Title cannot be longer than 20 characters.")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Due Date is required.")]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(TaskRequestDTO), nameof(ValidateDueDate))]
    public DateTime DueDate { get; set; }
    
    [Required(ErrorMessage = "Priority is required.")]
    [Range(1, 5, ErrorMessage = "Priority must be between 1 and 5.")]
    public int Priority { get; set; }
    
    public static ValidationResult ValidateDueDate(DateTime dueDate, ValidationContext context)
    {
        if (dueDate < DateTime.Now.Date)
        {
            return new ValidationResult("Due Date cannot be in the past.");
        }
        return ValidationResult.Success;
    }
}
