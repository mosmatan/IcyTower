using System;
using System.Collections;
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

    [SerializeField] private Collider2D collider; 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float holdShake;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody;
    
    public Collider2D Collider => collider; // Gets the collider.
    public Sprite Sprite { get { return spriteRenderer.sprite; } set { spriteRenderer.sprite = value; } } // Gets/sets the sprite.

    public Vector3 LastPosition => lastPosition;
    private void Awake()
    {
        lastPosition = transform.position;
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
        if (other.transform.tag == "Player")
        {
            handleShake();
        }
    }

    private void handleShake()
    {
        if (!isShaking)
        {
            isShaking = true;
            StartCoroutine(startShake());
        }
    }

    private IEnumerator startShake()
    {
        yield return new WaitForSeconds(holdShake);
        animator.SetTrigger("StartShake");
        yield return new WaitForSeconds(holdShake);
        StartCoroutine(falling());
    }

    private IEnumerator falling()
    {
        isFalling = true;
        collider.enabled = false;

        while (isShaking)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 2f);
            yield return new WaitForFixedUpdate();
        }

        isFalling = false;
    }
}
