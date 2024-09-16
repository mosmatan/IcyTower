using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private IPlayerController playerController;

    [SerializeField] private GameObject playerControllerPref;

    public KeyCode RightKey { get; set; } = KeyCode.D;
    public KeyCode LeftKey { get; set; } = KeyCode.A;
    public KeyCode JumpKey { get; set; } = KeyCode.Space;
    public MenuSelector SceneMenu { get; set; } = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
