using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyMine : MonoBehaviour
{
    public GameObject Mine;
    public int MineCost;
    public Text MineCostText;
    public int IsActive = 0; //IS INT BECAUSE PLAYERPREFS CANT STORE BOOL.
    public Miner MinerRef;

    void Start ()
    {
        MineCostText.text = "Buy Mine: " + MineCost;
        GetSaves.Instance.SaveAll += Save;
        if (PlayerPrefs.HasKey("IsActive"))
            Load();
        if(IsActive == 1)
        {
            Mine.SetActive(true);
            Destroy(gameObject);
        }
	}
	
    public void BuyMineButton()
    {
        if(PlayerStatManager.Instance.Money >= MineCost)
        {
            Mine.SetActive(true);
            PlayerStatManager.Instance.Money -= MineCost;
            IsActive = 1;
            Destroy(gameObject);
        }       
    }
	private void Save()
    {
        if(MinerRef.MinerNum == 0)
            PlayerPrefs.SetInt("IsActive", IsActive);
        else if (MinerRef.MinerNum == 1)
            PlayerPrefs.SetInt("1IsActive", IsActive);
        else if (MinerRef.MinerNum == 2)
            PlayerPrefs.SetInt("2IsActive", IsActive);
    }
    private void Load()
    {
        if (MinerRef.MinerNum == 0)
            IsActive = PlayerPrefs.GetInt("IsActive");
        else if (MinerRef.MinerNum == 1)
            IsActive = PlayerPrefs.GetInt("1IsActive");
        else if (MinerRef.MinerNum == 2)
            IsActive = PlayerPrefs.GetInt("2IsActive");
    }
}
