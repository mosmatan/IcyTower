using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ObjectsList = new List<GameObject>();
    private IRelativePositionManager positionManager;
    private IJumper jumper;

    [SerializeField] private float nextObjectHeight;
    [SerializeField] private float offsetUnder;
    [SerializeField] private int boundries;

    private void Awake()
    {
        positionManager = new JumperRelativePositionManager(ObjectsList);
        positionManager.Boundries = boundries;
        positionManager.NextObjectDelta = nextObjectHeight;
        positionManager.MoveOffset = offsetUnder;
        positionManager.MovedObject += disableCollider;
    }

    private void Start()
    {
        jumper = GameObject.FindAnyObjectByType<Jumper>();
    }
    void Update()
    {
        positionManager.MoveObject();
        enableColliders();
    }

    private void disableCollider(GameObject brick)
    {
        brick.GetComponent<Collider2D>().enabled = false;
    }

    private void enableColliders()
    {
        foreach (GameObject brick in ObjectsList)
        {
            if(jumper.MaxHeight >= brick.transform.position.y)
            {
                brick.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
