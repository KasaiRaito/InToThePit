using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [Header("Game IDs (desde el Dashboard)")]
    [SerializeField] private string _androidGameId = "TU_GAME_ID_ANDROID";
    [SerializeField] private string _iOSGameId = "TU_GAME_ID_IOS";
    [SerializeField] private bool _testMode = true;

    public static event Action OnAdsInitialized;  // dispararemos esto al completar

    private string _gameId;

    void Awake()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#else
        _gameId = _androidGameId;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
            Debug.Log("[Ads] Inicializando...");
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("[Ads] Inicialización completa.");
        OnAdsInitialized?.Invoke();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"[Ads] Falló la inicialización: {error} - {message}");
    }
}
