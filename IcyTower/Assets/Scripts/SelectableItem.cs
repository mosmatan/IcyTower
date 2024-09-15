using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SelectableItem : MonoBehaviour, ISelectableItem
{
    public virtual void Enter()
    {
        gameObject.SetActive(true);
    }

    public virtual void Leave()
    {
        gameObject.SetActive(false);
    }

    public abstract void Select();
}
