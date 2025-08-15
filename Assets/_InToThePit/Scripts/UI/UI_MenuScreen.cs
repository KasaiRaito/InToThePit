using UnityEngine;
using UnityEngine.UI;

public class UI_MenuScreen : MonoBehaviour
{
    /*
     * This code controlles the UI in MainMenu
     */
    
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button storeButton;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button logInButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        startGameButton.onClick.AddListener(OnUserStartGame);
        registerButton.onClick.AddListener(OnUserRegister);
        logInButton.onClick.AddListener(OnUserLogIn);
        storeButton.onClick.AddListener(OnUserStore);
    }

    // Update is called once per frame
    void OnDisable()
    {
        startGameButton.onClick.RemoveAllListeners();
        registerButton.onClick.RemoveAllListeners();
        logInButton.onClick.RemoveAllListeners();
        storeButton.onClick.RemoveAllListeners();
        FindAnyObjectByType<GameControllerCore>().ResetTimer(); //Pasar al UI manager
    }

    void OnUserStartGame()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.Playing); //Esto esta mal, pero se hace al no tener otro lugar donde llamar a este evento.
        // En juegos mas completos se hace en el GameplayManager o GameController
    }

    void OnUserStore()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.Store);
    }

    void OnUserRegister()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.Register);
    }

    void OnUserLogIn()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.LogIn);
    }
}
