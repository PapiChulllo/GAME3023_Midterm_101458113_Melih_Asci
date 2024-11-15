using System;
using UnityEngine;
using UnityEngine.Events;

public class GameCalendar : MonoBehaviour
{
    public int day = 1;
    public int month = 1;
    public int year = 1;
    public int hour = 6; // starting hour of the day

    public float secondsPerMinute = 1f; // Adjust for time speed

    public UnityEvent onNewDay;
    public UnityEvent onNewMonth;
    public UnityEvent onNewYear;

    private float timeAccumulator = 0f;

    private void Update()
    {
        timeAccumulator += Time.deltaTime;
        if (timeAccumulator >= secondsPerMinute)
        {
            timeAccumulator = 0f;
            AdvanceTime();
        }
    }

    private void AdvanceTime()
    {
        hour++;
        if (hour >= 24) // Adjust for hours in a day
        {
            hour = 0;
            day++;
            onNewDay.Invoke();

            if (day > 30) // Adjust for days in a month
            {
                day = 1;
                month++;
                onNewMonth.Invoke();

                if (month > 4) // Adjust for months in a year
                {
                    month = 1;
                    year++;
                    onNewYear.Invoke();
                }
            }
        }
    }
}
