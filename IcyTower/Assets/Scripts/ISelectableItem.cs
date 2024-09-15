using System;
using System.Collections.Generic;
using System.Linq;
namespace Assets.Scripts
{
    public interface ISelectableItem
    {
        void Select();

        void Enter(); 
        
        void Leave();
    }
}
