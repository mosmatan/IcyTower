using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the player's score, floors, and combo states.
/// </summary>
public class ScoreManager : IScoreManager
{
    public override event Action<int> ComboChanged; // Event triggered when the combo changes.

    [SerializeField] private List<Brick> bricks = new List<Brick>();
    [SerializeField] private TextMeshProUGUI scoreUI; // UI element for displaying the score.
    [SerializeField] private IJumper jumper; // Reference to the jumper interface.

    private int score = 0; // Current score.
    private int floor = 0; // Current floor count.
    private int combo = 0; // Current combo count.

    public override int Score => score; 
    public override int Floor => floor; 
    public override int Combo => combo;

    private void Start()
    {
        foreach (var brick in bricks)
        {
            brick.PlayerPass += Brick_PlayerPass; // Subscribe to PlayerPass event.
        }
    }

    private void Brick_PlayerPass()
    {
        countScore(); // Update score when the player passes a brick.
    }

    private void countScore()
    {
        score++; 
        floor++; 

        if (jumper.IsSuperJumping)
        {
            score++; // Additional score for super jumping.
            combo++; // Increment combo count.
            OnComboChanged(); // Notify combo change.
        }
        else if (combo != 0)
        {
            combo = 0; // Reset combo if not jumping.
            OnComboChanged();
        }

        scoreUI.text = $"Score: {score}"; // Update score display.
    }

    protected override void OnComboChanged()
    {
        ComboChanged?.Invoke(combo); // Trigger ComboChanged event.
    }
}
