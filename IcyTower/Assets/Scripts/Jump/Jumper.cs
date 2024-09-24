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

    [SerializeField] private Rigidbody2D rigidbody; // Rigidbody component for physics.
    [SerializeField] private Collider2D collider; 
    [SerializeField] private float jumpForce = 5; // Force applied for a normal jump.
    [SerializeField] private float speed = 5; 
    [SerializeField] private float superJumpWindow = 0.1f; // Duration for super jump window.
    [SerializeField] private Animator animator; // Animator for character animations.
    [SerializeField] private ObjectPooler pooler; // Object pooler for stars.
    [SerializeField] private int starsPerFrame; // Number of stars to throw per frame.

    private bool onFloor = true; 
    private float maxHeight = 0; // Maximum height reached.
    private int xDirection = 0; // Horizontal movement direction.
    private bool isSuperJump = false; // Is the jumper in a super jump state?
    private bool isSuperJumpWindowOpen = false;

    public override float MaxHeight => maxHeight; // Gets the max height.

    public override float MinBoundaryY => collider.bounds.min.y; // Gets the minimum Y boundary.

    public override float CurrentHeight => transform.position.y; // Gets the current height.

    public override bool IsSuperJumping => isSuperJump; // Checks if currently super jumping.

    private void Update()
    {
        maxHeight = Math.Max(maxHeight, transform.position.y); 
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
        if (collision.gameObject.CompareTag("Brick"))
        {
            onFloor = false; // Set onFloor to false when leaving a brick.
            isSuperJumpWindowOpen = false; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            StartCoroutine(SuperJumpTimer()); // Start super jump window timer.
            onFloor = true; // Set onFloor to true when landing on a brick.
        }
    }

    public override void Jump()
    {
        if (isSuperJumpWindowOpen && rigidbody.velocity.y <= 0)
        {
            spicelJump(2);
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
            transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z); // Flip character based on direction.
        }
    }

    private void throwStars()
    {
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
        animator.SetFloat("XVelocity", math.abs(rigidbody.velocity.x)); 
        animator.SetFloat("YVelocity", rigidbody.velocity.y); 
    }

    private void updateStopSuperJump()
    {
        if (onFloor && rigidbody.velocity.y < 0.1f)
        {
            animator.SetBool("SuperJump", false); 
            isSuperJump = false; 
        }
    }

    private IEnumerator SuperJumpTimer()
    {
        isSuperJumpWindowOpen = true; 
        yield return new WaitForSeconds(superJumpWindow); 
        isSuperJumpWindowOpen = false; 
    }

    protected override void OnJumped()
    {
        Jumped?.Invoke(); // Invoke the Jumped event.
    }

    public void UltraJumpBoost(float force)
    {
        spicelJump(force);
    }
}
