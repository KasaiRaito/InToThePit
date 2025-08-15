using UnityEngine;
using UnityEngine.UI;

public class UI_SignUp : MonoBehaviour
{
    [SerializeField] Button cancelButton;
    [SerializeField] Button signUpButton;

    void OnEnable()
    {
        cancelButton.onClick.AddListener(OnUserCancel);
        
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
