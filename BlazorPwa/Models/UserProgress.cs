namespace BlazorPwa.Models;

public class UserProgress
{
    public int TotalCardsLearned { get; set; }
    public int TotalQuizTaken { get; set; }
    public int TotalCorrectAnswers { get; set; }
    public Dictionary<Guid, bool> FlashcardResults { get; set; } = new();
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    public double SuccessRate => TotalQuizTaken == 0 ? 0 : (double)TotalCorrectAnswers / TotalQuizTaken * 100;
}
