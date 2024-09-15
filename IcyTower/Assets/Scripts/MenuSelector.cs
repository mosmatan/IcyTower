using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] private List<SelectableItem> menuItems = new List<SelectableItem>();

    private int index = 0;

    private void Update()
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

        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            menuItems[index].Select();
        }
    }
}
