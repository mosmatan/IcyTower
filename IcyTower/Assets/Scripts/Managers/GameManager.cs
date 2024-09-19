using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private IPlayerController playerController;

    [SerializeField] private GameObject playerControllerPref;
    [SerializeField] private IFadeScreen fadeScreen;

    private bool resetGameOnPrograss = false;
    public bool IsPlaying { get; private set; } = true;

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
        if(playerController != null)
        {
            Debug.Log("Destroy");
            Destroy(playerController.gameObject);
        }

        base.OnDestroy();
    }

    private void SceneManager_sceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        if(loadedScene.name == "GameScene")
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

        setActiveComponents(false);

        GameObject.FindWithTag("FadeScreen")?.TryGetComponent<IFadeScreen>(out fadeScreen);

        if (fadeScreen != null)
        {
            fadeScreen.Faded += FadeScreen_Faded;
            fadeScreen.Fading += FadeScreen_Fading;
        }

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
