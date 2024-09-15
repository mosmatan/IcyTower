using Assets.Scripts;
using System;
using UnityEngine;

public class Jumper : MonoBehaviour, IJumper
{
    
    private Rigidbody2D rigidbody;

    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float speed = 5;

    private bool onFloor = true;
    private float maxHeight = 0;

    public float MaxHeight => maxHeight;

    public float CurrentHeight => gameObject.transform.position.y;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        rigidbody.AddForce(new Vector2(speed * direction, 0), ForceMode2D.Force);
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
