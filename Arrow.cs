using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IKillable
{
    private int Speed = 250;
    public GameObject Target;
    public BattleManager BM;
    public int Damage;
    public BaseKnightMovement Sender;
	void Start ()
    {
        //BM = Camera.main.GetComponent<BattleManager>();
        BattleManager.Instance.DestroyAllUnits += KillThis;
    }
	
	void Update ()
    {
        if (Target == null)
        {
            KillThis();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
            transform.LookAt(Target.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Target)// || collision.GetComponent<BattleBases>())
        {
            collision.GetComponent<IDamagable>().TakeDamage(Damage, Sender);
            KillThis();
        }
        //else if (Target.GetComponent<BaseKnightMovement>().IsDead)
            //Destroy(gameObject);

    }

    public void KillThis()
    {
        //BM.DestroyAllUnits -= KillThis;
        if(gameObject != null)
        Destroy(gameObject);
        BattleManager.Instance.DestroyAllUnits -= KillThis;
    }
}
