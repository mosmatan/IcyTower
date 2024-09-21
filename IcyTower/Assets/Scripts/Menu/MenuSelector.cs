using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the selection of items in a menu interface.
/// </summary>
public class MenuSelector : MonoBehaviour
{
    [SerializeField] private List<SelectableItem> menuItems = new List<SelectableItem>(); 

    private int index = 0; // Current index of the selected item.
    public bool isActive { get; set; } = true; 

    private void Awake()
    {
        // Set this menu selector as the current scene menu in the GameManager.
        if (GameManager.Instance.SceneMenu == null)
        {
            GameManager.Instance.SceneMenu = this;
        }
    }

    private void OnDestroy()
    {
        // Clear the scene menu reference if this instance is being destroyed.
        if (GameManager.Instance.SceneMenu == this)
        {
            GameManager.Instance.SceneMenu = null;
        }
    }

    private void Update()
    {
        // If the menu is active, process input for menu navigation.
        if (isActive)
        {
            menu();
        }
    }

    private void menu()
    {
        // Navigate down the menu.
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            menuItems[index].Leave(); // Notify leaving current item.
            index = (++index) % menuItems.Count; // Move to the next item.
            menuItems[index].Enter(); 
        }
        // Navigate up the menu.
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            menuItems[index].Leave(); 
            if ((--index) < 0)
            {
                index = menuItems.Count - 1; // Loop to the last item if at the top.
            }
            menuItems[index].Enter(); 
        }
        // Select the current item.
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            menuItems[index].Select(); // Trigger the selection of the current item.
        }
    }
}
