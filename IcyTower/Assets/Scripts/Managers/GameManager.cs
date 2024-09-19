using Assets.Scripts;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private IPlayerController playerController;

    [SerializeField] private GameObject playerControllerPref;
    [SerializeField] private IFadeScreen fadeScreen;
    [SerializeField] private GameOverMenu gameOverMenu;
    [SerializeField] private GameObject pauseObject;

    private bool resetGameOnPrograss = false;
    private bool sceneWithPause = false;
    private bool isPlaying = true;
    private bool isPaused = false;
    public bool IsPlaying 
    {
        get { return isPlaying && !isPaused; }
        private set { isPlaying = value; isPaused = value ? false : isPaused; }
    }

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
        base.OnDestroy();

        if (playerController != null)
        {
            Destroy(playerController.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                gameOverMenu?.gameObject.SetActive(true);
            }
        }

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
            if (playerController != null)
            {
                Destroy(playerController.gameObject);
                playerController = null;
            }
            playerController = GameObject.Instantiate(playerControllerPref, transform).GetComponent<IPlayerController>();
            playerController.JumpKey = JumpKey;
            playerController.RightKey = RightKey;
            playerController.LeftKey = LeftKey;

            GameOverMenu[] gameOverMenuArr = (Resources.FindObjectsOfTypeAll(typeof(GameOverMenu)) as GameOverMenu[]);

            if (gameOverMenuArr.Length > 0)
            {
                gameOverMenuArr[0].TryGetComponent<GameOverMenu>(out gameOverMenu);
            }

            GameObject[] pauseTextArr = (Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as GameObject[]);

            pauseObject = pauseTextArr.First(obj => obj.tag == "PauseText");
        }

        setActiveComponents(false);

        GameObject.FindWithTag("FadeScreen")?.TryGetComponent<IFadeScreen>(out fadeScreen);
        

        if (fadeScreen != null)
        {
            fadeScreen.Faded += FadeScreen_Faded;
            fadeScreen.Fading += FadeScreen_Fading;
        }

        sceneWithPause = loadedScene.name == "GameScene";

        IsPlaying = true;
        resetGameOnPrograss = false;
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
}
