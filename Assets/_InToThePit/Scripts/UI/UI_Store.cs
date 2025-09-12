using UnityEngine;
using UnityEngine.UI;

public class UI_Store : MonoBehaviour
{
    [SerializeField] Button cancelButton;
    //[SerializeField] Button loginButton;
    [SerializeField] GameObject shelf;

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
        CleanShelf();
        GameplayEventsHUD.onGameStateChanged.Invoke(GameState.MainMenu);
    }

    void CleanShelf()
    {
        foreach (Transform child in shelf.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
