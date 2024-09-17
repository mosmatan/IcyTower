using UnityEngine;

namespace Assets.Scripts
{
    public abstract class IJumper: MonoBehaviour
    {
        public abstract float MaxHeight { get; }
        public abstract float MinBoundaryY { get; }
        public abstract float CurrentHeight { get; }
        public abstract void Jump();
        public abstract void Move(int direction);
    }
}
