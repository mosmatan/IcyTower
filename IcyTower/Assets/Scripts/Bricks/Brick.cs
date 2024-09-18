using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public event Action PlayerPass;
    public event Action PositionChanged;
    
    private RaycastHit2D hitRight;
    private RaycastHit2D hitLeft;
    private bool countScoreMode = true;
    
    [SerializeField] Collider2D collider;

    public Collider2D Collider => collider;

    public void SetActiveCollider(bool  active)
    {
        collider.enabled = active;
    }

    private void Update()
    {
        if(transform.hasChanged)
        {
            OnPositionChanged();
            transform.hasChanged = false;
            countScoreMode = true;
        }
    }

    private void FixedUpdate()
    {
        hitRight = Physics2D.Raycast(transform.position, transform.right);
        hitLeft = Physics2D.Raycast(transform.position, -transform.right);

        if (countScoreMode)
        {
            if (hitRight.collider != null && hitRight.collider.tag == "Player")
            {
                countScoreMode = false;
                OnPlayerPass();
            }

            if (hitLeft.collider != null && hitLeft.collider.tag == "Player")
            {
                countScoreMode = false;
                OnPlayerPass();
            }
        }
    }

    protected virtual void OnPlayerPass()
    {
        if (PlayerPass != null)
        {
            PlayerPass();
        }
    }

    protected virtual void OnPositionChanged()
    {
        if(PositionChanged != null)
        {
            PositionChanged();
        }
    }
}
