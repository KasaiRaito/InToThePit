using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject UI_MenuScreen;
    public GameObject UI_GameplayScreen;
    
    private GameObject currentScreen;

    public void Init()
    {
        GameplayEventsHUD.onGameStateChanged.AddListener(OnGameStateChanged);
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
            GameState.Paused => UI_GameplayScreen,
            _ => null
        };

        foreach (GameObject screen in new GameObject[] { UI_MenuScreen, UI_GameplayScreen })
        {
            screen.SetActive(screen == currentScreen);
        }

        if (currentScreen == UI_MenuScreen)
        {
            
        }
        else if (currentScreen == UI_GameplayScreen)
        {
            
        }
        else
        {
            Debug.LogWarning($"No UI screen found for GameState: {newState}");
        }
    }
}
