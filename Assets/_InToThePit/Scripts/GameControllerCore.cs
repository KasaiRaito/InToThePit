using UnityEngine;

public class GameControllerCore : MonoBehaviour
{
    //Define session duration
    public float sessionTime;
    public float currentTime;

    private bool _isRunning;

    [SerializeField] UI_GameplayScreen _uiGameplayScreen;
    private float _fillAmount;

    [SerializeField] DataSaver _dataSaver;

    public void Init()
    {
        Debug.Log("GameControllerCore was initialized().");

        GameplayEventsHUD.onGameStateChanged.AddListener(ManageCurrentSession);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isRunning)
        {
            return;
        }

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            GameplayEventsHUD.onGameStateChanged.Invoke(GameState.GameOver);
        }

        _fillAmount = currentTime / sessionTime;
        _uiGameplayScreen.SetTimeBar(_fillAmount);

    }

    private void ManageCurrentSession(GameState gameState)
    {
        Debug.Log("GameControllerCore ManageCurrentSession(): " + gameState);

        //Set _isRunning to if we are on Playing State
        _isRunning = gameState == GameState.Playing;
    }

    public void ResetTimer()
    {
        currentTime = sessionTime;
    }

    public void SetJoyStickCanMovePlayer(bool canMove)
    {

    }

    public void PurchaseItemFromStore(int price, string name)
    {
        if (_dataSaver.GetTotalCoins() > price)
        {
            _dataSaver.AddCoins(-price);
            _dataSaver.AddToOwnedSkins(name);
            _dataSaver.SaveDataFn();
            Debug.Log("Purchased: " + name + " for " + price + " coins.");
            Debug.Log("New total coins: " + _dataSaver.GetTotalCoins());
        }
        else
        {
            Debug.Log("Not enough coins to purchase: " + name);

        }
    }

    public bool CheckIfOwned(string name)
    {
       return _dataSaver.GetOwnedSkins().Contains(name);
    }
}