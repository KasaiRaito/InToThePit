using UnityEngine.Events; // Using Unity's built-in event system

public static class GameplayEventsHUD
{
    //We can use UnityEvents to allow other scripts to subscribe to these events
    // We make an event for each action we want to trigger
    
    public static UnityEvent<GameState> onGameStateChanged = new UnityEvent<GameState>();
    
    //variable templatizada 
}
