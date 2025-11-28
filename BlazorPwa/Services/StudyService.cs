using System.Net.Http.Json;
using BlazorPwa.Models;

using Microsoft.JSInterop;

namespace BlazorPwa.Services;

public class StudyService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private readonly List<Subject> _subjects = new();
    private readonly List<Flashcard> _flashcards = new();
    private readonly List<RevisionSheet> _sheets = new();
    private bool _initialized = false;
    private UserProgress _progress = new();

    public StudyService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
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
            
            await LoadProgressAsync();
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

    public async Task LoadProgressAsync()
    {
        try
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", "userProgress");
            if (!string.IsNullOrEmpty(json))
            {
                _progress = System.Text.Json.JsonSerializer.Deserialize<UserProgress>(json) ?? new UserProgress();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading progress: {ex.Message}");
        }
    }

    public async Task SaveProgressAsync()
    {
        try
        {
            _progress.LastUpdated = DateTime.Now;
            var json = System.Text.Json.JsonSerializer.Serialize(_progress);
            await _js.InvokeVoidAsync("localStorage.setItem", "userProgress", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving progress: {ex.Message}");
        }
    }

    public async Task RecordFlashcardResult(Guid cardId, bool success)
    {
        if (!_progress.FlashcardResults.ContainsKey(cardId))
        {
            _progress.TotalCardsLearned++;
        }

        _progress.FlashcardResults[cardId] = success;
        
        // Update quiz stats if this is considered a quiz action (optional, but good for stats)
        _progress.TotalQuizTaken++;
        if (success) _progress.TotalCorrectAnswers++;

        await SaveProgressAsync();
    }

    public UserProgress GetProgress() => _progress;

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
