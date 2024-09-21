using System;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IRelativePositionManager
    {
        //public event Action<GameObject> MovedObject;
        public float NextObjectDelta { get; set; }
        public float MoveOffset { get; set; }
        public float Boundries { get; set; }

        void MoveObject();
    }
}
