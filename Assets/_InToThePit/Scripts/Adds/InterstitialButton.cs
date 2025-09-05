// En un botón:
using UnityEngine;

public class InterstitialButton : MonoBehaviour
{
    [SerializeField] private InterstitialAdExample _interstitial;

    public void OnClickShowInterstitial()
    {
        _interstitial.ShowAd();
    }
}
