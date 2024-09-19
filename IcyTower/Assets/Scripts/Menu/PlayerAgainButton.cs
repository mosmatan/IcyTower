using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgainButton : MonoBehaviour
{
    [SerializeField] 

    public void OnClick()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.ResetGame();
    }
}
