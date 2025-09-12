using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject UI_MenuScreen;
    public GameObject UI_GameplayScreen;
    public GameObject UI_GameOverScreen;
    public GameObject UI_PauseScreen;
    public GameObject UI_Store;
    public GameObject UI_LoginScreen;
    public GameObject UI_RegisterScreen;
    public MyJoyStick JoyStick;
    
    private GameObject _currentScreen;
    private GameObject _preScreen;
    
    //Ocupa mas memoria, pero es limpío en sintaxis
    private GameObject[] UIs;
    
    [SerializeField] private InterstitialAdExample _interstitial;

    public void Init()
    {
        GameplayEventsHUD.onGameStateChanged.AddListener(OnGameStateChanged);
        UIs = new GameObject[] { UI_MenuScreen, UI_GameplayScreen, UI_GameOverScreen, UI_PauseScreen, UI_Store,
            UI_LoginScreen, UI_RegisterScreen };
    }

    public void OnDisable()
    {
        GameplayEventsHUD.onGameStateChanged.RemoveListener(OnGameStateChanged);
        UIs = null;
    }

    void OnGameStateChanged(GameState newState)
    {
        _currentScreen = newState switch
        {
            GameState.MainMenu => UI_MenuScreen,
            GameState.Playing => UI_GameplayScreen,
            GameState.Paused => UI_PauseScreen,
            GameState.GameOver => UI_GameOverScreen,
            GameState.Register => UI_RegisterScreen,
            GameState.LogIn => UI_LoginScreen,
            GameState.Store => UI_Store,
            _ => null
        };

        if (_currentScreen == UI_MenuScreen)
        {
            SetActiveScreen();
            JoyStick.SetCanMovePlayer(false);
            ShowAdd();
            //SceneManager.LoadScene("SampleScene");
        }
        else if (_currentScreen == UI_GameplayScreen)
        {
            SetActiveScreen();
            JoyStick.SetCanMovePlayer(true);
        }
        else if (_currentScreen == UI_GameOverScreen)
        {
            SetActiveScreen();
            JoyStick.SetCanMovePlayer(false);
        }
        else if (_currentScreen == UI_PauseScreen)
        {
            SetActiveScreen(UI_GameplayScreen);
            JoyStick.SetCanMovePlayer(false);
        }
        else if (_currentScreen == UI_Store)
        {
            SetActiveScreen();
            JoyStick.SetCanMovePlayer(false);
        }
        //LOG IN
        else if (_currentScreen == UI_LoginScreen)
        {
            SetActiveScreen();
            JoyStick.SetCanMovePlayer(false);
        }
        else if (_currentScreen == UI_RegisterScreen)
        {
            SetActiveScreen();
            JoyStick.SetCanMovePlayer(false);
        }
        else
        {
            Debug.LogWarning($"No UI screen found for GameState: {newState}");
        }
        
    }

    void SetActiveScreen(GameObject exeption = null)
    {
        foreach (var ui in UIs)
        {
            ui.SetActive(ui == _currentScreen || ui == exeption);
        }
    }
    
    void ShowAdd()
    {
        _interstitial.ShowAd();
    }
}
