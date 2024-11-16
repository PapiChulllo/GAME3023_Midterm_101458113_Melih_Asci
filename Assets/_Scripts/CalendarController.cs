using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarController : MonoBehaviour
{
    public GameObject calendarPanel;
    public TextMeshProUGUI yearText; // Displays the current year
    public TextMeshProUGUI monthText; // Displays the current month
    public TextMeshProUGUI seasonText; // Displays the current season
    public TextMeshProUGUI[] dayTexts; // Text elements for each day in the calendar
    public TextMeshProUGUI eventText; // Displays the event for the selected day
    public TextMeshProUGUI dateText; // Displays the full current date
    public TextMeshProUGUI timeText; // Displays the current time (HH:MM AM/PM)

    private int year = 2024; // Current year (starts from 1)
    private int currentDay = 1; // Current day of the month
    private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    private int currentMonthIndex = 0; // Tracks the current month (0 = January)

    private int hour = 6; // Default start time (6 AM)
    private int minute = 0; // Default start minute
    private float timeAccumulator = 0f; // Tracks time progression

    public float timeMultiplier = 60f; // 1 second real-time equals 60 seconds in-game time

    private Dictionary<string, Dictionary<int, string>> monthEvents = new Dictionary<string, Dictionary<int, string>>();

    public float dayAdvanceInterval = 10f; // How long it takes (in seconds) to advance to the next day
    private float timer = 0f; // Tracks time for day advancement

    private void Start()
    {
        InitializeCalendar();
        InitializeEvents();
        DisplayDate(); // Show the current date on the UI
        UpdateTimeDisplay(); // Show the current time
        UpdateEventText(); // Show the event for the current day (if any)

        // Make sure the calendar is hidden when the game starts
        if (calendarPanel != null)
        {
            calendarPanel.SetActive(false); // Hide the calendar
        }

        UpdateDayNumbers(); // Highlight days with events
    }

    private void Update()
    {
        // Update the clock every frame to make it progress faster
        UpdateTimeDisplay();
    }

    public void ToggleCalendar()
    {
        // Opens or closes the calendar when called
        if (calendarPanel != null)
        {
            bool isActive = calendarPanel.activeSelf;
            calendarPanel.SetActive(!isActive); // Toggle visibility
        }
    }

    private void InitializeCalendar()
    {
        UpdateMonthText();
        UpdateSeasonText(); // Update season instead of week
        UpdateDayNumbers();
    }

    private void InitializeEvents()
    {
        // Add specific events for certain days and months
        monthEvents["January"] = new Dictionary<int, string> {
            { 3, "Alex's Birthday" },
            { 15, "Team Meeting" }
        };

        monthEvents["June"] = new Dictionary<int, string> {
            { 15, "Joss's Birthday" }
        };

        monthEvents["February"] = new Dictionary<int, string> {
            { 14, "Valentine's Day" }
        };

        monthEvents["October"] = new Dictionary<int, string> {
            { 10, "Burak's Birthday" }
        };
        monthEvents["April"] = new Dictionary<int, string> {
            { 25, "GTA6 Release Date " }
        };

    }

    private void UpdateDayNumbers()
    {
        // Update the calendar days and highlight special ones
        for (int i = 0; i < dayTexts.Length; i++)
        {
            int dayNumber = i + 1; // Calendar starts from day 1
            dayTexts[i].text = dayNumber.ToString("D2"); // Format day as "01", "02", etc.

            string currentMonth = months[currentMonthIndex];
            bool isCurrentDay = dayNumber == currentDay;

            if (isCurrentDay)
            {
                dayTexts[i].color = Color.green; // Highlight the current day
            }
            else if (monthEvents.ContainsKey(currentMonth) && monthEvents[currentMonth].ContainsKey(dayNumber))
            {
                dayTexts[i].color = Color.magenta; // Highlight days with events
            }
            else
            {
                dayTexts[i].color = Color.white; // Default color for regular days
            }
        }
    }

    private void UpdateEventText()
    {
        // Show the event for the current day, or "No events today" if none
        string currentMonth = months[currentMonthIndex];

        if (monthEvents.ContainsKey(currentMonth) && monthEvents[currentMonth].ContainsKey(currentDay))
        {
            eventText.text = monthEvents[currentMonth][currentDay];
        }
        else
        {
            eventText.text = "No events today.";
        }
    }

    private void DisplayDate()
    {
        // Update the full date display
        string dayOfWeek = GetDayOfWeek(currentDay);
        yearText.text = $"Year: {year}";
        monthText.text = months[currentMonthIndex];
        seasonText.text = GetCurrentSeason(); // Show the current season

        dateText.text = $"{dayOfWeek}, {months[currentMonthIndex]} {currentDay}";
        UpdateEventText();
        UpdateDayNumbers();
    }

    private string GetDayOfWeek(int day)
    {
        // Calculate the day of the week (Monday, Tuesday, etc.)
        string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        return daysOfWeek[(day - 1) % 7];
    }

    private void UpdateTimeDisplay()
    {
        // Accumulate real-time scaled by the multiplier
        timeAccumulator += Time.deltaTime * timeMultiplier;

        // Convert accumulated time into hours and minutes
        while (timeAccumulator >= 60f)
        {
            minute++;
            timeAccumulator -= 60f;

            if (minute >= 60)
            {
                minute = 0;
                hour++;

                if (hour >= 24)
                {
                    hour = 0;
                    NextDay(); // Advance to the next day
                }
            }
        }

        // Update the time display in HH:MM AM/PM format
        string period = hour < 12 ? "AM" : "PM";
        int displayHour = hour % 12;
        if (displayHour == 0) displayHour = 12;

        timeText.text = $"{displayHour:D2}:{minute:D2} {period}";
    }

    public void NextDay()
    {
        // Advance to the next day
        currentDay++;

        if (currentDay > dayTexts.Length)
        {
            currentDay = 1; // Reset day to 1
            NextMonth(); // Move to the next month
        }

        DisplayDate();
    }

    public void NextMonth()
    {
        // Advance to the next month
        currentMonthIndex = (currentMonthIndex + 1) % months.Length;
        UpdateMonthText();
        UpdateSeasonText();
        DisplayDate();
    }

    public void PreviousMonth()
    {
        // Go back to the previous month
        currentMonthIndex = (currentMonthIndex - 1 + months.Length) % months.Length;
        UpdateMonthText();
        UpdateSeasonText();
        DisplayDate();
    }

    private void UpdateMonthText()
    {
        // Update the month display
        monthText.text = months[currentMonthIndex];
    }

    private void UpdateSeasonText()
    {
        // Update the season display based on the month
        seasonText.text = GetCurrentSeason();
    }

    private string GetCurrentSeason()
    {
        // Determine the season based on the month
        switch (currentMonthIndex)
        {
            case 11:
            case 0:
            case 1:
                return "Winter";
            case 2:
            case 3:
            case 4:
                return "Spring";
            case 5:
            case 6:
            case 7:
                return "Summer";
            case 8:
            case 9:
            case 10:
                return "Fall";
            default:
                return "";
        }
    }
}
