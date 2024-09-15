using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
