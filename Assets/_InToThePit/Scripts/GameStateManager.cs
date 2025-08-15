using UnityEngine;

public enum GameState //Estos se usan para hacer llamados al cual todos puedan suscribirse y recibir notificaciones de cambios de estado
{
    Loading,
    Loaded,
    Playing,
    Paused,
    GameOver,
    Victory,
    MainMenu,
    Store,
    Register,
    LogIn,
}

public class GameStateManager : MonoBehaviour
{
    public void Init()
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
