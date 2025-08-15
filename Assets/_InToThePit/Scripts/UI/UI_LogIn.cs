using UnityEngine;
using UnityEngine.UI;

public class UI_LogIn : MonoBehaviour
{
    [SerializeField] Button cancelButton;
    //[SerializeField] Button loginButton;

    void OnEnable()
    {
        cancelButton.onClick.AddListener(OnUserCancel);
        //loginButton.onClick.AddListener();
    }

    void OnDisable()
    {
        cancelButton.onClick.RemoveAllListeners();
    }
    void OnUserCancel()
    {
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.MainMenu);
    }
}