using UnityEngine;

namespace Assets.Scripts
{
    public interface IPlayerController
    {
        KeyCode RightKey { get; set; }
        KeyCode LeftKey { get; set; }
        KeyCode JumpKey { get; set; }

    }
}
