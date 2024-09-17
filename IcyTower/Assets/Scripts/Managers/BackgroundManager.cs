using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ObjectsList = new List<GameObject>();
    private IRelativePositionManager positionManager;

    [SerializeField] private float nextObjectHeight;
    [SerializeField] private float offsetUnder;

    private void Awake()
    {
        positionManager = new JumperRelativePositionManager(ObjectsList);
        positionManager.NextObjectDelta = nextObjectHeight;
        positionManager.MoveOffset = offsetUnder;
    }
    void Update()
    {
        positionManager.MoveObject();
    }
}
