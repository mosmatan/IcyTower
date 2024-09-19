using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : IScoreManager
{
    [SerializeField] private List<Brick> bricks = new List<Brick>();
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private IJumper jumper;

    private int score = 0;
    private int floor = 0;

    public override int Score => score;
    public override int Floor => floor;

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
        floor++;

        if(jumper.IsSuperJumping)
        {
            score++;
        }

        scoreUI.text = $"Score: {score}";
    }
}
