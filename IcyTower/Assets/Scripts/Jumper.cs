using Assets.Scripts;
using System;
using UnityEngine;

public class Jumper : IJumper
{
    private const float multiSpeedConst = 100f;

    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Collider2D collider;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float speed = 5;

    private bool onFloor = true;
    private float maxHeight = 0;
    private int xDirection = 0;

    public override float MaxHeight => maxHeight;

    public override float MinBoundaryY => collider.bounds.min.y;

    public override float CurrentHeight => gameObject.transform.position.y;

    // Update is called once per frame
    void Update()
    {
        maxHeight = Math.Max(maxHeight, transform.position.y);
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(speed * multiSpeedConst * xDirection * Time.deltaTime, 0), ForceMode2D.Force);
    }

    public override void Jump()
    {
        if (onFloor)
        {
            onFloor = false;
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public override void Move(int direction)
    {
        xDirection = direction;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Brick")
        {
            onFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Brick")
        {
            onFloor = true;
        }
    }
}
