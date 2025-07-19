using UnityEngine;
using UnityEngine.UI;

/*
 * This code controlles the UI in Gameplay
 */

public class UI_GameplayScreen : MonoBehaviour
{
    //BUTTONS
    [SerializeField] private Button pauseGameButton;
    //STICK
    [SerializeField] private MyJoyStick joyStick;
    
    //IMAGES
    [SerializeField] private Image timeBar;


    void Start()
    {
        
    }
    
    void OnEnable()
    {
        pauseGameButton.onClick.AddListener(PauseGame);
        joyStick.SetCanMovePlayer(true);
    }
    
    void OnDisable()
    {
        pauseGameButton.onClick.RemoveAllListeners();
        joyStick.SetCanMovePlayer(false);
    }
    
    void OnUserMainMenu()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.MainMenu); //Esto esta mal, pero se hace al no tener otro lugar donde llamar a este evento.
        // En juegos mas completos se hace en el GameplayManager o GameController
    }

    void PauseGame()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.Paused);
    }

    public void SetTimeBar(float fillPercent)
    {
        timeBar.fillAmount =  fillPercent;
    }
}
