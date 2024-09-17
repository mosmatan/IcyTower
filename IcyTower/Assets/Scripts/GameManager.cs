using Assets.Scripts;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private IPlayerController playerController;

    [SerializeField] private GameObject playerControllerPref;
    [SerializeField] private IFadeScreen fadeScreen;

    private bool resetGameOnPrograss = false;

    public KeyCode RightKey { get; set; } = KeyCode.D;
    public KeyCode LeftKey { get; set; } = KeyCode.A;
    public KeyCode JumpKey { get; set; } = KeyCode.Space;
    public MenuSelector SceneMenu { get; set; } = null;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    private void SceneManager_sceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        if(loadedScene.name == "GameScene")
        {
            playerController = GameObject.Instantiate(playerControllerPref).GetComponent<IPlayerController>();
            playerController.JumpKey = JumpKey;
            playerController.RightKey = RightKey;
            playerController.LeftKey = LeftKey;
        }

        fadeScreen = GameObject.FindWithTag("FadeScreen").GetComponent<IFadeScreen>();
        resetGameOnPrograss = false;
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
            float seconds = (float)fadeScreen.Seconds;
            fadeScreen.FadeIn();
            yield return new WaitForSeconds(seconds + 0.5f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
