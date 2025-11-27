using System.Net.Http.Json;
using BlazorPwa.Models;

namespace BlazorPwa.Services;

public class StudyService
{
    private readonly HttpClient _http;
    private readonly List<Subject> _subjects = new();
    private readonly List<Flashcard> _flashcards = new();
    private readonly List<RevisionSheet> _sheets = new();
    private bool _initialized = false;

    public StudyService(HttpClient http)
    {
        _http = http;
    }

    public async Task InitializeAsync()
    {
        if (_initialized) return;

        try 
        {
            var data = await _http.GetFromJsonAsync<CurriculumData>("data/curriculum.json");
            if (data != null)
            {
                _subjects.Clear();
                _flashcards.Clear();

                foreach (var subjectDto in data.Subjects)
                {
                    var subject = new Subject 
                    { 
                        Name = subjectDto.Name, 
                        Color = subjectDto.Color, 
                        Icon = subjectDto.Icon 
                    };
                    _subjects.Add(subject);

                    foreach (var theme in subjectDto.Themes)
                    {
                        foreach (var cardDto in theme.Flashcards)
                        {
                            _flashcards.Add(new Flashcard
                            {
                                SubjectId = subject.Id,
                                Theme = theme.Name,
                                Question = cardDto.Question,
                                Answer = cardDto.Answer
                            });
                        }
                    }
                }
            }
            _initialized = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading curriculum: {ex.Message}");
        }
    }

    public List<Subject> GetSubjects() => _subjects;
    
    public Subject? GetSubject(Guid id) => _subjects.FirstOrDefault(s => s.Id == id);

    public List<Flashcard> GetFlashcards(Guid subjectId) => 
        _flashcards.Where(f => f.SubjectId == subjectId).ToList();

    public List<RevisionSheet> GetSheets() => 
        _sheets.OrderByDescending(s => s.CreatedAt).ToList();

    public void AddSheet(RevisionSheet sheet)
    {
        sheet.Id = Guid.NewGuid();
        sheet.CreatedAt = DateTime.Now;
        _sheets.Add(sheet);
    }

    private class CurriculumData
    {
        public List<SubjectDto> Subjects { get; set; } = new();
    }

    private class SubjectDto
    {
        public string Name { get; set; } = "";
        public string Color { get; set; } = "";
        public string Icon { get; set; } = "";
        public List<ThemeDto> Themes { get; set; } = new();
    }

    private class ThemeDto
    {
        public string Name { get; set; } = "";
        public List<FlashcardDto> Flashcards { get; set; } = new();
    }

    private class FlashcardDto
    {
        public string Question { get; set; } = "";
        public string Answer { get; set; } = "";
    }
}
