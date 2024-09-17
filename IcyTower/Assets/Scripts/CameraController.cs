using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private IJumper jumper;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < jumper.MaxHeight)
        {
            transform.position = new Vector3(0, jumper.MaxHeight, transform.position.z);
        }

        else if (transform.position.y > jumper.CurrentHeight + 5)
        {
            //transform.position = new Vector3(0, jumper.CurrentHeight + 1, -10);
            GameManager.Instance.ResetGame();
        }
    }

}
