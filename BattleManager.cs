using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public delegate void Battle();
    public event Battle StartBattle;
    public event Battle ReturnToCastle;
    public event Battle DestroyAllUnits;
    public event Battle WonBattle;
    public event Battle LostBattle;
    public event Battle AllFriendlySpawned;
    public event Battle AllEnemiesSpawned;

    public ObjectPooler Pool;

    public int Enemys;
    public int EnemyArchers;
    public int EnemyMages;
    public int EnemyNinjas;
    public int Friendlys;
    public int FriendlyArchers;
    public int FriendlyMages;
    public int FriendlyNinjas;
    public int CastleReward;
    public int eBaseHP;
    private int sDamage, sHealth, eSpeed, aDamage, aHealth, mDamage, mHealth;
    private float eSpawnRate, eMageSpawnRate, eNinjaSpawnRate;
    private bool BattleEnded = false;

    public float FSpawnRate;
    public float FMageSpawnRate;
    public float FNinjaSpawnRate;

    public GameObject[] EnemyStartPoints = new GameObject[3];
    public GameObject[] FriendlyStartPoints = new GameObject[3];
    public GameObject[] FriendlyArcherPoints = new GameObject[5];
    public GameObject[] EnemyArcherPoints = new GameObject[5];
    //public List<GameObject> AllUnits = new List<GameObject>();
    public GameObject EnemyGO;
    public GameObject EnemyArcherGO;
    public GameObject EnemyMageGO;
    public GameObject FriendlyGO;
    public GameObject FriendlyMageGO;
    public GameObject FriendlyArcherGO;

    public Archer WallArcherRef;
    public EnemyUnitManager EnemyStatsRef;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    void Start ()
    {
        GetSaves.Instance.SaveAll += Save;
        if (PlayerPrefs.HasKey("FSpawnRate"))
            Load();
        StartBattle += SpawnEnemies;
        StartBattle += SpawnFriendlys;
        //EndBattle += StopSpawning;
        // LostBattle += PlayerLostBattle;
        //Pool = ObjectPooler.Instance;
    }

    public void PrepareBattle(int EnemySoldiers, int Enemyarchers, int Enemymages, int Enemyninjas, float ESpawnRate, float EMageSpawnRate, float ENinjaSpawnRate, int EBaseHP)
    {
        eSpawnRate = ESpawnRate;
        eMageSpawnRate = EMageSpawnRate;
        eNinjaSpawnRate = ENinjaSpawnRate;
        eBaseHP = EBaseHP;
        Enemys = EnemySoldiers;
        EnemyArchers = Enemyarchers;
        EnemyMages = Enemymages;
        EnemyNinjas = Enemyninjas;
        Friendlys = PlayerStatManager.Instance.Soldiers;
        FriendlyArchers = PlayerStatManager.Instance.Archers;
        FriendlyMages = PlayerStatManager.Instance.Mages;
        FriendlyNinjas = PlayerStatManager.Instance.Ninjas;
        StartBattle();
    }

    public void SpawnEnemies()
    {
        BattleEnded = false;
        for (int i = 0; i < EnemyArchers; i++)
        {
            GameObject GO = Pool.SpawnFromPool("EArcher", EnemyArcherPoints[i].transform.position);
            //Archer obj = GO.GetComponent<Archer>();
        }
        StartCoroutine(EnemySpawner());
        StartCoroutine(EnemyMageSpawner());
        StartCoroutine(EnemyNinjaSpawner());
    }
    IEnumerator EnemySpawner()
    {
        if (Enemys > 0 && !BattleEnded)
        {
            float YPos = Random.Range(40, 180);
            GameObject GO = Pool.SpawnFromPool("ESoldier", transform.position = new Vector3(1800, YPos, 1));
            //BaseKnightMovement obj = GO.GetComponent<BaseKnightMovement>();
            Enemys--;
            yield return new WaitForSeconds(eSpawnRate);
            StartCoroutine(EnemySpawner());
        }
        else
        {
            AllEnemiesSpawned?.Invoke();
        }
            
    }
    IEnumerator EnemyMageSpawner()
    {
        yield return new WaitForSeconds(eMageSpawnRate);
        if (EnemyMages > 0 && !BattleEnded)
        {
            float YPos = Random.Range(40, 180);
            GameObject GO = Pool.SpawnFromPool("EMage", transform.position = new Vector3(1800, YPos, 1));
           // Mage obj = GO.GetComponent<Mage>();
            EnemyMages--;
            StartCoroutine(EnemyMageSpawner());
        }
    }
    IEnumerator EnemyNinjaSpawner()
    {
        yield return new WaitForSeconds(eNinjaSpawnRate);
        if (EnemyNinjas > 0 && !BattleEnded)
        {            
            float YPos = Random.Range(40, 180);
            GameObject GO = Pool.SpawnFromPool("ENinja", transform.position = new Vector3(1800, YPos, 1));
            EnemyNinjas--;
            StartCoroutine(EnemyNinjaSpawner());
        }
    }
    public void SpawnFriendlys()
    {
        BattleEnded = false;
        WallArcherRef.gameObject.SetActive(true);
        for (int i = 0; i < FriendlyArchers; i++)
        {
            Pool.SpawnFromPool("FArcher", FriendlyArcherPoints[i].transform.position);
        }
        StartCoroutine(FriendlySpawner());
        StartCoroutine(FriendlyMageSpawner());
        StartCoroutine(FriendlyNinjaSpawner());
    }
    IEnumerator FriendlySpawner()
    {
        if (Friendlys > 0 && !BattleEnded)
        {
            float YPos = Random.Range(40, 180);
            //Instantiate(FriendlyGO, transform.position = new Vector3(1010, YPos, 1), transform.rotation);
            GameObject GO = Pool.SpawnFromPool("FSoldier", transform.position = new Vector3(1010, YPos, 1));
            //BaseKnightMovement obj = GO.GetComponent<BaseKnightMovement>();
            Friendlys--;
            yield return new WaitForSeconds(FSpawnRate);
            StartCoroutine(FriendlySpawner());
        }
        else
            AllFriendlySpawned?.Invoke();
    }
    IEnumerator FriendlyMageSpawner()
    {
        yield return new WaitForSeconds(FMageSpawnRate);
        if (FriendlyMages > 0 && !BattleEnded)
        {
            
            float YPos = Random.Range(40, 180);
            Pool.SpawnFromPool("FMage", transform.position = new Vector3(1010, YPos, 1));
            FriendlyMages--;
            
            StartCoroutine(FriendlyMageSpawner());
        }
    }
    IEnumerator FriendlyNinjaSpawner()
    {
        yield return new WaitForSeconds(FNinjaSpawnRate);
        if (FriendlyNinjas > 0 && !BattleEnded)
        {
            
            float YPos = Random.Range(40, 180);
            Pool.SpawnFromPool("FNinja", transform.position = new Vector3(1010, YPos, 1));
            FriendlyNinjas--;

            StartCoroutine(FriendlyNinjaSpawner());
        }
    }
    public void PlayerWon()
    {
        BattleEnded = true;
        DestroyAllUnits();
        WonBattle();
        ReturnToCastle();
    }

    public void PlayerLost()
    {
        BattleEnded = true;
        LostBattle += PlayerStatManager.Instance.LostBattle;
        LostBattle();
        DestroyAllUnits();
        ReturnToCastle();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("FSpawnRate", FSpawnRate);
        PlayerPrefs.SetFloat("FMageSpawnRate", FMageSpawnRate);
        PlayerPrefs.SetFloat("FNinjaSpawnRate", FNinjaSpawnRate);
    }
    public void Load()
    {
        FSpawnRate = PlayerPrefs.GetFloat("FSpawnRate");
        FMageSpawnRate = PlayerPrefs.GetFloat("FMageSpawnRate");
        FNinjaSpawnRate = PlayerPrefs.GetFloat("FNinjaSpawnRate");
    }
}
