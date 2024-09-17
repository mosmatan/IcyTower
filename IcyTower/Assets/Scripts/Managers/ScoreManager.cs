using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private List<Brick> bricks = new List<Brick>();
    [SerializeField] private Text scoreUI;

    private int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var brick in bricks)
        {
            brick.PlayerPass += countScore;
        }
    }

    private void countScore()
    {
        score++;

        scoreUI.text = $"Score: {score}";
    }
}
