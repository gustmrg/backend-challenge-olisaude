using System.ComponentModel.DataAnnotations;

namespace OliSaude.API.Models;

public class HealthIssue
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [Range(1,2)]
    public int Severity { get; set; }
}