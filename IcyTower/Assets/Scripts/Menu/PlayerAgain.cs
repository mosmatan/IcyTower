using UnityEngine;

public class PlayerAgain : MonoBehaviour
{
    public void OnClick()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.ResetGame();
    }
}
