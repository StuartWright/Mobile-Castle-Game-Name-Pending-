using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : BaseKnightMovement
{
    public GameObject FirePoint;
    public GameObject Arrow;
    public BaseKnightMovement ArcherOpponent;
    public GameObject OpponentsBase;
    public float FireTimer = 2;
    bool NoMoreEnemies = false;
    public List<GameObject> Enemies = new List<GameObject>();
    public Animator anim;
    private new void Start()
    {
        anim = GetComponent<Animator>();
        if (EType == EnemyType.WallArcher)
        {
                
                GetSaves.Instance.SaveAll += Save;
            if (PlayerPrefs.HasKey("CWADamage"))
            {
                Load();
            }
        }
        if (!IsEnemy)
        {
            OpponentsBase = GameObject.Find("EnemyBase");
            BattleManager.Instance.AllEnemiesSpawned += AllEnemiesDead;
        }        
        else if (IsEnemy)
        {
            OpponentsBase = GameObject.Find("FriendlyBase");
            BattleManager.Instance.AllFriendlySpawned += AllEnemiesDead;
        }           
        base.Start();
    }
    void FixedUpdate ()
    {
        anim.SetBool("Shooting", true);
        if(FK != null)
        {
            if (FK.IsDead)
                FK = null;
        }
        if (FK == null)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                FindTarget();
                Timer = .1f;
            }
        }
            
        /*
        if (FK != null)
        {
            if (FK.IsDead)
                FK = null;
            if (CanShoot)
            {
                Anim.SetBool("Shooting", true);
                Anim.SetBool("Idle", false);
                CanShoot = false;
            }
        }
        else if(FK == null)
        {
            CanShoot = true;
            FireTimer = 2;
            Anim.SetBool("Idle", true);
            Anim.SetBool("Shooting", false);
            FindTarget();
        }
        */
     }
    
    public void FindTarget()
    {
        Hit = Physics2D.OverlapCircleAll(transform.position, 1000 << LayerMask.NameToLayer("Default"));
        foreach (Collider2D EnemieCols in Hit)
        {
            if (!IsEnemy && EnemieCols.tag == "Enemy" && !EnemieCols.GetComponent<BaseKnightMovement>().IsDead && !EnemieCols.GetComponent<Archer>())
            {
                Enemies.Add(EnemieCols.gameObject);
                //FK = Hit[i].GetComponent<BaseKnightMovement>();
                //break;

            }
            else if (IsEnemy && EnemieCols.tag == "Friendly" && !EnemieCols.GetComponent<BaseKnightMovement>().IsDead && !EnemieCols.GetComponent<Archer>())
            {
                Enemies.Add(EnemieCols.gameObject);
                // FK = Hit[i].GetComponent<BaseKnightMovement>();
                //break;
            }
        }
        
        
        if (Enemies.Count > 0)
        {
            int RandomNum = Random.Range(0, Enemies.Count);
            FK = Enemies[RandomNum].GetComponent<BaseKnightMovement>();
            Enemies.Clear();
        }
            

    }

    public new void FindEnemies()
    {
        Hit = Physics2D.OverlapCircleAll(transform.position, 1000 << LayerMask.NameToLayer("Default"));
        for (int i = 0; i < Hit.Length; i++)
        {
            if (!IsEnemy && Hit[i].tag == "Enemy")
            {
                FK = Hit[i].GetComponent<BaseKnightMovement>();
               //FK.OnDeath += TargetDied;
                //OnDeath += FK.TargetDied;
                break;

            }
            else if (IsEnemy && Hit[i].tag == "Friendly")
            {
                FK = Hit[i].GetComponent<BaseKnightMovement>();
                //FK.OnDeath += TargetDied;
                //OnDeath += FK.TargetDied;
                break;
            }
        }
    }

    public void Fire()
    {      
        GameObject arrow = Instantiate(Arrow, FirePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
        if (FK != null)
        {
            arrow.GetComponent<Arrow>().Target = FK.gameObject;
            FK = null;
            ArcherOpponent = null;
            FindTarget();
        }
        else if(NoMoreEnemies)
        {
            arrow.GetComponent<Arrow>().Target = OpponentsBase;
            
        }
        arrow.GetComponent<Arrow>().Damage = Damage;
        arrow.GetComponent<Arrow>().Sender = this;
    }

    public void AllEnemiesDead()
    {
        NoMoreEnemies = true;
        BattleManager.Instance.AllEnemiesSpawned -= AllEnemiesDead;
    }


    public void Save()
    {
        PlayerPrefs.SetInt("CWADamage", Damage);
        PlayerPrefs.SetFloat("CWASpeed", anim.speed);
    }
    public void Load()
    {
        Damage = PlayerPrefs.GetInt("CWADamage");
        anim.speed = PlayerPrefs.GetFloat("CWASpeed");
    }
}
