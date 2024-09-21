using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Abstract base class for selectable menu items.
/// </summary>
public abstract class SelectableItem : MonoBehaviour, ISelectableItem
{
    public virtual void Enter()
    {
        gameObject.SetActive(true); // Activate the item when selected.
    }

    public virtual void Leave()
    {
        gameObject.SetActive(false); // Deactivate the item when unselected.
    }

    public abstract void Select(); 
}