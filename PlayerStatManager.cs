using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager Instance;
    
    public int Money;
    public int Gems;
    //public int SoldierLimit;
    public int SoldierCost = 10;
    public int ArcherCost = 20;
    public int MageCost = 60;
    public int NinjaCost = 150;
    public int BarracksSpace = 0;
    public int MaxBarracksSpace = 5;
    private int ArcherAmount = 0;

    public int SoldierKillAmount;
    public int ArcherKillAmount;
    public int MageKillAmount;
    public int NinjaKillAmount;

    public int Archers;
    public float PlayerThreatLevel;
    public int Soldiers;
    public int Mages;
    public int Ninjas;
    public int BaseHP;

    public int SMaxHealth;
    public int SMaxDamage;
    public int AMaxHealth;
    public int AMaxDamage;
    public int MMaxHealth;
    public int MMaxDamage;
    public int NMaxHealth;
    public int NMaxDamage;

    public int Speed;
    public int AOERange;

    public GameObject WinWavePanel;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject ArcherButton;
    public GameObject CreditPanel;

    public BaseKnightMovement SoldierRef;
    public Archer ArcherRef;
    public Mage MageRef;

    void Start ()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
     
        GetSaves.Instance.SaveAll += Save;
        if (PlayerPrefs.HasKey("Money"))
            LoadPlayerStats();
        //SetSoldiersStats();
    }
	
    public void SetSoldiersStats()
    {
        SoldierRef.Damage = 1;
        SoldierRef.Speed = 100;
        BattleManager.Instance.FSpawnRate = 2;

        ArcherRef.Health = 2;
        ArcherRef.Damage = 1;

        MageRef.Health = 10;
        MageRef.Damage = 2;
        MageRef.Speed = 100;
        MageRef.AoeRange = 50;
        BattleManager.Instance.FMageSpawnRate = 5;
    }
    public void WonWave(WaveManager sender, int GoldReward)
    {
        WinWavePanel.SetActive(true);
        Gems++;
        Money += GoldReward;
        WinWavePanel.GetComponent<WinPanel>().SetUp(GoldReward, 0);
        if (BarracksSpace < 0)
            BarracksSpace = 0;//quick fix.
        UIManager.Instance.UpdateUI();
        sender.WaveWon -= WonWave;
        GetSaves.Instance.SaveGame();
    }
    public void WonBattle(EnemyCastle Sender, int GoldReward, int SoldierReward)
    {
        Money += GoldReward;
        Soldiers += SoldierReward;
        BarracksSpace += SoldierReward;
        WinPanel.SetActive(true);
        WinPanel.GetComponent<WinPanel>().SetUp(GoldReward, SoldierReward);
        MaxBarracksSpace++;
        Gems++;
        if (BarracksSpace < 0)
            BarracksSpace = 0;//quick fix.
        UIManager.Instance.UpdateUI();
        Sender.PlayerReward -= WonBattle;
        GetSaves.Instance.SaveGame();
    }
    public void LostBattle()
    {
        LosePanel.SetActive(true);
        if (BarracksSpace < 0)
            BarracksSpace = 0;//quick fix.
        UIManager.Instance.UpdateUI();
        BattleManager.Instance.LostBattle -= LostBattle;
        GetSaves.Instance.SaveGame();
    }

    public float CalculatePlayerThreat()
    {
        float ThreatLevel = (Soldiers / 10) + (Archers / 5);
        return ThreatLevel;
    }

    public void BuySoldier()
    {
        if(Money >= SoldierCost && BarracksSpace < MaxBarracksSpace)
        {
            Soldiers++;
            Money -= SoldierCost;
            BarracksSpace++;
            UIManager.Instance.UpdateUI();
        }
    }
    public void BuyArcher()
    {
        if(Money >= ArcherCost && BarracksSpace < MaxBarracksSpace)
        {
            Archers++;
            Money -= ArcherCost;
            ArcherCost = ArcherCost += 5000;
            BarracksSpace++;
            ArcherAmount++;
            UIManager.Instance.UpdateUI();
            if (ArcherAmount >= 5)
                Destroy(ArcherButton);
        } 
    }

    public void BuyMage()
    {
        if (Money >= MageCost && (BarracksSpace < MaxBarracksSpace) && WaveManager.Instance.Wave >= 20)
        {
            Mages++;
            Money -= MageCost;
            BarracksSpace++;
            UIManager.Instance.UpdateUI();
        }
    }

    public void BuyNinja()
    {
        if (Money >= NinjaCost && (BarracksSpace < MaxBarracksSpace) && WaveManager.Instance.Wave >= 10)
        {
            Ninjas++;
            Money -= NinjaCost;
            BarracksSpace++;
            UIManager.Instance.UpdateUI();
        }
    }
    public void OpenCredits()
    {
        CreditPanel.SetActive(true);
    }
    public void CloseCredits()
    {
        CreditPanel.SetActive(false);
    }
    public void LoadPlayerStats()
    {
        Money = PlayerPrefs.GetInt("Money");
        Gems = PlayerPrefs.GetInt("Gems");
        SoldierCost = PlayerPrefs.GetInt("SoldierCost");
        ArcherCost = PlayerPrefs.GetInt("ArcherCost");
        MageCost = PlayerPrefs.GetInt("MageCost");
        NinjaCost = PlayerPrefs.GetInt("NinjaCost");
        BarracksSpace = PlayerPrefs.GetInt("BarracksSpace");
        MaxBarracksSpace = PlayerPrefs.GetInt("MaxBarracksSpace");
        ArcherAmount = PlayerPrefs.GetInt("ArcherAmount");
        SoldierKillAmount = PlayerPrefs.GetInt("SoldierKillAmount");
        ArcherKillAmount = PlayerPrefs.GetInt("ArcherKillAmount");
        MageKillAmount = PlayerPrefs.GetInt("MageKillAmount");
        NinjaKillAmount = PlayerPrefs.GetInt("NinjaKillAmount");
        Soldiers = PlayerPrefs.GetInt("Soldiers");
        Archers = PlayerPrefs.GetInt("Archers");
        Mages = PlayerPrefs.GetInt("Mages");
        Ninjas = PlayerPrefs.GetInt("Ninjas");
        BaseHP = PlayerPrefs.GetInt("BaseHP");
        SMaxHealth = PlayerPrefs.GetInt("SMaxHealth");
        SMaxDamage = PlayerPrefs.GetInt("SMaxDamage");
        AMaxHealth = PlayerPrefs.GetInt("AMaxHealth");
        AMaxDamage = PlayerPrefs.GetInt("AMaxDamage");
        MMaxHealth = PlayerPrefs.GetInt("MMaxHealth");
        MMaxDamage = PlayerPrefs.GetInt("MMaxDamage");
        NMaxHealth = PlayerPrefs.GetInt("NMaxHealth");
        NMaxDamage = PlayerPrefs.GetInt("NMaxDamage");
        Speed = PlayerPrefs.GetInt("Speed");

        //PlayerData data = SaveSystem.LoadPlayer();
        //Money = data.Money;       
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetInt("Gems", Gems);
        PlayerPrefs.SetInt("SoldierCost", SoldierCost);
        PlayerPrefs.SetInt("ArcherCost", ArcherCost);
        PlayerPrefs.SetInt("MageCost", MageCost);
        PlayerPrefs.SetInt("NinjaCost", NinjaCost);
        PlayerPrefs.SetInt("BarracksSpace", BarracksSpace);
        PlayerPrefs.SetInt("MaxBarracksSpace", MaxBarracksSpace);
        PlayerPrefs.SetInt("ArcherAmount", ArcherAmount);
        PlayerPrefs.SetInt("SoldierKillAmount", SoldierKillAmount);
        PlayerPrefs.SetInt("ArcherKillAmount", ArcherKillAmount);
        PlayerPrefs.SetInt("MageKillAmount", MageKillAmount);
        PlayerPrefs.SetInt("NinjaKillAmount", NinjaKillAmount);
        PlayerPrefs.SetInt("Soldiers", Soldiers);
        PlayerPrefs.SetInt("Archers", Archers);
        PlayerPrefs.SetInt("Mages", Mages);
        PlayerPrefs.SetInt("Ninjas", Ninjas);
        PlayerPrefs.SetInt("BaseHP", BaseHP);
        PlayerPrefs.SetInt("SMaxHealth", SMaxHealth);
        PlayerPrefs.SetInt("SMaxDamage", SMaxDamage);
        PlayerPrefs.SetInt("AMaxHealth", AMaxHealth);
        PlayerPrefs.SetInt("AMaxDamage", AMaxDamage);
        PlayerPrefs.SetInt("MMaxHealth", MMaxHealth);
        PlayerPrefs.SetInt("MMaxDamage", MMaxDamage);
        PlayerPrefs.SetInt("NMaxHealth", NMaxHealth);
        PlayerPrefs.SetInt("NMaxDamage", NMaxDamage);
        PlayerPrefs.SetInt("Speed", Speed);
    }
}
