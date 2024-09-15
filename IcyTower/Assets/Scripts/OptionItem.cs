using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class OptionItem : SelectableItem
    {
        [SerializeField] private GameObject optionMenu;

        public override void Select()
        {
            optionMenu.SetActive(true);
            GameManager.Instance.SceneMenu.isActive = false;
        }
    }
}
