using System;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public abstract class IScoreManager :MonoBehaviour
    {
        public abstract event Action<int> ComboChanged;

        public abstract int Score { get; }
        public abstract int Floor { get; }
        public abstract int Combo { get; }

        protected abstract void OnComboChanged();
    }
}
