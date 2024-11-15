using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCalendar : MonoBehaviour
{
    // Public fields to display year, season, week, and day
    public TextMeshProUGUI yearText;
    public TextMeshProUGUI seasonText;
    public TextMeshProUGUI weekText;
    public TextMeshProUGUI[] dayTexts; // Array for each day's TextMeshPro

    public TextMeshProUGUI eventText; // Displays events happening on the selected day

    // Calendar Data
    private int year = 1;
    private int week = 1;
    private int currentDay = 1;
    private string[] seasons = { "Spring", "Summer", "Autumn", "Winter" };
    private int currentSeasonIndex = 0;

    private Dictionary<int, string> events = new Dictionary<int, string>();

    private void Start()
    {
        InitializeCalendar();
        DisplayDate();
    }

    private void InitializeCalendar()
    {
        // Add example events for specific days (you can add more or make them dynamic)
        events[2] = "Rainy Season on Spring 7";
        events[11] = "Testing Event on Spring 11";
        events[28] = "Summer Solstice on Spring 28";

        UpdateSeasonText();
        UpdateWeekText();
        UpdateDayNumbers();
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

    private void DisplayDate()
    {
        yearText.text = $"Year: {year}";
        seasonText.text = seasons[currentSeasonIndex];
        weekText.text = $"Week: {week}";
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
            dayTexts[i].text = (i + 1).ToString("D2");

            // Highlight the current day
            if (i + 1 == currentDay)
            {
                dayTexts[i].color = Color.green; // Change this color as desired
            }
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
    }
}
