using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeMine : MonoBehaviour
{
    public int MineRateCost;
    public int MineAmountCost;
    public int GemChanceCost;
    public Miner MinerRef;
    public Text MineRateCostText;
    public Text MineAmountText;
    public Text GemChangeText;
    public Text TotalMineAmountText;
    public Text TotalMineSpeedText;
    public Text TotalGemChanceText;
    public GameObject UpgradePanel;
    private void Start()
    {
        
        if(MinerRef.MinerNum == 0)
        {
            GetSaves.Instance.SaveAll += MinerRef.Save;//For some reason putting this event subscription in the actual miner class wouldnt subscribe it for each miner, this seems to be a quick fix. No idea why it works here and not in the actual miner class.
            if(PlayerPrefs.GetInt("IsActive") == 0)
            MinerRef.gameObject.SetActive(false);
        }           
        else if (MinerRef.MinerNum == 1)
        {
            GetSaves.Instance.SaveAll += MinerRef.Save;//^^
            if (PlayerPrefs.GetInt("1IsActive") == 0)
            MinerRef.gameObject.SetActive(false);
        }           
        else if (MinerRef.MinerNum == 2)
        {
            GetSaves.Instance.SaveAll += MinerRef.Save;//^^
            if(PlayerPrefs.GetInt("2IsActive") == 0)
            MinerRef.gameObject.SetActive(false);
        }
        
        UpgradePanel.SetActive(false);
        GetSaves.Instance.SaveAll += Save;
        if (PlayerPrefs.HasKey("MineRateCost"))
            Load();
        else
        {
            MineRateCostText.text = "Gold: " + MineRateCost;
            MineAmountText.text = "Gold: " + MineAmountCost;
            TotalMineAmountText.text = "Mine Amount: " + MinerRef.GoldAmount;
            TotalMineSpeedText.text = "Miner Speed: " + MinerRef.Speed / 100;
            GemChangeText.text = "Gold: " + GemChanceCost;
            TotalGemChanceText.text = "Gem Chance: " + MinerRef.GemChance;
        }  
    }

    public void UpgradeMineAmount()
    {
        if(PlayerStatManager.Instance.Money >= MineAmountCost)
        {
            MinerRef.GoldAmount += 6;
            PlayerStatManager.Instance.Money -= MineAmountCost;
            MineAmountCost = MineAmountCost += 255;
            MineAmountText.text = "Gold: " + MineAmountCost;
            TotalMineAmountText.text = "Mine Amount: " + MinerRef.GoldAmount;
            UIManager.Instance.UpdateGold();
            GetSaves.Instance.SaveGame();
        }
    }

    public void UpgradeMineRate()
    {
        if (PlayerStatManager.Instance.Money >= MineRateCost)
        {
            //MinerRef.Anim.speed += 0.2f;
            MinerRef.Speed += 10;
            MinerRef.WaitTime -= .1f;
            PlayerStatManager.Instance.Money -= MineRateCost;
            MineRateCost = MineRateCost += 255;
            MineRateCostText.text = "Gold: " + MineRateCost;
            TotalMineSpeedText.text = "Miner Speed: " + MinerRef.Speed / 100;
            UIManager.Instance.UpdateGold();
            GetSaves.Instance.SaveGame();
        }
    }

    public void UpgradeGemChance()
    {
        if (PlayerStatManager.Instance.Money >= GemChanceCost)
        {
            MinerRef.GemChance += .5f;
            PlayerStatManager.Instance.Money -= GemChanceCost;
            GemChanceCost = GemChanceCost += 255;
            GemChangeText.text = "Gold: " + GemChanceCost;
            TotalGemChanceText.text = "Gem Chance: " + MinerRef.GemChance;
            UIManager.Instance.UpdateGold();
            GetSaves.Instance.SaveGame();
        }
    }

    public void OpenPanel()
    {
        UpgradePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        UpgradePanel.SetActive(false);
    }

    private void Save()
    {
        if (MinerRef.MinerNum == 0)
        {
            PlayerPrefs.SetInt("MineRateCost", MineRateCost);
            PlayerPrefs.SetInt("MineAmountCost", MineAmountCost);
            PlayerPrefs.SetInt("GemChanceCost", GemChanceCost);
            PlayerPrefs.SetFloat("WaitTime", MinerRef.WaitTime);
        }
        else if (MinerRef.MinerNum == 1)
        {
            PlayerPrefs.SetInt("1MineRateCost", MineRateCost);
            PlayerPrefs.SetInt("1MineAmountCost", MineAmountCost);
            PlayerPrefs.SetInt("1GemChanceCost", GemChanceCost);
            PlayerPrefs.SetFloat("1WaitTime", MinerRef.WaitTime);
        }
        else if (MinerRef.MinerNum == 2)
        {
            PlayerPrefs.SetInt("2MineRateCost", MineRateCost);
            PlayerPrefs.SetInt("2MineAmountCost", MineAmountCost);
            PlayerPrefs.SetInt("2GemChanceCost", GemChanceCost);
            PlayerPrefs.SetFloat("2WaitTime", MinerRef.WaitTime);
        }
    }
    private void Load()
    {
        if (MinerRef.MinerNum == 0)
        {
            MineRateCost = PlayerPrefs.GetInt("MineRateCost");
            MineAmountCost = PlayerPrefs.GetInt("MineAmountCost");
            GemChanceCost = PlayerPrefs.GetInt("GemChanceCost");
            MinerRef.WaitTime = PlayerPrefs.GetFloat("WaitTime");
            MineRateCostText.text = "Gold: " + MineRateCost;
            MineAmountText.text = "Gold: " + MineAmountCost;
            TotalMineAmountText.text = "Mine Amount: " + MinerRef.GoldAmount;
            TotalMineSpeedText.text = "Miner Speed: " + MinerRef.Speed / 100;
            GemChangeText.text = "Gold: " + GemChanceCost;
            TotalGemChanceText.text = "Gem Chance: " + MinerRef.GemChance;
        }
        else if (MinerRef.MinerNum == 1)
        {
            MineRateCost = PlayerPrefs.GetInt("1MineRateCost");
            MineAmountCost = PlayerPrefs.GetInt("1MineAmountCost");
            GemChanceCost = PlayerPrefs.GetInt("1GemChanceCost");
            MinerRef.WaitTime = PlayerPrefs.GetFloat("1WaitTime");
            MineRateCostText.text = "Gold: " + MineRateCost;
            MineAmountText.text = "Gold: " + MineAmountCost;
            TotalMineAmountText.text = "Mine Amount: " + MinerRef.GoldAmount;
            TotalMineSpeedText.text = "Miner Speed: " + MinerRef.Speed / 100;
            GemChangeText.text = "Gold: " + GemChanceCost;
            TotalGemChanceText.text = "Gem Chance: " + MinerRef.GemChance;
        }
        else if (MinerRef.MinerNum == 2)
        {
            MineRateCost = PlayerPrefs.GetInt("2MineRateCost");
            MineAmountCost = PlayerPrefs.GetInt("2MineAmountCost");
            GemChanceCost = PlayerPrefs.GetInt("2GemChanceCost");
            MinerRef.WaitTime = PlayerPrefs.GetFloat("2WaitTime");
            MineRateCostText.text = "Gold: " + MineRateCost;
            MineAmountText.text = "Gold: " + MineAmountCost;
            TotalMineAmountText.text = "Mine Amount: " + MinerRef.GoldAmount;
            TotalMineSpeedText.text = "Miner Speed: " + MinerRef.Speed / 100;
            GemChangeText.text = "Gold: " + GemChanceCost;
            TotalGemChanceText.text = "Gem Chance: " + MinerRef.GemChance;
        }

    }
}
