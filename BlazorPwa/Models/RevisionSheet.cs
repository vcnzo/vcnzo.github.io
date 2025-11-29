namespace BlazorPwa.Models;

public class RevisionSheet
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SubjectId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty; // HTML or Markdown
    public List<string> ImageUrls { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
