using UnityEngine;
using UnityEngine.UI;

public class UI_PauseScreen : MonoBehaviour
{
    [SerializeField] Button PauseButton;
    [SerializeField] Button ContinueButton;
    [SerializeField] Button MainmenuButton;
    void OnEnable()
    {
        PauseButton.onClick.AddListener(UnPauseGame);
        ContinueButton.onClick.AddListener(UnPauseGame);
        MainmenuButton.onClick.AddListener(MainMenu);
        
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    void UnPauseGame()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.Playing);
    }

    void MainMenu()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.MainMenu);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
