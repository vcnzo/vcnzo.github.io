namespace BlazorPwa.Models;

public class Flashcard
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SubjectId { get; set; } = string.Empty;
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
}
