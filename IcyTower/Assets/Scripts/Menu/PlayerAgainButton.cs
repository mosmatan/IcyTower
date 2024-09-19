using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgainButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.ResetGame();
    }
}
