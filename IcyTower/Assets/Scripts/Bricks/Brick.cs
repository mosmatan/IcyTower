using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public event Action PlayerPass;
    public event Action<Brick, int> PositionChanged;

    private RaycastHit2D hitRight;
    private RaycastHit2D hitLeft;
    private bool countScoreMode = true;
    private int positionChangeCounter = -1;
    

    [SerializeField] private Collider2D collider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Collider2D Collider => collider;
    public Sprite Sprite { get { return spriteRenderer.sprite; } set { spriteRenderer.sprite = value; } }

    public void SetActiveCollider(bool  active)
    {
        collider.enabled = active;
    }

    private void Update()
    {
        handlePositionChange();
        handlePlayerPass();
    }

    private void handlePlayerPass()
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

    private void handlePositionChange()
    {
        if (transform.hasChanged)
        {
            OnPositionChanged();
            transform.hasChanged = false;
            countScoreMode = true;
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
        positionChangeCounter++;

        if(PositionChanged != null)
        {
            PositionChanged(this, positionChangeCounter);
        }
    }

    public void AddPostionChangedTime()
    {
        positionChangeCounter++;
    }
}
