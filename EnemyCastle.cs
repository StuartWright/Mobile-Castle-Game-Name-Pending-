using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCastle : MonoBehaviour
{
    public static EnemyCastle Instance;
    public delegate void Rewards(EnemyCastle sender, int Gold, int SoldierReward);
    public event Rewards PlayerReward;
    public event Rewards UnSubWin;
    public float EnemyThreatLevel;

    public int GoldReward;
    public int SoldierReward;
    public int EnemySoldiers;
    public int EnemyArchers;
    public int EnemyMages;
    public int EnemyNinjas;
    public int CastleLevel;
    private int CurrentLevel;
    public int SoldierDamage, SoldierHealth, ArcherDamage, ArcherHealth, MageDamage, MageHealth, NinjaDamage, NinjaHealth, Speed, BaseHP;
    public float SpawnRate, MageSpawnRate, NinjaSpawnRate;
    public Text EnemyThreatLevelText;
    public Text SoldierAmountText;
    public Text ArcherAmountText;
    public Text MagesAmountText;
    public GameObject BattleButton;
    public BattleManager BM;
    public Sprite DestroyedCastle;
    public GameObject CastleIMG;
	void Start ()
    {
        EnemyThreatLevelText.text = "Level: " + CastleLevel;
        //SoldierAmountText.text = "Soldiers: " + EnemySoldiers;
        // ArcherAmountText.text = "Archers: " + EnemyArchers;
        // MagesAmountText.text = "Mages: " + EnemyMages;
        if(PlayerPrefs.HasKey("CurrentLevel"))
        Load();
       if(CastleLevel <= CurrentLevel)
        {
            BattleButton.SetActive(false);
            CastleIMG.GetComponent<SpriteRenderer>().sprite = DestroyedCastle;
        }
            
    }
    public void RemoveButton()
    {
        BattleButton.SetActive(false);
        CastleIMG.GetComponent<SpriteRenderer>().sprite = DestroyedCastle;
        PlayerReward += PlayerStatManager.Instance.WonBattle;
        BM.WonBattle -= RemoveButton;
        Save();
        PlayerReward(this, GoldReward, SoldierReward);
    }
    public void UnSubwin()
    {
        BM.WonBattle -= RemoveButton;
        BM.LostBattle -= UnSubwin;
        EnemyCastle.Instance = null;
    }
    /*
    public void QuickAttack()
    {
        if(PlayerStatManager.Instance.PlayerThreatLevel > EnemyThreatLevel)
        {
            BattleWon(this, GoldReward);
        }
        else
        {
            float Max = (int)EnemyThreatLevel;
            float Min = (int)PlayerStatManager.Instance.PlayerThreatLevel;
            float RandomNum = (int)Random.Range(0, Max);
            if (RandomNum <= Min)
            {
                BattleWon(this, GoldReward);
            }
            else
            {
                BattleLost(this, 0);
            }
        }
        //float test = (PlayerStatManager.Instance.PlayerThreatLevel * EnemyThreatLevel) / 100;
    }
    */
    public void Attack()
    {
        WaveManager.Instance.WaveText.gameObject.SetActive(false);
        BM.LostBattle += UnSubwin;
        BM.WonBattle += RemoveButton;
        Instance = this;
        BattleManager.Instance.PrepareBattle(EnemySoldiers, EnemyArchers, EnemyMages, EnemyNinjas, SpawnRate, MageSpawnRate, NinjaSpawnRate, BaseHP);
    }
    private void Save()
    {
        PlayerPrefs.SetInt("CurrentLevel", CastleLevel);
    }
    private void Load()
    {
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }
}

