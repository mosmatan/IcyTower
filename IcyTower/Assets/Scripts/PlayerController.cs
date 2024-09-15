using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private IJumper jumper;

    private void Awake()
    {
        
    }

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
        float xAxis = Input.GetAxisRaw("Horizontal");

        jumper.Move((int)xAxis);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumper.Jump();
        }
    }
}
