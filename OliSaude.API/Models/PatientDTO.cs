namespace OliSaude.API.Models;

public class PatientDTO
{
    public string Name { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    //public List<HealthIssue> HealthIssues { get; set; } = new();
}