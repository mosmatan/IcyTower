using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents a selectable item that starts the game when selected.
    /// </summary>
    public class StartGameItem : SelectableItem
    {
        [SerializeField] private SceneChanger changer; // Reference to the scene changer.

        public override void Select()
        {
            changer.OnClick("GameScene"); // Initiate the transition to the game scene.
        }
    }
}