# Study App Transformation Plan

Goal: Transform the `BlazorPwa` into a premium mobile-first study application for middle school students.

## User Review Required
- **Design Direction**: "Ultra premium, discrete, subtle, effective, reassuring". I plan to use a clean, minimalist aesthetic with soft rounded corners, a calming color palette (slate/teal/soft gray), and smooth micro-interactions.
- **Data Storage**: For this version, I will use **In-Memory State** with a Mock Service to demonstrate functionality. Real persistence (Local Storage or Database) can be added later.
- **Scanning**: The "Scan text" feature will be a UI placeholder (button) for now, as actual OCR requires camera integration and external libraries which might be too complex for this initial step.

## Proposed Changes

### 1. Design System & Layout (`wwwroot/css/app.css`, `MainLayout.razor`)
- **Theme**:
    - Font: 'Inter' or system sans-serif.
    - Colors:
        - Primary: `#475569` (Slate 600) - Discrete & Reassuring.
        - Background: `#F8FAFC` (Slate 50) - Clean.
        - Surface: `#FFFFFF` (White) with soft shadows (`box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1)`).
        - Accent: `#0F766E` (Teal 700) - For success/action.
- **Layout**:
    - Remove default Blazor sidebar.
    - Add **Bottom Navigation Bar** (fixed at bottom) with icons for: `Quiz`, `Fiches`, `Infos`.
    - Mobile-first responsive container.

### 2. Data Models (`Models/`)
#### [NEW] `Models/Subject.cs`
- Id, Name, Color.
#### [NEW] `Models/Flashcard.cs`
- Id, Question, Answer, SubjectId, Theme.
#### [NEW] `Models/RevisionSheet.cs`
- Id, Title, SubjectId, Theme, Content (HTML/Markdown), ImageUrls.

### 3. Services (`Services/`)
#### [NEW] `Services/StudyService.cs`
- Mock data for Subjects, Flashcards, and Revision Sheets.
- Methods: `GetSubjects`, `GetFlashcards(subject)`, `GetSheets()`, `AddSheet(sheet)`.

### 4. Features (Pages)
#### [MODIFY] `Pages/Home.razor`
- Redirect to `Quiz` or show a "Dashboard" summary.

#### [NEW] `Pages/Quiz.razor` & `Pages/QuizSession.razor`
- **Quiz**: Select Subject/Theme.
- **QuizSession**: Display Flashcards with "Flip" animation. Buttons for "I know" / "I don't know".

#### [NEW] `Pages/Fiches.razor` & `Pages/FicheEditor.razor`
- **Fiches**: List of existing sheets (Masonry or Card grid).
- **FicheEditor**: Form to add Title, Subject, Text (textarea), and "Add Image" / "Scan" buttons.

#### [NEW] `Pages/Infos.razor`
- Static content about the app/usage.

## Verification Plan
- **Visual Check**: Ensure "Premium" look (no default Bootstrap look).
- **Navigation**: Verify Bottom Nav works on mobile view.
- **Quiz Flow**: Start quiz -> Flip card -> Next card.
- **Fiches Flow**: Create new sheet -> Save -> Appears in list.
