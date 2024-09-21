using Assets.Scripts;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages game state, audio, player controller, and scene transitions.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public event Action<float> AudioVolumeChanged; // Event for audio volume changes.

    private IPlayerController playerController; // Reference to the player controller.

    [SerializeField] private GameObject playerControllerPref; 
    [SerializeField] private IFadeScreen fadeScreen; // Reference to fade screen.
    [SerializeField] private GameOverMenu gameOverMenu; // Reference to game over menu.
    [SerializeField] private GameObject pauseObject; // UI object for pause.
    [SerializeField] private int floorsForLevel = 100; // Floors for each level.

    private bool resetGameOnProgress = false; // Flag for resetting game.
    private bool sceneWithPause = false; 
    private bool isPlaying = true; 
    private bool isPaused = false; 
    private float audioVolume = 1; // Current audio volume level.

    private bool IsPlaying 
    {
        get { return isPlaying && !isPaused; } 
        set { isPlaying = value; isPaused = !value; }
    }

    public float AudioVolume 
    { 
        get { return audioVolume; } 
        set { audioVolume = value; OnAudioVolumeChanged(); } 
    }

    public int FloorsForLevel => (floorsForLevel / 10) * 10; 

    public KeyCode RightKey { get; set; } = KeyCode.RightArrow; // Key for moving right.
    public KeyCode LeftKey { get; set; } = KeyCode.LeftArrow; // Key for moving left.
    public KeyCode JumpKey { get; set; } = KeyCode.Space; // Key for jumping.
    public MenuSelector SceneMenu { get; set; } = null; // Reference to scene menu.

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded; // Subscribe to scene load events.
    }

    protected override void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded; // Unsubscribe from scene load events.
        base.OnDestroy();

        if (playerController != null)
        {
            Destroy(playerController.gameObject); 
        }
    }

    private void Update()
    {
        InputController(); // Handle input for game state.
    }

    private void InputController()
    {
        GameOverController(); // Check for game over conditions.
        PauseController(); // Check for pause conditions.
    }

    private void GameOverController()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPlaying)
            {
                IsPlaying = false; // Trigger game over.
                gameOverMenu?.gameObject.SetActive(true);
            }
        }
    }

    private void PauseController()
    {
        if (Input.GetKeyUp(KeyCode.P) && IsPlaying)
        {
            IsPlaying = false; // Pause the game.
            isPaused = true;
            pauseObject?.SetActive(true);
            Time.timeScale = 0f; // Stop the game time.
        }
        if (Input.GetKeyUp(KeyCode.Space) && isPaused)
        {
            IsPlaying = true; // Resume the game.
            pauseObject?.SetActive(false);
            Time.timeScale = 1f; // Resume game time.
        }
    }

    private void SceneManager_sceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        if (loadedScene.name == "GameScene")
        {
            setPlayerController(); // Set up the player controller for the game scene.
            findGameSceneUI(); 
        }

        setActiveComponents(false); 
        setSceneVolume(); // Set audio volume for the scene.
        findSceneUI(); 

        sceneWithPause = loadedScene.name == "GameScene"; // Determine if the scene supports pause.
        IsPlaying = true; // Set the game state to playing.
        resetGameOnProgress = false;
    }

    // Find UI elements for the scene.
    private void findSceneUI()
    {
        GameObject.FindWithTag("FadeScreen")?.TryGetComponent<IFadeScreen>(out fadeScreen); // Find fade screen.

        if (fadeScreen != null)
        {
            isPlaying = false; // Disable playing during fade.
            fadeScreen.Faded += FadeScreen_Faded; // Subscribe to fade events.
            fadeScreen.Fading += FadeScreen_Fading;
        }
    }

    // Find UI elements in the game scene.
    private void findGameSceneUI()
    {
        GameOverMenu[] gameOverMenuArr = Resources.FindObjectsOfTypeAll<GameOverMenu>(); // Find game over menu.
        if (gameOverMenuArr.Length > 0)
        {
            gameOverMenuArr[0].TryGetComponent<GameOverMenu>(out gameOverMenu);
        }

        TextMeshProUGUI[] pauseTextArr = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>(); // Find pause text.
        pauseObject = pauseTextArr.First(obj => obj.tag == "PauseText").gameObject;
    }

    private void setPlayerController()
    {
        if (playerController != null)
        {
            Destroy(playerController.gameObject); // Destroy existing player controller.
        }
        playerController = Instantiate(playerControllerPref, transform).GetComponent<IPlayerController>(); // Instantiate new player controller.
        playerController.JumpKey = JumpKey; // Set control keys.
        playerController.RightKey = RightKey;
        playerController.LeftKey = LeftKey;
    }

    private void setSceneVolume()
    {
        AudioSource[] audios = Resources.FindObjectsOfTypeAll<AudioSource>(); // Find all audio sources.
        AudioManager[] managers = Resources.FindObjectsOfTypeAll<AudioManager>(); // Find all audio managers.

        foreach (AudioManager manager in managers)
        {
            manager.Volume = AudioVolume; // Set audio manager volume.
        }

        foreach (AudioSource audio in audios)
        {
            audio.volume = AudioVolume; // Set audio source volume.
        }
    }

    private void FadeScreen_Fading()
    {
        setActiveComponents(false); // Disable components during fade.
        IsPlaying = false; 
    }

    private void FadeScreen_Faded()
    {
        setActiveComponents(true); // Enable components after fade.
        IsPlaying = true; 
    }

    private void setActiveComponents(bool active)
    {
        if (SceneMenu != null)
        {
            SceneMenu.isActive = active; // Activate/deactivate scene menu.
        }

        IsPlaying = active; 
    }

    public void ResetGame()
    {
        if (!resetGameOnProgress)
        {
            resetGameOnProgress = true; // Set the reset flag.
            StartCoroutine(resetGame()); 
        }
    }

    private IEnumerator resetGame()
    {
        if (fadeScreen != null)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.FadeIn(); // Fade in effect before resetting.
            yield return new WaitForSeconds(fadeScreen.Seconds + 1f);
        }

        Destroy(playerController.gameObject); // Destroy player controller.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene.
    }

    protected virtual void OnAudioVolumeChanged()
    {
        // Clamp audio volume between 0 and 1.
        if (audioVolume > 1)
        {
            audioVolume = 1;
        }
        else if (audioVolume < 0)
        {
            audioVolume = 0;
        }

        AudioVolumeChanged?.Invoke(audioVolume); // Trigger volume change event.
    }
}
