using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject UI_MenuScreen;
    public GameObject UI_GameplayScreen;
    public GameObject UI_GameOverScreen;
    public GameObject UI_PauseScreen;
    
    private GameObject currentScreen;
    
    //Ocupa mas memoria, pero es limpío en sintaxis
    private GameObject[] UIs;

    public void Init()
    {
        GameplayEventsHUD.onGameStateChanged.AddListener(OnGameStateChanged);
        UIs = new GameObject[] { UI_MenuScreen, UI_GameplayScreen, UI_GameOverScreen, UI_PauseScreen };
    }

    public void OnDisable()
    {
        GameplayEventsHUD.onGameStateChanged.RemoveListener(OnGameStateChanged);
    }

    void OnGameStateChanged(GameState newState)
    {
        currentScreen = newState switch
        {
            GameState.MainMenu => UI_MenuScreen,
            GameState.Playing => UI_GameplayScreen,
            GameState.Paused => UI_PauseScreen,
            GameState.GameOver => UI_GameOverScreen,
            _ => null
        };

        foreach (GameObject screen in UIs)
        {
            screen.SetActive(screen == currentScreen);
        }

        if (currentScreen == UI_MenuScreen)
        {
            
        }
        else if (currentScreen == UI_GameplayScreen)
        {
            
        }
        else if (currentScreen == UI_GameOverScreen)
        {
            
        }
        else if (currentScreen == UI_PauseScreen)
        {
            
        }
        else
        {
            Debug.LogWarning($"No UI screen found for GameState: {newState}");
        }
    }
}
