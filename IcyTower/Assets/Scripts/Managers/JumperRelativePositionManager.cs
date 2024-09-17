using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class JumperRelativePositionManager : IRelativePositionManager
{
    private Queue<GameObject> objects = new Queue<GameObject>();

    private IJumper jumper;
    private GameObject currentObject;

    public event Action<GameObject> MovedObject;

    private List<GameObject> objectsList;
    public float NextObjectDelta { get; set; } = 0;
    public float MoveOffset { get; set; } = 0;
    public float Boundries { get; set; } = 0;

    public JumperRelativePositionManager(List<GameObject> objectsList)
    {
        this.objectsList = objectsList;
        initialize();
    }

    private void initialize()
    {
        jumper = GameObject.FindAnyObjectByType<IJumper>();
        listToQueue();
        currentObject = objects.Dequeue();
    }

    public void MoveObject()
    {
        if (currentObject.transform.position.y <= jumper.MaxHeight - MoveOffset)
        {
            float xPosition = UnityEngine.Random.Range(-Boundries, Boundries);
            currentObject.transform.position = new Vector3(xPosition, currentObject.transform.position.y + NextObjectDelta, currentObject.transform.position.z);
            OnMovedObject();
            objects.Enqueue(currentObject);
            currentObject = objects.Dequeue();
        } 
    }

    private void listToQueue()
    {
        foreach (var brick in objectsList)
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
