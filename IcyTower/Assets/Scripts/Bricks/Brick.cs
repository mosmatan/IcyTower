using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] Collider2D collider;

    public Collider2D Collider => collider;

    public void SetActiveCollider(bool  active)
    {
        collider.enabled = active;
    }
}
