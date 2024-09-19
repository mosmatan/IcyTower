using Assets.Scripts;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action<float> AudioVolumeChanged;

    private IPlayerController playerController;

    [SerializeField] private GameObject playerControllerPref;
    [SerializeField] private IFadeScreen fadeScreen;
    [SerializeField] private GameOverMenu gameOverMenu;
    [SerializeField] private GameObject pauseObject;

    private bool resetGameOnPrograss = false;
    private bool sceneWithPause = false;
    private bool isPlaying = true;
    private bool isPaused = false;
    private float audioVolume = 1;
    public bool IsPlaying 
    {
        get { return isPlaying && !isPaused; }
        private set { isPlaying = value; isPaused = value ? false : isPaused; }
    }
    public float AudioVolume { get { return audioVolume; } set { audioVolume = value; OnAudioVolumeChanged(); } }

    public KeyCode RightKey { get; set; } = KeyCode.RightArrow;
    public KeyCode LeftKey { get; set; } = KeyCode.LeftArrow;
    public KeyCode JumpKey { get; set; } = KeyCode.Space;
    public MenuSelector SceneMenu { get; set; } = null;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    protected override void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;

        base.OnDestroy();

        if (playerController != null)
        {
            Destroy(playerController.gameObject);
        }
    }

    private void Update()
    {
        inputController();
    }

    private void inputController()
    {
        gameOverController();
        pauseController();
    }

    private void gameOverController()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                gameOverMenu?.gameObject.SetActive(true);
            }
        }
    }
    
    private void pauseController()
    {
        if (Input.GetKeyUp(KeyCode.P) && IsPlaying)
        {
            IsPlaying = false;
            isPaused = true;
            pauseObject?.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && isPaused)
        {
            IsPlaying = true;
            pauseObject?.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void SceneManager_sceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        if(loadedScene.name == "GameScene")
        {
            setPlayerController();
            findGameSceneUI();
        }

        setActiveComponents(false);
        setSceneVolume();
        findSceneUI();

        sceneWithPause = loadedScene.name == "GameScene";
        IsPlaying = true;
        resetGameOnPrograss = false;
    }

    private void findSceneUI()
    {
        GameObject.FindWithTag("FadeScreen")?.TryGetComponent<IFadeScreen>(out fadeScreen);

        if (fadeScreen != null)
        {
            fadeScreen.Faded += FadeScreen_Faded;
            fadeScreen.Fading += FadeScreen_Fading;
        }
    }

    private void findGameSceneUI()
    {
        GameOverMenu[] gameOverMenuArr = (Resources.FindObjectsOfTypeAll(typeof(GameOverMenu)) as GameOverMenu[]);

        if (gameOverMenuArr.Length > 0)
        {
            gameOverMenuArr[0].TryGetComponent<GameOverMenu>(out gameOverMenu);
        }

        TextMeshProUGUI[] pauseTextArr = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as TextMeshProUGUI[];

        pauseObject = pauseTextArr.First(obj => obj.tag == "PauseText").gameObject;
    }

    private void setPlayerController()
    {
        if (playerController != null)
        {
            Destroy(playerController.gameObject);
        }
        playerController = GameObject.Instantiate(playerControllerPref, transform).GetComponent<IPlayerController>();
        playerController.JumpKey = JumpKey;
        playerController.RightKey = RightKey;
        playerController.LeftKey = LeftKey;
    }

    private void setSceneVolume()
    {
        AudioSource[] audios = Resources.FindObjectsOfTypeAll<AudioSource>();
        AudioManager[] managers = Resources.FindObjectsOfTypeAll<AudioManager>();

        foreach (AudioManager manager in managers)
        {
            manager.Volume = AudioVolume;
        }

        foreach (AudioSource audio in audios)
        {
            audio.volume = AudioVolume;
        }
    }

    private void FadeScreen_Fading()
    {
        setActiveComponents(false);
    }

    private void FadeScreen_Faded()
    {
        setActiveComponents(true);
    }

    private void setActiveComponents(bool active)
    {
        if (SceneMenu != null)
        {
            SceneMenu.isActive = active;
        }

        IsPlaying = active;
    }

    public void ResetGame()
    {
        if (!resetGameOnPrograss)
        {
            resetGameOnPrograss = true;

            StartCoroutine(resetGame());
        }
    }

    private IEnumerator resetGame()
    {
        if (fadeScreen != null)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.FadeIn();
            yield return new WaitForSeconds(fadeScreen.Seconds + 1f);
        }

        Destroy(playerController.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected virtual void OnAudioVolumeChanged()
    {
        if (audioVolume > 1)
        {
            audioVolume = 1;
        }
        else if (audioVolume < 0)
        {
            audioVolume = 0;
        }

        if(AudioVolumeChanged != null)
        {
            AudioVolumeChanged(audioVolume);
        }
    }
}
