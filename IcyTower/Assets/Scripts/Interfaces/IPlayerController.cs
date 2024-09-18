using UnityEngine;

namespace Assets.Scripts
{
    public abstract class IPlayerController : MonoBehaviour
    {
        public abstract KeyCode RightKey { get; set; }
        public abstract KeyCode LeftKey { get; set; }
        public abstract KeyCode JumpKey { get; set; }

    }
}
