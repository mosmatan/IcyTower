using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour 
{
    [SerializeField] private Scrollbar audioScrollbar;

    private void Start()
    {
        audioScrollbar.value = GameManager.Instance.AudioVolume;
    }

    public void AudioScrollValueChanged()
    {
        GameManager.Instance.AudioVolume = audioScrollbar.value;
    }

    public void Return()
    {
        GameManager.Instance.SceneMenu.isActive = true;
        gameObject.SetActive(false);
    }
}
