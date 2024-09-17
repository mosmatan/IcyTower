using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] private List<SelectableItem> menuItems = new List<SelectableItem>();

    private int index = 0;
    public bool isActive { get; set; } = true;

    private void Awake()
    {
        if(GameManager.Instance.SceneMenu == null)
        {
            GameManager.Instance.SceneMenu = this;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.SceneMenu == this)
        {
            GameManager.Instance.SceneMenu = null;
        }
    }

    private void Update()
    {
        if (isActive)
        {
            menu();
        }
    }

    private void menu()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            menuItems[index].Leave();

            index = (++index) % menuItems.Count;

            menuItems[index].Enter();
        }

        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            menuItems[index].Leave();

            if ((--index) < 0)
            {
                index = menuItems.Count - 1;
            }

            menuItems[index].Enter();
        }

        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            menuItems[index].Select();
        }
    }
}
