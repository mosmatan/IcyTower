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
        animator.SetFloat("XVelocity", math.abs(rigidbody.velocity.x));
        animator.SetFloat("YVelocity", rigidbody.velocity.y);

        if (onFloor && rigidbody.velocity.y <= 0)
        {
            animator.SetBool("SuperJump", false);
            isSuperJump = false;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(speed * multiSpeedConst * xDirection * Time.deltaTime, 0), ForceMode2D.Force);
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
        if(collision.gameObject.tag == "Brick")
        {
            StartCoroutine(SuperJumpTimer());
            onFloor = true;
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
