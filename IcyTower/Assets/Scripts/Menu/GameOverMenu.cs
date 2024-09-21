using UnityEngine;
using TMPro;
using Assets.Scripts.Interfaces;

/// <summary>
/// Manages the game over menu UI, displaying the player's score and floor.
/// </summary>
public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // UI element for displaying score.
    [SerializeField] private TextMeshProUGUI floorText; // UI element for displaying floor count.
    [SerializeField] private IScoreManager scoreManager; // Reference to the score manager.

    private void OnEnable()
    {
        Time.timeScale = 0; // Pause the game when the menu is enabled.
        scoreText.text = $"Score: {scoreManager.Score}"; // Update score display.
        floorText.text = $"Floor: {scoreManager.Floor}"; // Update floor display.
    }
}