using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : BaseKnightMovement
{
    protected Collider2D[] AOE;
    public int AoeRange = 50;
    public new void DealDamage()
    {
        if (AtEnemyBase)
            BB.GetComponent<IDamagable>().TakeDamage(Damage, this);
        else
        {
            //if (FK != null)
               // FK.TakeDamage(Damage);
            AOE = Physics2D.OverlapCircleAll(transform.position, AoeRange << LayerMask.NameToLayer("Default"));
            foreach(Collider2D Target in AOE)
            {
                if(!IsEnemy && Target.tag == "Enemy")
                {
                    Target.GetComponent<IDamagable>().TakeDamage(Damage, this);
                }
                else if (IsEnemy && Target.tag == "Friendly")
                {
                    Target.GetComponent<IDamagable>().TakeDamage(Damage, this);
                }
            }
        }
    }
}
