using Assets.Scripts;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Controls the jumping and movement of the jumper character.
/// </summary>
public class Jumper : IJumper
{
    private const float multiSpeedConst = 100f; // Multiplier for speed.

    public override event Action Jumped; // Triggered when the jumper jumps.

    [SerializeField] private Rigidbody2D rigidbody; 
    [SerializeField] private Collider2D collider; 
    [SerializeField] private float jumpForce = 5; // Force applied for a normal jump.
    [SerializeField] private float speed = 5; // Horizontal movement speed.
    [SerializeField] private float superJumpWindow = 0.1f; // Duration for super jump window.
    [SerializeField] private Animator animator; 
    [SerializeField] private ObjectPooler pooler; 
    [SerializeField] private int starsPerFrame; // Number of stars to throw per frame.

    private bool onFloor = true; // Indicates if the jumper is on the ground.
    private float maxHeight = 0; // Maximum height reached.
    private int xDirection = 0; // Horizontal movement direction.
    private bool isSuperJump = false; // Is the jumper in a super jump state?
    private bool isSuperJumpWindowOpen = false; 

    public override float MaxHeight => maxHeight; // Gets the maximum height.

    public override float MinBoundaryY => collider.bounds.min.y; // Gets the minimum Y boundary.

    public override float CurrentHeight => transform.position.y; // Gets the current height.

    public override bool IsSuperJumping => isSuperJump; // Checks if currently super jumping.

    private void Update()
    {
        maxHeight = Math.Max(maxHeight, transform.position.y); // Update the maximum height.
        updateAnimationParams(); // Update animation parameters.
        throwStars(); // Throw stars during a super jump.
        updateStopSuperJump(); // Stop super jump if on the ground.
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(speed * multiSpeedConst * xDirection * Time.deltaTime, 0), ForceMode2D.Force);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the jumper leaves the brick.
        if (collision.gameObject.CompareTag("Brick"))
        {
            onFloor = false; 
            isSuperJumpWindowOpen = false; // Close super jump window.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the jumper lands on a brick.
        if (collision.gameObject.CompareTag("Brick"))
        {
            StartCoroutine(SuperJumpTimer()); // Start the super jump window timer.
            onFloor = true;
        }
    }

    public override void Jump()
    {
        // Check if the jumper can perform a super jump.
        if (isSuperJumpWindowOpen && rigidbody.velocity.y <= 0)
        {
            spicelJump(2); // Apply super jump force.
        }
        else if (onFloor)
        {
            onFloor = false; 
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Apply normal jump force.
            OnJumped(); // Notify jump event.
        }
    }

    private void spicelJump(float multiForce)
    {
        // Apply force for a super jump and update state.
        rigidbody.AddForce(new Vector2(0, jumpForce * multiForce), ForceMode2D.Impulse); // Apply super jump force.
        animator.SetBool("SuperJump", true); // Trigger super jump animation.
        isSuperJump = true; 
        OnJumped(); // Notify jump event.
        onFloor = false; 
    }

    public override void Move(int direction)
    {
        xDirection = direction; // Set horizontal movement direction.

        if (direction != 0)
        {
            // Flip character based on direction.
            transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
        }
    }

    private void throwStars()
    {
        // Throw stars if currently super jumping.
        if (isSuperJump)
        {
            for (int i = 0; i < starsPerFrame; i++)
            {
                GameObject star = pooler.DrawObjectFrom("Stars"); // Get a star from the pool.
                star.transform.SetPositionAndRotation(transform.position, transform.rotation); // Set position and rotation.
            }
        }
    }

    private void updateAnimationParams()
    {
        // Update animator parameters for movement.
        animator.SetFloat("XVelocity", math.abs(rigidbody.velocity.x));
        animator.SetFloat("YVelocity", rigidbody.velocity.y);
    }

    private void updateStopSuperJump()
    {
        // Stop super jump animation and state if on the ground.
        if (onFloor && rigidbody.velocity.y < 0.1f)
        {
            animator.SetBool("SuperJump", false); 
            isSuperJump = false; 
        }
    }

    private IEnumerator SuperJumpTimer()
    {
        isSuperJumpWindowOpen = true; // Open the super jump window.
        yield return new WaitForSeconds(superJumpWindow); // Wait for the duration.
        isSuperJumpWindowOpen = false; // Close the super jump window.
    }

    protected override void OnJumped()
    {
        Jumped?.Invoke(); // Invoke the Jumped event.
    }

    public void UltraJumpBoost(float force)
    {
        spicelJump(force); // Apply a boosted jump with a specified force.
    }
}
