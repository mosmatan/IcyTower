using UnityEngine;
using TMPro;
using Assets.Scripts.Interfaces;
public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI floorText;
    [SerializeField] private IScoreManager scoreManager;

    private void OnEnable()
    {
        Time.timeScale = 0;
        scoreText.text = $"Score: {scoreManager.Score}";
        floorText.text = $"Floor: {scoreManager.Floor}";
    }
}
