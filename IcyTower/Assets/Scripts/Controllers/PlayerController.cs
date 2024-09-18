using Assets.Scripts;
using UnityEngine;

public class PlayerController : IPlayerController
{
    private IJumper jumper;

    public override KeyCode RightKey { get; set; } = KeyCode.RightArrow;
    public override KeyCode LeftKey { get; set; } = KeyCode.LeftArrow;
    public override KeyCode JumpKey { get; set; } = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        jumper = GameObject.FindAnyObjectByType<IJumper>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        int xAxis = 0;

        if (Input.GetKey(RightKey))
        {
            xAxis = 1;
        }
        else if (Input.GetKey(LeftKey)) 
        {
            xAxis = -1;
        }

        jumper.Move(xAxis);

        if (Input.GetKeyDown(JumpKey))
        {
            jumper.Jump();
        }
    }
}
