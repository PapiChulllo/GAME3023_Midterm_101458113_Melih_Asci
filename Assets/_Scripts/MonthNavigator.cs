using UnityEngine;

public class MonthNavigator : MonoBehaviour
{
    public CalendarController calendarController; // Reference to the CalendarController script

    public void NextMonth()
    {
        if (calendarController != null)
        {
            calendarController.NextMonth();
        }
    }

    public void PreviousMonth()
    {
        if (calendarController != null)
        {
            calendarController.PreviousMonth();
        }
    }
}
