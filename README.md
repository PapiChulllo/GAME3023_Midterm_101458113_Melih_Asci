# GAME3023 Midterm — Calendar UI (Melih Asci)

**Educational midterm project (GAME3023, student ID in repo name: 101458113).** A Unity URP 2D scene with an in-game **calendar / date-time UI** (months, seasons, day cells, events) plus simple 2D movement and month navigation helpers.

---

## What it contains

- `CalendarController`: year/month/season labels, day text array, event lookup by month/day, togglable calendar panel, accelerated in-game clock (`timeMultiplier`), optional day-advance timer fields
- Hard-coded sample events (birthdays, meeting, Valentine’s, etc.)
- `MonthNavigator` and `Movement2D` support scripts
- Main play scene `Assets/Scenes/Main.unity`

**Status / limitations:** **course midterm**, not a full game. Calendar logic is self-contained UI/simulation rather than a large gameplay loop. Unity **2022.3.36f1**. Commit messages mention packing readiness; this pass did not produce or verify a build. No automated tests.

## Tech stack

| Area | What it uses |
|---|---|
| Engine | **Unity** `2022.3.36f1` |
| Render | **URP** (template settings scene also present) |
| UI | **TextMesh Pro** calendar/date/time fields |
| Movement | `Movement2D` Rigidbody2D helper |

## What's in the project

| System | Key files |
|---|---|
| Calendar, events, clock, toggle | `Assets/_Scripts/CalendarController.cs` |
| Month UI navigation | `Assets/_Scripts/MonthNavigator.cs` |
| 2D movement helper | `Assets/_Scripts/Movement2D.cs` |
| Main scene | `Assets/Scenes/Main.unity` |

Three authored scripts (~10 KB).

### Code / system highlights

- **Event dictionary:** per-month maps of day → string; day cells can highlight dates with events.
- **Time display:** accumulates scaled real time into HH:MM-style UI; separate fields exist for advancing days on an interval.
- **Panel toggle:** `ToggleCalendar()` shows/hides `calendarPanel` (starts hidden).

## Scenes

| Scene | Purpose |
|---|---|
| `Assets/Scenes/Main.unity` | Midterm play/UI scene |
| `Assets/Settings/Scenes/URP2DSceneTemplate.unity` | URP template leftover |

## Third-party assets

Unity URP/TMP packages and any sprites assigned in `Main`. No separate license inventory committed.

## About this repository

**Labeled educational / course midterm (GAME3023).** Author **Melih Asci** / **PapiChulllo**. Showcase of UI/time systems coursework, not a commercial title.
