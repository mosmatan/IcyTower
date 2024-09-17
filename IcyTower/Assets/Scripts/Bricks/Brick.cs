using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public event Action PlayerPass;
    
    private RaycastHit2D hitRight;
    private RaycastHit2D hitLeft;
    
    [SerializeField] Collider2D collider;

    public Collider2D Collider => collider;

    public void SetActiveCollider(bool  active)
    {
        collider.enabled = active;
    }

    private void FixedUpdate()
    {
        hitRight = Physics2D.Raycast(transform.position, transform.right);
        hitLeft = Physics2D.Raycast(transform.position, -transform.right);
        
        if (hitRight.collider != null && hitRight.collider.tag == "Player" && transform.hasChanged)
        {
            transform.hasChanged = false;
            OnPlayerPass();
        }

        if (hitLeft.collider != null && hitLeft.collider.tag == "Player" && transform.hasChanged)
        {
            transform.hasChanged = false;
            OnPlayerPass();
        }
    }

    protected virtual void OnPlayerPass()
    {
        if (PlayerPass != null)
        {
            PlayerPass();
        }
    }
}
