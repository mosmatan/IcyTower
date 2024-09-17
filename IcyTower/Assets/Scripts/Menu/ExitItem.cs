using UnityEngine;

namespace Assets.Scripts
{
    internal class ExitItem : SelectableItem
    {
        public override void Select()
        {
            Application.Quit();
        }
    }
}
