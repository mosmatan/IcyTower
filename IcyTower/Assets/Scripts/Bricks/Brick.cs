using System;
using System.Collections;
using Assets.Scripts.Interfaces;
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
    private int positionChangeCounter = 0;
    private Vector3 lastPosition;
    private bool isShaking = false;
    private bool isFalling = false;
    private int bigBrick;

    [SerializeField] private Collider2D collider; 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float holdShake;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private TextBrick textBrick;
    [SerializeField] private bool isTextBrick = false;
    public Collider2D Collider => collider; // Gets the collider.
    public Sprite Sprite { get { return spriteRenderer.sprite; } set { spriteRenderer.sprite = value; } } // Gets/sets the sprite.

    public Vector3 LastPosition => lastPosition;
    
    private void Awake()
    {
        lastPosition = transform.position;
        bigBrick = GameManager.Instance.FloorsForLevel; // Initialize bigBrick.
    }

    public void SetActiveCollider(bool active)
    {
        collider.enabled = active && !isFalling; // Activates or deactivates the collider
    }

    private void Update()
    {
        //HandlePositionChange();
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
    public void HandlePositionChange()
    {
        isShaking = false;
        animator.SetTrigger("StopShake");
        OnPositionChanged();
        transform.hasChanged = false;
        countScoreMode = true; // Reset score counting after position change.
        lastPosition = transform.position;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            handleShake(); // Trigger shake effect on player collision.
        }
    }

    private void handleShake()
    {
        Debug.Log($"TextBrick Value: {textBrick.Value}, BigBrick: {bigBrick}");
        // Big brick will not shake and fall. 
        if (!isShaking)
        {
            if (!isTextBrick || (textBrick.Value % bigBrick != 0))
            {
                isShaking = true;
                StartCoroutine(startShake()); // Start the shake coroutine.
            }
        }
    }

    private IEnumerator startShake()
    {
        Debug.Log("shake");
        yield return new WaitForSeconds(holdShake); // Wait before starting shake.
        animator.SetTrigger("StartShake"); // Trigger shake animation.
        yield return new WaitForSeconds(holdShake); // Wait for shake duration.
        Debug.Log("after shake before fall");
        StartCoroutine(falling()); // Start falling coroutine.
    }

    private IEnumerator falling()
    {
        Debug.Log("fall");
        isFalling = true; 
        collider.enabled = false; // Disable collider during fall.

        while (isShaking)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 2f); // Move the brick downwards.
            yield return new WaitForFixedUpdate(); 
        }

        isFalling = false; 
        isShaking = false; 
    }
}