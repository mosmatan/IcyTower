using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an option item in the menu that opens an option menu when selected.
    /// </summary>
    public class OptionItem : SelectableItem
    {
        [SerializeField] private GameObject optionMenu; // Reference to the option menu GameObject.

        public override void Select()
        {
            optionMenu.SetActive(true); // Activate the option menu.
            GameManager.Instance.SceneMenu.isActive = false; // Deactivate the current scene menu.
        }
    }
}