using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private IFadeScreen fadeScreen;
    [SerializeField] private AudioManager audioManager;

    private string nextSceneName;

    private void Start()
    {
        fadeScreen.Faded += loadNextScene;
    }

    public void OnClick(string sceneName)
    {
        nextSceneName = sceneName;
        Time.timeScale = 1.0f;
        backToMenu();
    }

    private void backToMenu()
    {
        if (fadeScreen != null)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.FadeIn();
        }

        audioManager.sceneFadeOutAudio();
    }

    private void loadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
