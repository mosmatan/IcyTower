using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickManager : Singleton<BrickManager>
{
    [SerializeField] List<GameObject> bricksArr = new List<GameObject>();
    private Queue<GameObject> bricks = new Queue<GameObject>();

    private IJumper jumper;
    GameObject currentBrick;

    [SerializeField] private float nextBrickHeigh = 10;
    [SerializeField] private float offsetNotUnder = 6;

    // Start is called before the first frame update
    void Start()
    {
        jumper = GameObject.FindAnyObjectByType<Jumper>();
        listToQueue();
        currentBrick = bricks.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBrick.transform.position.y <= jumper.MaxHeight - offsetNotUnder)
        {
            moveBrickUp();
        }
    }

    private void moveBrickUp()
    {
        currentBrick.transform.Translate(transform.position + (new Vector3(0, nextBrickHeigh, 0)));
        bricks.Enqueue(currentBrick);
        currentBrick = bricks.Dequeue();
    }

    private void listToQueue()
    {
        foreach (var brick in bricksArr)
        {
            bricks.Enqueue(brick);
        }
    }
}
