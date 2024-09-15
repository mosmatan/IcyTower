using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraRelativePositionManager
{
    private Queue<GameObject> objects = new Queue<GameObject>();

    private IJumper jumper;
    private GameObject currentObject;
    private System.Random random = new System.Random();

    public event Action<GameObject> MovedObject;

    public List<GameObject> ObjectsList { get; set; }
    public float NextObjectHeight { get; set; } = 0;
    public float OffsetUnder { get; set; } = 0;
    public int Boundries { get; set; } = 0;

    public GameObject CurrentObject => currentObject;

    public void Start()
    {
        jumper = GameObject.FindAnyObjectByType<Jumper>();
        listToQueue();
        currentObject = objects.Dequeue();
    }

    public void MoveObjectUp()
    {
        if (currentObject.transform.position.y <= jumper.MaxHeight - OffsetUnder)
        {
            float xPosition = random.Next(-Boundries, Boundries);
            currentObject.transform.position = new Vector3(xPosition, currentObject.transform.position.y + NextObjectHeight, currentObject.transform.position.z);
            objects.Enqueue(currentObject);
            currentObject = objects.Dequeue();
            Debug.Log("Hello");
            OnMovedObject();
            Debug.Log("!!!!");
        } 
    }

    private void listToQueue()
    {
        foreach (var brick in ObjectsList)
        {
            objects.Enqueue(brick);
        }
    }

    private void OnMovedObject()
    {
        if (MovedObject != null)
        {
            MovedObject.Invoke(currentObject);
        }
    }
}
