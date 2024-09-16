using Assets.Scripts;
using System;
using UnityEngine;

public class Jumper : MonoBehaviour, IJumper
{
    private const float multiSpeedConst = 100f;
    
    private Rigidbody2D rigidbody;
    private Collider2D collider;

    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float speed = 5;

    private bool onFloor = true;
    private float maxHeight = 0;
    private int xDirection = 0;

    public float MaxHeight => maxHeight;

    public float MinBoundaryY => collider.bounds.min.y;

    public float CurrentHeight => gameObject.transform.position.y;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxHeight = Math.Max(maxHeight, transform.position.y);
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(speed * multiSpeedConst * xDirection * Time.deltaTime, 0), ForceMode2D.Force);
    }

    public void Jump()
    {
        if (onFloor)
        {
            onFloor = false;
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void Move(int direction)
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
