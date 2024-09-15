using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ObjectsList = new List<GameObject>();
    private CameraRelativePositionManager positionManager = new CameraRelativePositionManager();
    private IJumper jumper;

    [SerializeField] private float nextObjectHeight;
    [SerializeField] private float offsetUnder;
    [SerializeField] private int boundries;

    private void Awake()
    {
        positionManager.Boundries = boundries;
        positionManager.NextObjectHeight = nextObjectHeight;
        positionManager.OffsetUnder = offsetUnder;
        positionManager.ObjectsList = ObjectsList;
        positionManager.MovedObject += disableCollider;
        positionManager.Start();
    }

    private void Start()
    {
        jumper = GameObject.FindAnyObjectByType<Jumper>();
    }
    void Update()
    {
        positionManager.MoveObjectUp();
        enableColliders();
    }

    private void disableCollider(GameObject brick)
    {
        brick.GetComponent<Collider2D>().enabled = false;
        Debug.Log("World");
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
