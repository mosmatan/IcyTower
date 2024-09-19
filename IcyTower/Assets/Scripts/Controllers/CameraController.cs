using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private IJumper jumper;
    [SerializeField] private GameOverMenu gameOverMenu;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < jumper.MaxHeight)
        {
            transform.position = new Vector3(0, jumper.MaxHeight, transform.position.z);
        }

        else if (transform.position.y > jumper.CurrentHeight + 6)
        {
            gameOverMenu.gameObject.SetActive(true);
        }
    }

}
