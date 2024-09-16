using Assets.Scripts;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    private IJumper jumper;

    public KeyCode RightKey { get; set; } = KeyCode.RightArrow;
    public KeyCode LeftKey { get; set; } = KeyCode.LeftArrow;
    public KeyCode JumpKey { get; set; } = KeyCode.Space;

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
