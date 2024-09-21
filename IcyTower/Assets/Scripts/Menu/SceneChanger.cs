using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene transitions with fade effects.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [SerializeField] private IFadeScreen fadeScreen; // Reference to the fade screen.
    [SerializeField] private AudioManager audioManager; // Reference to the audio manager.

    private string nextSceneName; // Name of the next scene to load.

    private void Start()
    {
        fadeScreen.Faded += loadNextScene; 
    }

    public void OnClick(string sceneName)
    {
        nextSceneName = sceneName; // Set the next scene name.
        Time.timeScale = 1.0f; // Ensure the game is not paused.
        backToMenu(); // Start the transition process.
    }

    private void backToMenu()
    {
        if (fadeScreen != null)
        {
            fadeScreen.gameObject.SetActive(true); 
            fadeScreen.FadeIn(); // Start fading in.
        }

        audioManager.sceneFadeOutAudio(); // Fade out the audio.
    }

    private void loadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // Load the next scene.
        }
    }
}