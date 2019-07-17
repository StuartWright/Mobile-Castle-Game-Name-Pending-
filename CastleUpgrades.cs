using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CastleUpgrades : MonoBehaviour
{
    public int BaseUpgradeCost;
    public int KillMoneyCost;
    public int CastleArcherCost;
    public int CastleArcherSpeedCost;
    public PlayerStatManager PSM;
    public Archer CastleArcher;
    public Text BaseHealthText;
    public Text SoldierKillMoneyText;
    public Text CastleArcherMoneyText;
    public Text CastleArcherSpeedText;
	void Start ()
    {
        GetSaves.Instance.SaveAll += Save;
        if(PlayerPrefs.HasKey("BaseUpgradeCost"))
        Load();
        else
        {
            BaseHealthText.text = "Gems: " + BaseUpgradeCost;
            SoldierKillMoneyText.text = "Gems: " + KillMoneyCost;
            CastleArcherMoneyText.text = "Gems: " + CastleArcherCost;
            CastleArcherSpeedText.text = "Gems: " + CastleArcherSpeedCost;
        }
    }

    public void UpgradeBaseHealth()
    {
        if (PSM.Gems >= BaseUpgradeCost)
        {
            PSM.BaseHP++;
            PSM.Gems -= BaseUpgradeCost;
            BaseUpgradeCost += 1;
            BaseHealthText.text = "Gems: " + BaseUpgradeCost;
            UIManager.Instance.UpdateUI();
        }
    }

    public void UpgradeKillMoney()
    {
        if (PSM.Gems >= KillMoneyCost)
        {
            PSM.SoldierKillAmount += 2;
            PSM.ArcherKillAmount += 2;
            PSM.MageKillAmount += 2;
            PSM.NinjaKillAmount += 2;
            PSM.Gems -= KillMoneyCost;
            KillMoneyCost += 1;
            SoldierKillMoneyText.text = "Gems: " + KillMoneyCost;
            UIManager.Instance.UpdateUI();
        }
    }

    public void UpgradeCastleArcher()
    {
        if(PSM.Gems >= CastleArcherCost)
        {
            CastleArcher.Damage++;
            PSM.Gems -= CastleArcherCost;
            CastleArcherCost += 5;
            CastleArcherMoneyText.text = "Gems: " + CastleArcherCost;
            UIManager.Instance.UpdateUI();
        }
    }

    public void UpgradeCastleArcherSpeed()
    {
        if (PSM.Gems >= CastleArcherSpeedCost)
        {
            CastleArcher.anim.speed += .1f;
            PSM.Gems -= CastleArcherSpeedCost;
            CastleArcherSpeedCost += 5;
            CastleArcherSpeedText.text = "Gems: " + CastleArcherSpeedCost;
            UIManager.Instance.UpdateUI();
        }
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    private void Save()
    {
        PlayerPrefs.SetInt("BaseUpgradeCost", BaseUpgradeCost);
        PlayerPrefs.SetInt("KillMoneyCost", KillMoneyCost);
        PlayerPrefs.SetInt("CAUpgradeCost", CastleArcherCost);
        PlayerPrefs.SetInt("CAUpgradeSpeedCost", CastleArcherSpeedCost);
    }
    private void Load()
    {
        BaseUpgradeCost = PlayerPrefs.GetInt("BaseUpgradeCost");
        KillMoneyCost = PlayerPrefs.GetInt("KillMoneyCost");
        CastleArcherCost = PlayerPrefs.GetInt("CAUpgradeCost");
        CastleArcherSpeedCost = PlayerPrefs.GetInt("CAUpgradeSpeedCost");
        BaseHealthText.text = "Gems: " + BaseUpgradeCost;
        CastleArcherMoneyText.text = "Gems: " + CastleArcherCost;
        CastleArcherSpeedText.text = "Gems: " + CastleArcherSpeedCost;
        SoldierKillMoneyText.text = "Gems: " + KillMoneyCost;
    }
}
