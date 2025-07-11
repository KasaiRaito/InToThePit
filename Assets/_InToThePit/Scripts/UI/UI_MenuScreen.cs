using UnityEngine;
using UnityEngine.UI;

public class UI_MenuScreen : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        startGameButton.onClick.AddListener(OnUserStartGame);
    }

    // Update is called once per frame
    void OnDisable()
    {
        startGameButton.onClick.RemoveAllListeners();
    }

    void OnUserStartGame()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.Playing); //Esto esta mal, pero se hace al no tener otro lugar donde llamar a este evento.
        // En juegos mas completos se hace en el GameplayManager o GameController
    }
}
