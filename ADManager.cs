using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEngine.Monetization;
using UnityEngine.Advertisements;
public class ADManager : MonoBehaviour
{
   private string StoreID = "3211937";
   private string VideoID = "rewardedVideo";
    public GameObject ADButton;
    void Start()
    {
        //Monetization.Initialize (StoreID, false);//must be false
        Advertisement.Initialize(StoreID, false);
    }

    public void StartAD()
    {
        WatchAD(OnAdClose);
    }
    public void WatchAD(Action<ShowResult> callback)
    {
        
        if(Advertisement.IsReady(VideoID))
        {
            ADButton.SetActive(false);
            ShowOptions SO = new ShowOptions();
            SO.resultCallback = callback;
            Advertisement.Show(VideoID, SO);
        }
        
        /*
        if(Monetization.IsReady(VideoID))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(VideoID) as ShowAdPlacementContent;
            
            if(ad != null)
            {
                ad.Show();
                PlayerStatManager.Instance.Gems += 2;
                UIManager.Instance.UpdateGold();
            }
        }
        */
    }
    
    public void OnAdClose(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                PlayerStatManager.Instance.Gems += 2;
                UIManager.Instance.UpdateGold();
                ADButton.SetActive(true);
                break;
            default:
                break;

        }

    }
    
}
