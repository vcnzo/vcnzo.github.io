using BlazorPwa.Models;

namespace BlazorPwa.Services;

public class StudyService
{
    private readonly List<Subject> _subjects = new();
    private readonly List<Flashcard> _flashcards = new();
    private readonly List<RevisionSheet> _sheets = new();

    public StudyService()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Subjects
        var math = new Subject { Name = "Mathématiques", Color = "#3B82F6", Icon = "📐" };
        var history = new Subject { Name = "Histoire", Color = "#D97706", Icon = "🏛️" };
        var french = new Subject { Name = "Français", Color = "#EC4899", Icon = "📚" };
        var science = new Subject { Name = "SVT", Color = "#10B981", Icon = "🧬" };

        _subjects.AddRange(new[] { math, history, french, science });

        // Flashcards - Math
        _flashcards.Add(new Flashcard { SubjectId = math.Id, Theme = "Algèbre", Question = "Qu'est-ce qu'une équation ?", Answer = "Une égalité comportant une ou plusieurs inconnues." });
        _flashcards.Add(new Flashcard { SubjectId = math.Id, Theme = "Géométrie", Question = "Formule de l'aire d'un triangle ?", Answer = "Base × Hauteur / 2" });
        
        // Flashcards - History
        _flashcards.Add(new Flashcard { SubjectId = history.Id, Theme = "Moyen Âge", Question = "Date du sacre de Charlemagne ?", Answer = "L'an 800" });
        
        // Sheets
        _sheets.Add(new RevisionSheet 
        { 
            SubjectId = math.Id, 
            Title = "Théorème de Pythagore", 
            Theme = "Géométrie",
            Content = "Dans un triangle rectangle, le carré de l'hypoténuse est égal à la somme des carrés des deux autres côtés.\n\nFormule : a² + b² = c²"
        });
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
}
