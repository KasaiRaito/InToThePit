using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdExample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSAdUnitId = "Interstitial_iOS";
    private string _adUnitId;
    private bool _isLoaded = false;

    void Awake()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#else
        _adUnitId = _androidAdUnitId;
#endif
    }

    void OnEnable()
    {
        AdsInitializer.OnAdsInitialized += HandleAdsInitialized;
    }

    void OnDisable()
    {
        AdsInitializer.OnAdsInitialized -= HandleAdsInitialized;
    }

    private void HandleAdsInitialized()
    {
        LoadAd();
    }

    // Puedes llamar esto manualmente si prefieres (por botón, etc.)
    public void LoadAd()
    {
        if (!Advertisement.isInitialized)
        {
            Debug.Log("[Ads] Aún no inicializado, no cargo.");
            return;
        }

        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        if (!_isLoaded)
        {
            Debug.Log("[Ads] Interstitial no cargado aún.");
            return;
        }

        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Callbacks de carga
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("[Ads] Interstitial cargado.");
        _isLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"[Ads] Error al cargar {adUnitId}: {error} - {message}");
        _isLoaded = false;
        // Ejemplo: reintento simple
        // Invoke(nameof(LoadAd), 2f);
    }

    // Callbacks de show
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"[Ads] Error al mostrar {adUnitId}: {error} - {message}");
        _isLoaded = false;
        // Si quieres, recarga otro anuncio:
        // LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"[Ads] Completado con estado: {showCompletionState}");
        _isLoaded = false;
        // Normalmente se vuelve a cargar para el próximo uso:
        LoadAd();
    }
}
