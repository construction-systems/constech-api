namespace Constech.API.Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    
    //? Thread
    public int ThreadId { get; set; }
    public Thread Thread { get; set; }
}