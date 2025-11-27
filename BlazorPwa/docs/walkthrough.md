# Study App Walkthrough

## Overview
Transformed the Blazor PWA into a **Premium Study App** for students.

## Features Implemented
### 1. Premium Design System
- **Mobile-First Layout**: Bottom navigation bar for easy thumb access.
- **Aesthetics**: "Slate & Teal" color palette, rounded corners, soft shadows.
- **Font**: Integrated 'Inter' font for a modern look.

### 2. Quiz (Flashcards)
- **Subject Selection**: Home page displays subjects with card counts.
- **Session**: Interactive flashcards with flip animation.
- **Progress**: Visual progress bar during the session.

### 3. Fiches (Revision Sheets)
- **List View**: Masonry-style grid of revision sheets.
- **Editor**: Create/Edit sheets with Title, Subject, Theme, and Content.
- **Mock Tools**: "Scan" and "Add Image" buttons (simulated).

### 4. Infos
- Simple informational page about the app.

## Technical Details
- **Data**: In-Memory `StudyService` with seeded mock data.
- **Models**: `Subject`, `Flashcard`, `RevisionSheet`.
- **Navigation**: Custom `MainLayout` with active state handling.

## How to Run
```powershell
cd BlazorPwa
dotnet run
```
Open browser at the provided localhost URL.
