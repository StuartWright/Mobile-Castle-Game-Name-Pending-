using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveManager : MonoBehaviour
{
    public delegate void WonWave(WaveManager sender, int MoneyReward);
    public event WonWave WaveWon;

    public static WaveManager Instance;
    public BattleManager BM;
    public Text WaveText;
    public int SDamage, SHealth, ADamage, AHealth, MDamage, MHealth, NDamage, NHealth;//, WSpeed;
    public float WSpawnRate, WMageSpawnRate, WNinjaSpawnRate;
    public int WSoldierAmount, WArcherAmount, WMageAmount, WNinjaAmount, WBaseHP;
    public int Wave;
    public int GoldReward = 20;
    public bool WaveActive;
    void Start ()
    {
        Instance = this;
        BM = BattleManager.Instance;
        GetSaves.Instance.SaveAll += Save;
        if (PlayerPrefs.HasKey("Wave"))
            Load();
        
        WaveText.text = "Wave: " + Wave;
    }
    public void StartWave()
    {
        if(!WaveActive)
        {
            WaveText.gameObject.SetActive(true);
            BM.LostBattle += UnSubwin;
            BM.WonBattle += Win;
            WaveActive = true;
            BM.PrepareBattle(WSoldierAmount, WArcherAmount, WMageAmount, WNinjaAmount, WSpawnRate, WMageSpawnRate, WNinjaSpawnRate, WBaseHP);
        }           
    }

    public void Win()
    {
        WaveWon += PlayerStatManager.Instance.WonWave;
        BM.WonBattle -= Win;
        WaveWon(this, GoldReward);
        WaveProgression();
    }

    public void UnSubwin()
    {
        BM.WonBattle -= Win;
        BM.LostBattle -= UnSubwin;
    }

    public void WaveProgression()
    {
        Wave++;
        if ((Wave % 7) == 0)
            WNinjaAmount++;
        WaveText.text = "Wave: " + Wave;
        WBaseHP += 1;
        GoldReward += 5;
        if (WSoldierAmount > 15)
        {
            WSoldierAmount = 1;
            WMageAmount++;
            SDamage++;
            SHealth++;
            ADamage++;
            AHealth++;
            MDamage++;
            MHealth++;
            NDamage++;
            NHealth++;
        }
        else
        {
            WSoldierAmount++;
        }

        WSpawnRate -= .03f;
        WMageSpawnRate -= .1f;
        if (Wave == 10 || Wave == 20 || Wave == 30 || Wave == 40 || Wave == 50)
            WArcherAmount++;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("SDamage", SDamage);
        PlayerPrefs.SetInt("SHealth", SHealth);
        PlayerPrefs.SetInt("ADamage", ADamage);
        PlayerPrefs.SetInt("AHealth", AHealth);
        PlayerPrefs.SetInt("MDamage", MDamage);
        PlayerPrefs.SetInt("MHealth", MHealth);
        PlayerPrefs.SetInt("NDamage", NDamage);
        PlayerPrefs.SetInt("NHealth", NHealth);
        //PlayerPrefs.SetInt("WSpeed", WSpeed);
        PlayerPrefs.SetFloat("WSpawnRate", WSpawnRate);
        PlayerPrefs.SetFloat("WMageSpawnRate", WMageSpawnRate);
        PlayerPrefs.SetInt("WSoldierAmount", WSoldierAmount);
        PlayerPrefs.SetInt("WArcherAmount", WArcherAmount);
        PlayerPrefs.SetInt("WMageAmount", WMageAmount);
        PlayerPrefs.SetInt("WBaseHP", WBaseHP);
        PlayerPrefs.SetInt("Wave", Wave);
        PlayerPrefs.SetInt("GoldReward", GoldReward);
    }
    private void Load()
    {
        SDamage = PlayerPrefs.GetInt("SDamage");
        SHealth = PlayerPrefs.GetInt("SHealth");
        ADamage = PlayerPrefs.GetInt("ADamage");
        AHealth = PlayerPrefs.GetInt("AHealth");
        MDamage = PlayerPrefs.GetInt("MDamage");
        MHealth = PlayerPrefs.GetInt("MHealth");
        NDamage = PlayerPrefs.GetInt("NDamage");
        NHealth = PlayerPrefs.GetInt("NHealth");
        //WSpeed = PlayerPrefs.GetInt("WSpeed");
        WSpawnRate = PlayerPrefs.GetFloat("WSpawnRate");
        WMageSpawnRate = PlayerPrefs.GetFloat("WMageSpawnRate");
        WSoldierAmount = PlayerPrefs.GetInt("WSoldierAmount");
        WArcherAmount = PlayerPrefs.GetInt("WArcherAmount");
        WMageAmount = PlayerPrefs.GetInt("WMageAmount");
        WBaseHP = PlayerPrefs.GetInt("WBaseHP");
        Wave = PlayerPrefs.GetInt("Wave");
        GoldReward = PlayerPrefs.GetInt("GoldReward");
        WaveText.text = "Wave: " + Wave;
    }
}
