using UnityEngine;
using UnityEngine.UI;

public class UI_GameplayScreen : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        mainMenuButton.onClick.AddListener(OnUserMainMenu);
    }

    // Update is called once per frame
    void OnDisable()
    {
        mainMenuButton.onClick.RemoveAllListeners();
    }
    
    void OnUserMainMenu()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.MainMenu); //Esto esta mal, pero se hace al no tener otro lugar donde llamar a este evento.
        // En juegos mas completos se hace en el GameplayManager o GameController
    }
}
