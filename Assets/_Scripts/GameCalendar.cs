using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCalendar : MonoBehaviour
{
    // Public fields for UI elements
    public TextMeshProUGUI yearText;
    public TextMeshProUGUI seasonText;
    public TextMeshProUGUI weekText;
    public TextMeshProUGUI[] dayTexts; // Array for each day's TextMeshPro
    public TextMeshProUGUI eventText;
    public TextMeshProUGUI dateText; // Text for displaying the current date
    public TextMeshProUGUI timeText; // Text for displaying the current time

    // Calendar Data
    private int year = 1;
    private int week = 1;
    private int currentDay = 1;
    private string[] seasons = { "Spring", "Summer", "Autumn", "Winter" };
    private int currentSeasonIndex = 0;

    // Time Variables
    private int hour = 6; // Starting hour of the day (e.g., 6 AM)
    private int minute = 0;
    private float minuteInterval = 1f; // Number of seconds per in-game minute (adjust this to be flash)

    // Dictionary to hold events by day number
    private Dictionary<int, string> events = new Dictionary<int, string>();

    // Optional auto-advance timer
    public float dayAdvanceInterval = 10f; // Time in seconds to wait before advancing the day
    private float timer = 0f;

    private void Start()
    {
        InitializeCalendar();
        DisplayDate();
        UpdateTimeDisplay();
    }

    private void Update()
    {
        // Advance time in the game
        AdvanceTime();

        timer += Time.deltaTime;
        if (timer >= dayAdvanceInterval)
        {
            NextDay();
            timer = 0f;
        }
    }


    private void InitializeCalendar()
    {
        events[2] = "Rainy Season on Spring 7";
        events[11] = "Testing Event on Spring 11";
        events[28] = "Summer Solstice on Spring 28";

        UpdateSeasonText();
        UpdateWeekText();
        UpdateDayNumbers();
    }
    private float timeAccumulator = 0f; // Accumulator to keep track of time progression

    private void AdvanceTime()
    {
        // Accumulate time
        timeAccumulator += Time.deltaTime;

        // Each second of real time represents 1 in-game minute 
        if (timeAccumulator >= 1f)
        {
            minute += Mathf.FloorToInt(timeAccumulator);
            timeAccumulator = 0f; // Reset accumulator after updating minute

            // Handle minutes overflow to increase hours
            if (minute >= 60)
            {
                minute = 0;
                hour++;

                // If hour reaches 24, reset to 0 and go to the next day
                if (hour >= 24)
                {
                    hour = 0;
                    NextDay();
                }
            }

            // Update the time display each time the minute changes
            UpdateTimeDisplay();
        }
    }


    private void UpdateTimeDisplay()
    {
        // Format the time display as "HH:MM AM/PM"
        string period = hour < 12 ? "AM" : "PM";
        int displayHour = hour % 12;
        if (displayHour == 0) displayHour = 12; // 12-hour format

        timeText.text = $"{displayHour:D2}:{minute:D2} {period}";
    }

    private void DisplayDate()
    {
        string dayOfWeek = GetDayOfWeek(currentDay); // Get the correct day of the week
        yearText.text = $"Year: {year}";
        seasonText.text = seasons[currentSeasonIndex];
        weekText.text = $"Week: {week}";

        // Update the date display (e.g., "Tuesday / 2 / 4")
        dateText.text = $"{dayOfWeek} / {currentDay} / {currentSeasonIndex + 1}";
    }

    private string GetDayOfWeek(int day)
    {
        // Assuming a 7-day cycle starting from Monday
        string[] daysOfWeek = { "M", "T", "W", "T", "F", "SA", "SU" };
        return daysOfWeek[(day - 1) % 7];
    }

    public void NextSeason()
    {
        currentSeasonIndex = (currentSeasonIndex + 1) % seasons.Length;
        week = 1; // Reset the week for the new season
        UpdateSeasonText();
        UpdateWeekText();
    }

    public void PreviousSeason()
    {
        currentSeasonIndex = (currentSeasonIndex - 1 + seasons.Length) % seasons.Length;
        week = 1; // Reset the week for the new season
        UpdateSeasonText();
        UpdateWeekText();
    }

    private void UpdateSeasonText()
    {
        seasonText.text = seasons[currentSeasonIndex];
    }

    private void UpdateWeekText()
    {
        weekText.text = $"Week: {week}";
    }

    private void UpdateDayNumbers()
    {
        for (int i = 0; i < dayTexts.Length; i++)
        {
            int dayNumber = i + 1;
            dayTexts[i].text = dayNumber.ToString("D2");

            // Highlight the current day in green
            if (dayNumber == currentDay)
            {
                dayTexts[i].color = Color.green;
            }
            // Set event days to pink
            else if (events.ContainsKey(dayNumber))
            {
                dayTexts[i].color = Color.magenta;
            }
            // Default day color
            else
            {
                dayTexts[i].color = Color.white;
            }
        }

        UpdateEventText();
    }

    private void UpdateEventText()
    {
        // Check if there's an event for the current day
        if (events.ContainsKey(currentDay))
        {
            eventText.text = events[currentDay];
        }
        else
        {
            eventText.text = "No events today";
        }
    }

    public void NextDay()
    {
        currentDay++;

        // Reset to the beginning of the month if we've reached the end
        if (currentDay > dayTexts.Length)
        {
            currentDay = 1;
            week++;
            if (week > 4)
            {
                week = 1;
                NextSeason();
            }
        }

        UpdateDayNumbers();
        DisplayDate(); // Ensure date display is updated here
    }
}
