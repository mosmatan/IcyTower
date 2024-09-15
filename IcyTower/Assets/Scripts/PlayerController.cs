using Assets.Scripts;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private IJumper jumper;

    // Start is called before the first frame update
    void Start()
    {
        jumper = GameObject.FindAnyObjectByType<Jumper>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        int xAxis = (int)Input.GetAxisRaw("Horizontal");

        jumper.Move(xAxis);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumper.Jump();
        }
    }
}
