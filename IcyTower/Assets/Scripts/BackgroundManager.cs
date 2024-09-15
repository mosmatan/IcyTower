using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ObjectsList = new List<GameObject>();
    private CameraRelativePositionManager positionManager = new CameraRelativePositionManager();

    [SerializeField] private float nextObjectHeight;
    [SerializeField] private float offsetUnder;

    private void Awake()
    {
        positionManager.NextObjectHeight = nextObjectHeight;
        positionManager.OffsetUnder = offsetUnder;
        positionManager.ObjectsList = ObjectsList;
        positionManager.Start();
    }
    void Update()
    {
        positionManager.MoveObjectUp();
    }
}
