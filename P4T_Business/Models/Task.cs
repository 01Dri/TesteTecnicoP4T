using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class Task
{
    [Key]
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
    
}