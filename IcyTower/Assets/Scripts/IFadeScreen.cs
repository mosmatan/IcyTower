using UnityEngine;

namespace Assets.Scripts
{
    public abstract class IFadeScreen : MonoBehaviour
    {
        public abstract int Seconds { get; }

        public abstract void FadeIn();
        public abstract void FadeOut();
    }
}
