using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the positioning and state of bricks in the game.
/// </summary>
public class BrickManager : MonoBehaviour
{
    [SerializeField] private List<Brick> bricks = new List<Brick>(); 
    [SerializeField] private List<Sprite> sprites = new List<Sprite>(); 
    [SerializeField] private IJumper jumper; // Reference to the jumper interface.

    [SerializeField] private float nextObjectHeight; // Height for the next brick placement.
    [SerializeField] private float offsetUnder; // Offset for positioning bricks.
    [SerializeField] private int boundries; // Boundary for positioning.

    private int changeLevel; 
    private IRelativePositionManager positionManager;

    private void Awake()
    {
        setPositionManager(); 

        foreach (Brick brick in bricks)
        {
            brick.PositionChanged += Brick_PositionChanged; // Subscribe to position change events.
        }
    }

    private void Start()
    {
        changeLevel = GameManager.Instance.FloorsForLevel; // Set the change level from the game manager.
        DisableAllColliders(); // Disable colliders for all bricks.
    }

    private void Update()
    {
        positionManager.MoveObject(); // Update brick positions.
        EnableCollidersBasedOnPosition();
    }

    private void setPositionManager()
    {
        // Initialize the position manager with the list of bricks.
        positionManager = new JumperRelativePositionManager(bricks.ConvertAll(brick => brick.gameObject));
        positionManager.Boundries = boundries;
        positionManager.NextObjectDelta = nextObjectHeight;
        positionManager.MoveOffset = offsetUnder;
        positionManager.MovedObject += PositionManager_OnMovedObject;
    }

    private void PositionManager_OnMovedObject(GameObject obj)
    {
        obj.TryGetComponent(out Brick brick);
        obj.transform.position = new Vector3(obj.transform.position.x, brick.LastPosition.y + nextObjectHeight, obj.transform.position.z);
        brick.HandlePositionChange();
    }

    private void DisableAllColliders()
    {
        foreach (Brick brick in bricks)
        {
            brick.SetActiveCollider(false); 
        }
    }

    private void EnableCollidersBasedOnPosition()
    {
        foreach (Brick brick in bricks)
        {
            // Enable collider if brick is in the correct position.
            if (jumper.MinBoundaryY >= brick.Collider.bounds.max.y)
            {
                brick.SetActiveCollider(true);
            }
            else if (jumper.MinBoundaryY < brick.Collider.bounds.max.y - 0.6f)
            {
                brick.SetActiveCollider(false);
            }
        }
    }

    private void Brick_PositionChanged(Brick sender, int times)
    {
        // Change the sprite of the brick based on the number of times its position has changed.
        sender.Sprite = sprites[(times / (changeLevel / 10)) % sprites.Count];
    }
}
