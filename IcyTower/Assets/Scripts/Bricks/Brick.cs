using System;
using UnityEngine;

/// <summary>
/// Represents a brick object in a 2D game environment.
/// </summary>
public class Brick : MonoBehaviour
{
    public event Action PlayerPass; // Triggered when the player passes the brick.
    public event Action<Brick, int> PositionChanged; // Triggered when the brick's position changes.

    private RaycastHit2D hitRight; 
    private RaycastHit2D hitLeft;  
    private bool countScoreMode = true; // Flag to determine if scoring should be counted.
    private int positionChangeCounter = -1; 

    [SerializeField] private Collider2D collider; 
    [SerializeField] private SpriteRenderer spriteRenderer; 

    public Collider2D Collider => collider; // Gets the collider.
    public Sprite Sprite { get { return spriteRenderer.sprite; } set { spriteRenderer.sprite = value; } } // Gets/sets the sprite.

    public void SetActiveCollider(bool active)
    {
        collider.enabled = active; // Activates or deactivates the collider.
    }

    private void Update()
    {
        handlePositionChange();
        handlePlayerPass();
    }

    // Checks if the player has passed the brick using raycasting.
    private void handlePlayerPass()
    {
        hitRight = Physics2D.Raycast(transform.position, transform.right);
        hitLeft = Physics2D.Raycast(transform.position, -transform.right);

        if (countScoreMode)
        {
            if (hitRight.collider != null && hitRight.collider.CompareTag("Player"))
            {
                countScoreMode = false;
                OnPlayerPass();
            }

            if (hitLeft.collider != null && hitLeft.collider.CompareTag("Player"))
            {
                countScoreMode = false;
                OnPlayerPass();
            }
        }
    }

    // Checks if the brick's position has changed. If so, triggers the PositionChanged event.
    private void handlePositionChange()
    {
        if (transform.hasChanged)
        {
            OnPositionChanged();
            transform.hasChanged = false;
            countScoreMode = true; // Reset score counting after position change.
        }
    }

    protected virtual void OnPlayerPass()
    {
        PlayerPass?.Invoke(); // Invoke PlayerPass event.
    }

    protected virtual void OnPositionChanged()
    {
        positionChangeCounter++;
        PositionChanged?.Invoke(this, positionChangeCounter); // Invoke PositionChanged event.
    }

    public void AddPositionChangedTime()
    {
        positionChangeCounter++; // Increment the position change counter.
    }
}
