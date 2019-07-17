using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    Soldier,
    Archer,
    Mage,
    Ninja,
    WallArcher
}


public class BaseKnightMovement : MonoBehaviour, IDamagable, IKillable, IPopUp
{
    public delegate void Death();
    public event Death OnDeath;

    public EnemyType EType;
    public Rigidbody2D rb;
    public BaseKnightMovement FK;
    public EnemyUnitManager UnitStats;
    public BattleManager BM;
    private PlayerStatManager PSM;
    private WaveManager WM;
    private EnemyCastle EC;
    protected BattleBases BB;
    protected Collider2D[] Hit;
    protected Animator Anim;
    public Text PopUpText;
    public float Speed;
    protected float EffectTimer = 1f;
    protected bool EffectActive = false;
    public bool CanMove = true;
    public bool IsEnemy;
    public bool Attackable = true;
    public bool IsDead;
    public bool AtEnemyBase = false;
    public GameObject EnemyPoint;
    public GameObject AOEEffect;
    public int Damage, Health;
    protected float Timer = 1;
    private float Distance;
    public bool SetStats
    {
        set
        {
            if(!IsEnemy)
            {
                if (EType == EnemyType.Soldier)
                {
                    Damage = PSM.SMaxDamage;
                    Health = PSM.SMaxHealth;
                }
                else if (EType == EnemyType.Archer)
                {
                    Damage = PSM.AMaxDamage;
                    Health = PSM.AMaxHealth;
                }
                else if (EType == EnemyType.Mage)
                {
                    Damage = PSM.MMaxDamage;
                    Health = PSM.MMaxHealth;
                    GetComponent<Mage>().AoeRange = PSM.AOERange;
                }
                else if (EType == EnemyType.Ninja)
                {
                    Damage = PSM.NMaxDamage;
                    Health = PSM.NMaxHealth;
                }
            }
            else if(IsEnemy)
            {
                if(EnemyCastle.Instance == null)
                {
                    if (EType == EnemyType.Soldier)
                    {
                        Damage = WM.SDamage;
                        Health = WM.SHealth;
                    }
                    else if (EType == EnemyType.Archer)
                    {
                        Damage = WM.ADamage;
                        Health = WM.AHealth;
                    }
                    else if (EType == EnemyType.Mage)
                    {
                        Damage = WM.MDamage;
                        Health = WM.MHealth;
                        GetComponent<Mage>().AoeRange = PSM.AOERange;
                    }
                    else if (EType == EnemyType.Ninja)
                    {
                        Damage = WM.NDamage;
                        Health = WM.NHealth;
                    }
                }
                else
                {
                    EC = EnemyCastle.Instance;
                    if (EType == EnemyType.Soldier)
                    {
                        Damage = EC.SoldierDamage;
                        Health = EC.SoldierHealth;
                    }
                    else if (EType == EnemyType.Archer)
                    {
                        Damage = EC.ArcherDamage;
                        Health = EC.ArcherHealth;
                    }
                    else if (EType == EnemyType.Mage)
                    {
                        Damage = EC.MageDamage;
                        Health = EC.MageHealth;
                        GetComponent<Mage>().AoeRange = PSM.AOERange;
                    }
                    else if (EType == EnemyType.Ninja)
                    {
                        Damage = EC.NinjaDamage;
                        Health = EC.NinjaHealth;
                    }
                }
                
            }      
        }
    }
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        BM = BattleManager.Instance;
        BM.DestroyAllUnits += KillThis;     
        
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if(PSM == null)
            PSM = PlayerStatManager.Instance;
        if (WM == null)
            WM = WaveManager.Instance;
        if (FK != null)
            TargetDied();
        CanMove = true;
        IsDead = false;
        SetStats = true;
        FK = null;
    }
    void FixedUpdate()
    {
        if (CanMove)
        {
            Anim.SetBool("Running", true);
            Anim.SetBool("Attacking", false);
            if (FK == null || FK.IsDead)
            {
                rb.MovePosition(transform.position + transform.right * Speed * Time.deltaTime);
                if(IsEnemy)
                    rb.MovePosition(transform.position + -transform.right * Speed * Time.deltaTime);
                if(!IsEnemy)
                {
                    Timer -= Time.deltaTime;
                    if (Timer <= 0)
                    {
                        FindEnemies();
                        Timer = .1f;
                    }
                }
                              
            } 
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, FK.EnemyPoint.transform.position, Speed * Time.deltaTime);
                Distance = Vector2.Distance(gameObject.transform.position, FK.EnemyPoint.transform.position);
                if (Distance <= 10)
                {
                    CanMove = false;
                }
            }
        }
        else if(!IsDead)
        {
            Anim.SetBool("Running", false);
            Anim.SetBool("Attacking", true);
        }

        if(EffectActive)
        {
            EffectTimer -= Time.deltaTime;
            if(EffectTimer <= 0)
            {
                EffectActive = false;
                EffectTimer = 1f;
                AOEEffect.SetActive(false);
            }
        }
    }

    public void FindEnemies()
    {
        Hit = Physics2D.OverlapCircleAll(transform.position, 200 << LayerMask.NameToLayer("Default"));
        for (int i = 0; i < Hit.Length; i++)
        {
            if (!IsEnemy && Hit[i].tag == "Enemy" && Hit[i].GetComponent<BaseKnightMovement>().Attackable)
            {
                FK = Hit[i].GetComponent<BaseKnightMovement>();
                if (FK.GetComponent<Archer>())
                    FK.GetComponent<Archer>().ArcherOpponent = this;
                FK.Attackable = false;
                FK.FK = this;
                FK.OnDeath += TargetDied;
                OnDeath += FK.TargetDied;
                break;

            }
            else if (IsEnemy && Hit[i].tag == "Friendly" && Hit[i].GetComponent<BaseKnightMovement>().Attackable)
            {
                FK = Hit[i].GetComponent<BaseKnightMovement>();
                if (FK.GetComponent<Archer>())
                    FK.GetComponent<Archer>().ArcherOpponent = this;
                FK.Attackable = false;
                //FK.OnDeath += TargetDied;
                OnDeath += FK.TargetDied;
                break;
            }
        }
    }
    public void DealDamage()
    {
        if (AtEnemyBase)
        {
            BB.GetComponent<IDamagable>().TakeDamage(Damage, this);
        }
            
        else
        {
            if(FK != null)
            {
                FK.TakeDamage(Damage, this);
            }  
        }   
    }

    public void StopAOEEffect()
    {
        EffectActive = true;
    }

    public void TakeDamage(int Damage, BaseKnightMovement Sender)
    {
        Health -= Damage;
        if(Sender.GetComponent<Mage>())
        {
            AOEEffect.SetActive(true);
            EffectActive = true;
        }
        if (Health <= 0)
        {
            //OnDeath(FK);
            HasDied();
        }
        //StartCoroutine(RepeatAttack(Damage));
    }
    IEnumerator RepeatAttack(int damage)
    {
        Health--;
        yield return new WaitForSeconds(1);
        TakeDamage(1, null);
        StartCoroutine(RepeatAttack(1));
    }
  
    public void TargetDied()
    {
        if(FK != null)
        {
            FK.OnDeath -= TargetDied;
            OnDeath -= FK.TargetDied;
            FK.Attackable = true;
        }        
        FK = null;
        if(!IsDead)
        CanMove = true;
        Attackable = true;      
    }
    public void HasDied()
    {
        if (!IsEnemy && !IsDead)
        {
            if(EType == EnemyType.Soldier)
                PSM.Soldiers--;
            else if(EType == EnemyType.Mage)
                PSM.Mages--;
            else if (EType == EnemyType.Ninja)
                PSM.Ninjas--;
            if(!GetComponent<Archer>())
            PSM.BarracksSpace--;
        }
        else if(IsEnemy && !IsDead)
        {
            if (EType == EnemyType.Soldier)
            {
                PSM.Money += PSM.SoldierKillAmount;
                PopUp(PSM.SoldierKillAmount);
            }
            else if (EType == EnemyType.Archer)
            {
                PSM.Money += PSM.ArcherKillAmount;
                PopUp(PSM.ArcherKillAmount);
            }      
            else if (EType == EnemyType.Mage)
            {
                PSM.Money += PSM.MageKillAmount;
                PopUp(PSM.MageKillAmount);
            }                
            else if (EType == EnemyType.Ninja)
            {
                PSM.Money += PSM.NinjaKillAmount;
                PopUp(PSM.NinjaKillAmount);
            }
                
            UIManager.Instance.UpdateUI();
        }
        if (!IsDead)
            gameObject.AddComponent<DestroyUnits>();
        IsDead = true;
        CanMove = false;
        Anim.SetBool("Death", true);
        if (OnDeath != null)
        {
            OnDeath();
            if(FK != null)
                //FK.OnDeath -= TargetDied;
                OnDeath -= FK.TargetDied;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        AtEnemyBase = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsEnemy && other.tag == "EnemyBase")
        {
            CanMove = false;
            BB = other.GetComponent<BattleBases>();
            AtEnemyBase = true;
        }
        else if (IsEnemy && other.tag == "FriendlyBase")
        {
            CanMove = false;
            BB = other.GetComponent<BattleBases>();
            AtEnemyBase = true;
        }
    }

    public void PopUp(int MoneyAmount)
    {
        PopUpText.text = "+" + MoneyAmount + " Gold";
        PopUpText.gameObject.SetActive(true);
        StartCoroutine(StopPopup());
    }
    private IEnumerator StopPopup()
    {
        yield return new WaitForSeconds(1);
        PopUpText.gameObject.SetActive(false);
    }
    public void KillThis()
    {
        //BM.DestroyAllUnits -= KillThis;
        AtEnemyBase = false;
        Destroy(GetComponent<DestroyUnits>());
        if (FK != null)
            OnDeath -= FK.TargetDied;
        gameObject.SetActive(false);
    }

}

