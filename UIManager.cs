using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text SoldierAmountText, ArcherAmountText, MageAmountText, NinjaAmountText;
    public Text PlayerThreatLevel;
    public Text PlayerGold;
    public Text PlayerGems;
    public Text SoldierCostText;
    public Text ArcherCostText;
    public Text MageCostText;
    public Text NinjaCostText;
    public Text BarracksSpaceText;
    public Text MaxBarrackSpaceText;

    public GameObject SoldierUpgradePanel;
    public GameObject MageUpgradePanel;
    public GameObject ArcherUpgradePanel;
    public GameObject NinjaUpgradePanel;
    public GameObject CastleUpgradesPanel;
	void Start ()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        UpdateUI();
    }

    public void UpdateUI()
    {
        SoldierAmountText.text = "Soldiers: " + PlayerStatManager.Instance.Soldiers;
        ArcherAmountText.text = "Archers: " + PlayerStatManager.Instance.Archers;
        //PlayerThreatLevel.text = "Threat Level = " + PlayerStatManager.Instance.PlayerThreatLevel;
        PlayerGold.text = "Gold: " + PlayerStatManager.Instance.Money;
        PlayerGems.text = "Gems: " + PlayerStatManager.Instance.Gems;
        MageAmountText.text = "Mages: " + PlayerStatManager.Instance.Mages;
        NinjaAmountText.text = "Ninjas: " + PlayerStatManager.Instance.Ninjas;
        SoldierCostText.text = "Buy Soldier: " + PlayerStatManager.Instance.SoldierCost;
        ArcherCostText.text = "Buy Archer: " + PlayerStatManager.Instance.ArcherCost;
        if (WaveManager.Instance.Wave >= 20)
            MageCostText.text = "Buy Mage: " + PlayerStatManager.Instance.MageCost;
        else
            MageCostText.text = "Requires Wave 20";
        if (WaveManager.Instance.Wave >= 10)
            NinjaCostText.text = "Buy Ninja: " + PlayerStatManager.Instance.NinjaCost;
        else
            NinjaCostText.text = "Requires Wave 10";
        BarracksSpaceText.text = "Barracks Space: " + PlayerStatManager.Instance.BarracksSpace + "/" + PlayerStatManager.Instance.MaxBarracksSpace;
        //MaxBarrackSpaceText.text = "Max Barracks Space: " + PlayerStatManager.Instance.MaxBarracksSpace;
    }
    public void UpdateGold()
    {
        PlayerGold.text = "Gold: " + PlayerStatManager.Instance.Money;
        PlayerGems.text = "Gems: " + PlayerStatManager.Instance.Gems;
    }
	
    public void OpenSoldierPanel()
    {
        SoldierUpgradePanel.SetActive(true);
    }
    public void OpenMagePanel()
    {
        MageUpgradePanel.SetActive(true);
    }
    public void OpenArcherPanel()
    {
        ArcherUpgradePanel.SetActive(true);
    }
    public void OpenCastlePanel()
    {
        CastleUpgradesPanel.SetActive(true);
    }
    public void OpenNinjaPanel()
    {
        NinjaUpgradePanel.SetActive(true);
    }
}
