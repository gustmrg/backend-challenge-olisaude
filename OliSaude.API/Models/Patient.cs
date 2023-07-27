namespace OliSaude.API.Models;

public class Patient
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public DateTime? UpdatedDate { get; set; }
    
    //public ICollection<HealthIssue> HealthIssues { get; set; } = new HashSet<HealthIssue>(); 
}