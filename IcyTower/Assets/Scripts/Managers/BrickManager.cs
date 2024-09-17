using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BrickManager : MonoBehaviour
{
    [SerializeField] private List<Brick> bricks = new List<Brick>();
    [SerializeField] private IJumper jumper;

    [SerializeField] private float nextObjectHeight;
    [SerializeField] private float offsetUnder;
    [SerializeField] private int boundries;

    private IRelativePositionManager positionManager;

    private void Awake()
    {

        positionManager = new JumperRelativePositionManager(bricks.ConvertAll(brick => brick.gameObject));
        positionManager.Boundries = boundries;
        positionManager.NextObjectDelta = nextObjectHeight;
        positionManager.MoveOffset = offsetUnder;
    }

    private void Start()
    {
        DisableAllColliders();
    }

    void Update()
    {
        positionManager.MoveObject();
        EnableCollidersBasedOnPosition();
    }

    private void HandleMovedObject(GameObject brick)
    {
        DisableCollider(brick);
    }

    private void DisableAllColliders()
    {
        foreach (Brick brick in bricks)
        {
            brick.SetActiveCollider(false);
        }
    }

    private void DisableCollider(GameObject brick)
    {
        Collider2D collider = brick.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    private void EnableCollidersBasedOnPosition()
    {
        foreach (Brick brick in bricks)
        {
            // Enable collider if brick is in its correct position
            if (jumper.MinBoundaryY >= brick.Collider.bounds.max.y)
            {
                brick.SetActiveCollider(true);
            }
            else if (jumper.MinBoundaryY < brick.Collider.bounds.max.y - 0.1f)
            {
                brick.SetActiveCollider(false);
            }
        }
    }
}
