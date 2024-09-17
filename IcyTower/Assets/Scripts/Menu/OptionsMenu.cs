using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour 
{
    [SerializeField] InputField leftKeyIF;
    [SerializeField] InputField rightKeyIF;
    [SerializeField] InputField jumpKeyIF;

    private void Start()
    {
        leftKeyIF.onEndEdit.AddListener(SetLeftKey);
        rightKeyIF.onEndEdit.AddListener(SetRightKey);
        jumpKeyIF.onEndEdit.AddListener(SetJumpKey);
    }

    public void SetLeftKey(string input)
    {
        GameManager.Instance.LeftKey = (KeyCode)Enum.Parse(typeof(KeyCode), input.ToUpper());
        leftKeyIF.text = GameManager.Instance.LeftKey.ToString();
    }

    public void SetRightKey(string input)
    {
        GameManager.Instance.RightKey = (KeyCode)Enum.Parse(typeof(KeyCode), input.ToUpper());
        rightKeyIF.text = GameManager.Instance.RightKey.ToString();
    }

    public void SetJumpKey(string input)
    {
        GameManager.Instance.JumpKey = (KeyCode)Enum.Parse(typeof(KeyCode), input.ToUpper());
        jumpKeyIF.text = GameManager.Instance.JumpKey.ToString();
    }

    public void Save_Click()
    {
        Debug.Log(GameManager.Instance.LeftKey.ToString() + GameManager.Instance.RightKey.ToString() + GameManager.Instance.JumpKey.ToString());
        if (GameManager.Instance.SceneMenu != null)
        {
            GameManager.Instance.SceneMenu.isActive = true;
        }
        
        gameObject.SetActive(false);
    }
    
}
