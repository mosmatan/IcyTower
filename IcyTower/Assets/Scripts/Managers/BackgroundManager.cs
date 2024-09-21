using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

/// <summary>
/// Manages background objects' positions relative to the jumper.
/// </summary>
public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ObjectsList = new List<GameObject>(); // List of background objects.
    private IRelativePositionManager positionManager; 

    [SerializeField] private float nextObjectHeight; // Height for the next object placement.
    [SerializeField] private float offsetUnder; // Offset for positioning under the jumper.

    private void Awake()
    {
        // Initialize the position manager with the object list and set parameters.
        positionManager = new JumperRelativePositionManager(ObjectsList)
        {
            NextObjectDelta = nextObjectHeight,
            MoveOffset = offsetUnder
        };
    }

    private void Update()
    {
        positionManager.MoveObject(); 
    }
}