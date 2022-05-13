namespace AkvelonTest.DTO;

public class ProjectDTO
{
    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Priority { get; set; }
    
    public int? StatusId { get; set; }
}