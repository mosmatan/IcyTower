using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : IScoreManager
{
    public override event Action<int> ComboChanged;

    [SerializeField] private List<Brick> bricks = new List<Brick>();
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private IJumper jumper;

    private int score = 0;
    private int floor = 0;
    private int combo = 0;
    public override int Score => score;
    public override int Floor => floor;
    public override int Combo => combo;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var brick in bricks)
        {
            brick.PlayerPass += Brick_PlayerPass;
        }
    }

    private void Brick_PlayerPass()
    {
        countScore();
    }

    private void countScore()
    {
        score++;
        floor++;

        if (jumper.IsSuperJumping)
        {
            score++;
            combo++;

            OnComboChanged();
        }
        else if (combo != 0)
        {
            combo = 0;
            OnComboChanged();
        }

        scoreUI.text = $"Score: {score}";
    }

    protected override void OnComboChanged()
    {
        if(ComboChanged != null)
        {
            ComboChanged(combo);
        }
    }
}
