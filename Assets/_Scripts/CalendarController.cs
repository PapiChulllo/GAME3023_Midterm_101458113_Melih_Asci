using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarController : MonoBehaviour
{
    public GameObject calendarPanel;       // Reference to the CalendarPanel UI element
    public TextMeshProUGUI yearText;       // Displays the year
    public TextMeshProUGUI monthText;      // Displays the current month
    public TextMeshProUGUI weekText;       // Displays the current week
    public TextMeshProUGUI[] dayTexts;     // Array holding each day on the calendar as UI text
    public TextMeshProUGUI eventText;      // Displays event descriptions
    public TextMeshProUGUI dateText;       // Displays the current date
    public TextMeshProUGUI timeText;       // Displays the current time

    private int year = 1;
    private int week = 1;
    private int currentDay = 1;
    private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    private int currentMonthIndex = 0;

    private int hour = 6;
    private int minute = 0;
    private float timeAccumulator = 0f;

    private Dictionary<string, Dictionary<int, string>> monthEvents = new Dictionary<string, Dictionary<int, string>>();
    public float dayAdvanceInterval = 10f;
    private float timer = 0f;

    private void Start()
    {
        InitializeCalendar();
        InitializeEvents();
        DisplayDate();
        UpdateTimeDisplay();
        UpdateEventText();

        // Ensure the calendar is initially closed
        if (calendarPanel != null)
        {
            calendarPanel.SetActive(false);
        }
    }

    private void Update()
    {
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
        UpdateMonthText();
        UpdateWeekText();
        UpdateDayNumbers();
    }

    private void InitializeEvents()
    {
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
    }

    private void AdvanceTime()
    {
        timeAccumulator += Time.deltaTime;

        if (timeAccumulator >= 1f)
        {
            minute++;
            timeAccumulator = 0f;

            if (minute >= 60)
            {
                minute = 0;
                hour++;

                if (hour >= 24)
                {
                    hour = 0;
                    NextDay();
                }
            }

            UpdateTimeDisplay();
        }
    }

    private void UpdateTimeDisplay()
    {
        string period = hour < 12 ? "AM" : "PM";
        int displayHour = hour % 12;
        if (displayHour == 0) displayHour = 12;

        timeText.text = $"{displayHour:D2}:{minute:D2} {period}";
    }

    private void DisplayDate()
    {
        string dayOfWeek = GetDayOfWeek(currentDay);
        yearText.text = $"Year: {year}";
        monthText.text = months[currentMonthIndex];
        weekText.text = $"Week: {week}";

        dateText.text = $"{dayOfWeek}, {months[currentMonthIndex]} {currentDay}";
        UpdateEventText();
    }

    private string GetDayOfWeek(int day)
    {
        string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        return daysOfWeek[(day - 1) % 7];
    }

    private void UpdateMonthText()
    {
        monthText.text = months[currentMonthIndex];
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

            if (dayNumber == currentDay)
            {
                dayTexts[i].color = Color.green;
            }
            else if (monthEvents.ContainsKey(months[currentMonthIndex]) &&
                     monthEvents[months[currentMonthIndex]].ContainsKey(dayNumber))
            {
                dayTexts[i].color = Color.magenta;
            }
            else
            {
                dayTexts[i].color = Color.white;
            }
        }
    }

    private void UpdateEventText()
    {
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

    public void NextDay()
    {
        currentDay++;

        if (currentDay > dayTexts.Length)
        {
            currentDay = 1;
            week++;
            if (week > 4)
            {
                week = 1;
                NextMonth();
            }
        }

        UpdateDayNumbers();
        DisplayDate();
    }

    public void NextMonth()
    {
        currentMonthIndex = (currentMonthIndex + 1) % months.Length;
        week = 1;
        UpdateMonthText();
        UpdateWeekText();
        DisplayDate();
    }

    public void PreviousMonth()
    {
        currentMonthIndex = (currentMonthIndex - 1 + months.Length) % months.Length;
        week = 1;
        UpdateMonthText();
        UpdateWeekText();
        DisplayDate();
    }

    // Method to toggle the calendar's visibility
    public void ToggleCalendar()
    {
        if (calendarPanel != null)
        {
            bool isActive = calendarPanel.activeSelf;
            calendarPanel.SetActive(!isActive); // Toggle the panel's active state
        }
    }
}
