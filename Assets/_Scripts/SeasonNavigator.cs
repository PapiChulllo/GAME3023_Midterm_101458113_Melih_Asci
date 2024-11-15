using UnityEngine;
using TMPro;

public class SeasonNavigator : MonoBehaviour
{
    public TextMeshProUGUI seasonText; // Text element to display the current season
    private string[] seasons = { "Spring", "Summer", "Autumn", "Winter" }; // Array of seasons
    private int currentSeasonIndex = 0; // Index to keep track of the current season

    private void Start()
    {
        UpdateSeason(); // Set the initial season text
    }

    public void NextSeason()
    {
        currentSeasonIndex = (currentSeasonIndex + 1) % seasons.Length; // Increment season index and loop back if it exceeds array length
        UpdateSeason();
    }

    public void PreviousSeason()
    {
        currentSeasonIndex = (currentSeasonIndex - 1 + seasons.Length) % seasons.Length; // Decrement season index and loop back if negative
        UpdateSeason();
    }

    private void UpdateSeason()
    {
        seasonText.text = seasons[currentSeasonIndex]; // Update the seasonText to show the current season
    }
}
