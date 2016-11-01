using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class GameAdsManager : MonoBehaviour {

    ShowOptions showOptions;
    bool adsFinished;

    void Awake()
    {
        showOptions = new ShowOptions();
        if (Application.platform == RuntimePlatform.Android)
        {
            Advertisement.Initialize(DefineManager.androidAdsID, true);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Advertisement.Initialize(DefineManager.iosAdsID, true);
        }
        else
        {
            showOptions = null;
        }

        if(showOptions != null)
        {
            showOptions.resultCallback = OnAdsShowResultCallBack;
        }
    }

	// Use this for initialization
	void Start () {

        adsFinished = false;
	}

    void OnAdsShowResultCallBack(ShowResult showResult)
    {
        adsFinished = true;
    }

    public bool IsClientWatchedAds()
    {
        bool tempAdsFinished = adsFinished;
        adsFinished = false;
        return tempAdsFinished;
    }

    public void ShowUnityAdsToClient()
    {
        if(showOptions != null)
            Advertisement.Show(null, showOptions);
    }
}
