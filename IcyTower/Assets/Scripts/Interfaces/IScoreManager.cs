using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace Assets.Scripts.Interfaces
{
    public abstract class IScoreManager :MonoBehaviour
    {
        public abstract int Score { get; }
        public abstract int Floor { get; }
    }
}
