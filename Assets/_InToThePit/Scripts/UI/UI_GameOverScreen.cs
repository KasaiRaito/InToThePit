using UnityEngine;
using UnityEngine.UI;

public class UI_GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button gameOverButton;
    void OnEnable()
    {
        gameOverButton.onClick.AddListener(OnUserGameOver);
    }
    
    void OnDisable()
    {
        gameOverButton.onClick.RemoveAllListeners();
    }
    
    void OnUserGameOver()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.MainMenu); //Esto esta mal, pero se hace al no tener otro lugar donde llamar a este evento.
        // En juegos mas completos se hace en el GameplayManager o GameController
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
