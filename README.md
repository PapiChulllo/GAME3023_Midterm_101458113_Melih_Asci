# GAME3023 Midterm — In-Game Calendar

**A midterm prototype that drives an in-game calendar UI**: month/year/season display, day events, and a scaled real-time clock, alongside basic 2D movement.

---

## Status

Course midterm deliverable. Educational work focused on UI systems and time simulation.

## Tech stack

| Area | What it uses |
|---|---|
| Engine | **Unity 2022.3.36f1** |
| UI | **TextMesh Pro** |
| Language | **C#** |

## What's in the project

| System | Key files |
|---|---|
| Calendar / date / time / events | `Assets/_Scripts/CalendarController.cs` |
| Month navigation | `Assets/_Scripts/MonthNavigator.cs` |
| 2D movement | `Assets/_Scripts/Movement2D.cs` |

### Code highlights

- `CalendarController` advances in-game days on a timer, maps months to seasons, stores per-day events, and updates TMP labels for date and clock (with a configurable time multiplier).

## About this repository

Student midterm showcase.
