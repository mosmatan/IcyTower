using System.Diagnostics;

namespace Assets.Scripts
{
    public class OptionItem : SelectableItem
    {
        public override void Select()
        {
            Debug.WriteLine("Option Menu Open");
        }
    }
}
