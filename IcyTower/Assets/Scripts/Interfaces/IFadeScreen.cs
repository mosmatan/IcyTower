using System;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class IFadeScreen : MonoBehaviour
    {
        public abstract event Action Faded;
        public abstract event Action Fading;
        public abstract float Seconds { get; }

        public abstract void FadeIn();
        public abstract void FadeOut();
        protected abstract void OnFaded();
        protected abstract void OnFading();
    }
}
