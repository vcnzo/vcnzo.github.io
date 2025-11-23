namespace BlazorPwa.Models;

public class Subject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#64748B"; // Default slate
    public string Icon { get; set; } = "📚";
}
