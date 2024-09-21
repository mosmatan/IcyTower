using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Handles player input and controls the jumper character.
/// </summary>
public class PlayerController : IPlayerController
{
    private IJumper jumper; // Reference to the jumper interface.

    public override KeyCode RightKey { get; set; } = KeyCode.RightArrow; // Key for moving right.
    public override KeyCode LeftKey { get; set; } = KeyCode.LeftArrow; // Key for moving left.
    public override KeyCode JumpKey { get; set; } = KeyCode.Space; // Key for jumping.

    private void Start()
    {
        jumper = GameObject.FindAnyObjectByType<IJumper>(); // Find the jumper in the scene.
    }

    private void Update()
    {
        move(); // Check for player input.
    }

    private void move()
    {
        int xAxis = 0;

        // Determine horizontal movement direction.
        if (Input.GetKey(RightKey))
        {
            xAxis = 1;
        }
        else if (Input.GetKey(LeftKey)) 
        {
            xAxis = -1;
        }

        jumper?.Move(xAxis); // Move the jumper.

        if (Input.GetKeyDown(JumpKey))
        {
            jumper?.Jump(); // Execute jump action.
        }
    }
}