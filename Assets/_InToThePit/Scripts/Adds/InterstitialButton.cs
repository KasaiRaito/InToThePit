// En un bot�n:
using UnityEngine;

public class InterstitialButton : MonoBehaviour
{
    [SerializeField] private InterstitialAdExample _interstitial;

    public void OnClickShowInterstitial()
    {
        _interstitial.ShowAd();
    }
}
