using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Controls the camera's position based on the jumper's height.
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private IJumper jumper; // Reference to the jumper interface.
    [SerializeField] private GameOverMenu gameOverMenu; // Reference to the game over menu.

    private void Update()
    {
        // Adjust camera position based on jumper's max height.
        if (transform.position.y < jumper.MaxHeight)
        {
            transform.position = new Vector3(0, jumper.MaxHeight, transform.position.z);
        }
        // Show game over menu if jumper falls below a certain height.
        else if (transform.position.y > jumper.CurrentHeight + 6)
        {
            gameOverMenu.gameObject.SetActive(true);
        }
    }
}