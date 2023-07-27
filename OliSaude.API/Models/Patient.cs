using OliSaude.API.Enums;

namespace OliSaude.API.Models;

public class Patient
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public List<HealthIssue> HealthIssues { get; set; } = new();
}