using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the relative positioning of jumper-related objects.
/// </summary>
public class JumperRelativePositionManager : IRelativePositionManager
{
    private Queue<GameObject> objects = new Queue<GameObject>(); // Queue of objects to manage.
    private IJumper jumper; // Reference to the jumper interface.
    private GameObject currentObject; // Currently moving object.

    public event Action<GameObject> MovedObject; // Event triggered when an object is moved.

    private List<GameObject> objectsList; 
    public float NextObjectDelta { get; set; } = 0; // Distance to move the next object.
    public float MoveOffset { get; set; } = 0; // Offset for positioning.
    public float Boundries { get; set; } = 0; // Boundaries for random positioning.

    public JumperRelativePositionManager(List<GameObject> objectsList)
    {
        this.objectsList = objectsList; // Initialize with the provided list of objects.
        initialize(); 
    }

    private void initialize()
    {
        jumper = GameObject.FindAnyObjectByType<IJumper>(); // Find the jumper in the scene.
        listToQueue(); 
        currentObject = objects.Dequeue(); // Set the first object as the current object.
    }

    public void MoveObject()
    {
        // Move the current object if it is below the jumper's max height.
        if (currentObject.transform.position.y <= jumper.MaxHeight - MoveOffset)
        {
            float xPosition = UnityEngine.Random.Range(-Boundries, Boundries);
            currentObject.transform.position = new Vector3(xPosition, currentObject.transform.position.y + NextObjectDelta, currentObject.transform.position.z);
            OnMovedObject(); // Trigger the moved object event.
            objects.Enqueue(currentObject); // Add the current object back to the queue.
            currentObject = objects.Dequeue(); // Update the current object to the next in the queue.
        }
    }

    private void listToQueue()
    {
        // Enqueue all objects from the list.
        foreach (var brick in objectsList)
        {
            objects.Enqueue(brick);
        }
    }

    private void OnMovedObject()
    {
        MovedObject?.Invoke(currentObject); // Trigger the moved object event if there are subscribers.
    }
}
