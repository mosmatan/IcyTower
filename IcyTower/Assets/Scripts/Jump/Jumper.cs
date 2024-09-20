using Assets.Scripts;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Jumper : IJumper
{
    private const float multiSpeedConst = 100f;

    public override event Action Jumped;
    
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Collider2D collider;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float speed = 5;
    [SerializeField] private float superJumpWindow = 0.1f;
    [SerializeField] private Animator animator;
    [SerializeField] private ObjectPooler pooler;
    [SerializeField] private int starsPerFrame;

    private bool onFloor = true;
    private float maxHeight = 0;
    private int xDirection = 0;
    private bool isSuperJump = false;
    private bool isSuperJumpWindowOpen= false;

    public override float MaxHeight => maxHeight;

    public override float MinBoundaryY => collider.bounds.min.y;

    public override float CurrentHeight => gameObject.transform.position.y;

    public override bool IsSuperJumping => isSuperJump;

    void Update()
    {
        maxHeight = Math.Max(maxHeight, transform.position.y);

        updateAnimationParams();
        throwStars();
        updateStopSuperJump();
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(speed * multiSpeedConst * xDirection * Time.deltaTime, 0), ForceMode2D.Force);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Brick")
        {
            onFloor = false;
            isSuperJumpWindowOpen = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Brick")
        {
            StartCoroutine(SuperJumpTimer());
            onFloor = true;
        }
    }

    public override void Jump()
    {
        if (isSuperJumpWindowOpen && rigidbody.velocity.y <= 0)
        {
            onFloor = false;
            rigidbody.AddForce(new Vector2(0, jumpForce * 2), ForceMode2D.Impulse);
            animator.SetBool("SuperJump", true);
            isSuperJump = true;
            OnJumped();
        }
        else if (onFloor)
        {
            onFloor = false;
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            OnJumped();
        }
    }

    public override void Move(int direction)
    {
        xDirection = direction;

        if (direction != 0)
        {
            transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
        }
    }

    private void throwStars()
    {
        if(isSuperJump)
        {
            for(int i = 0;i<starsPerFrame;i++)
            {
                GameObject star = pooler.DrawObjectFrom("Stars");
                star.transform.SetPositionAndRotation(transform.position, transform.rotation);
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
        if (onFloor && rigidbody.velocity.y <= 0)
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
        if (Jumped != null)
        {
            Jumped();
        }
    }
}
