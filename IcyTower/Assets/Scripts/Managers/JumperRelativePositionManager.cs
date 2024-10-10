using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the relative positioning of jumper-related objects.
/// </summary>
public class JumperRelativePositionManager : IRelativePositionManager
{
    private IJumper jumper; // Reference to the jumper interface.
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
    }

    public void MoveObject()
    {
        foreach (var item in objectsList)
        {
            // Move the current object if it is below the jumper's max height.
            if (item.transform.position.y <= jumper.MaxHeight - MoveOffset)
            {
                float xPosition = UnityEngine.Random.Range(-Boundries, Boundries);
                item.transform.position = new Vector3(xPosition, item.transform.position.y + NextObjectDelta, item.transform.position.z);
                OnMovedObject(item); // Trigger the moved object event.
            }
        }
    }

    private void OnMovedObject(GameObject obj)
    {
        MovedObject?.Invoke(obj); // Trigger the moved object event if there are subscribers.
    }
}
