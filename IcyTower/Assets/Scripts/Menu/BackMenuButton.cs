using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenuButton : MonoBehaviour
{
    [SerializeField] private IFadeScreen fadeScreen;
    [SerializeField] private AudioManager audioManager;

    public void OnClick()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(backToMenu());
    }

    private IEnumerator backToMenu()
    {
        if (fadeScreen != null)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.FadeIn();
        }

        audioManager.sceneFadeOutAudio();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MenuScene");
    }
}
