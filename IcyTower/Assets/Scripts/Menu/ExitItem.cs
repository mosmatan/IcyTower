using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an item that exits the application when selected.
    /// </summary>
    internal class ExitItem : SelectableItem
    {
        public override void Select()
        {
            Application.Quit(); // Exit the application.
        }
    }
}