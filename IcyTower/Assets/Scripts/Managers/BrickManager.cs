using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
public class BrickManager : MonoBehaviour
{
    [SerializeField] private List<Brick> bricks = new List<Brick>();
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private IJumper jumper;

    [SerializeField] private float nextObjectHeight;
    [SerializeField] private float offsetUnder;
    [SerializeField] private int boundries;
   
    private int changeLevel;

    private IRelativePositionManager positionManager;

    private void Awake()
    {
        setPositionManager();

        foreach (Brick brick in bricks)
        {
            brick.PositionChanged += Brick_PositionChanged;
        }
    }

    private void Start()
    {
        changeLevel = GameManager.Instance.FloorsForLevel;
        DisableAllColliders();
    }

    void Update()
    {
        positionManager.MoveObject();
        EnableCollidersBasedOnPosition();
    }

    private void setPositionManager()
    {
        positionManager = new JumperRelativePositionManager(bricks.ConvertAll(brick => brick.gameObject));
        positionManager.Boundries = boundries;
        positionManager.NextObjectDelta = nextObjectHeight;
        positionManager.MoveOffset = offsetUnder;
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
            else if (jumper.MinBoundaryY < brick.Collider.bounds.max.y - 0.6f)
            {
                brick.SetActiveCollider(false);
            }
        }
    }

    private void Brick_PositionChanged(Brick sender, int times)
    {
        sender.Sprite = sprites[(times / (changeLevel / 10)) % sprites.Count];
    }
}
