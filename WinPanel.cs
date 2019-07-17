using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinPanel : MonoBehaviour
{
    public Text GoldRewardText;
    public Text SoldierRewardText;
    
    public void SetUp(int GoldReward, int SoldierReward)
    {
        GoldRewardText.text = "GoldReward: " + GoldReward;
        if(SoldierReward != 0)
        SoldierRewardText.text = "Soldier Reward: " + SoldierReward;
    }
    public void ClosePanel()
    {
        CameraManager.Instance.StopAnims();
        WaveManager.Instance.WaveActive = false;
        gameObject.SetActive(false);
    }
}
