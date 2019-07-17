using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleBases : MonoBehaviour, IDamagable
{
    public int BaseHealth;
    public bool PlayerBase;
    public BattleManager BM;
    public Text BaseHPText;
    private void Start()
    {
        //BM.StartBattle += ResetBases;
        BM.StartBattle += Init;
    }
    public void ResetBases()
    {
        BaseHealth = 10;
    }
    public void TakeDamage(int Damage, BaseKnightMovement Sender)
    {
        BaseHealth -= Damage;
        BaseHPText.text = "HP: " + BaseHealth;
        if(BaseHealth <= 0)
        {
            if (PlayerBase)
                BattleManager.Instance.PlayerLost();
            else
                BattleManager.Instance.PlayerWon();
        }
    }
    
    public void Init()
    {
        if (!PlayerBase)
            BaseHealth = BM.eBaseHP;
        else
            BaseHealth = PlayerStatManager.Instance.BaseHP;
        BaseHPText.text = "HP: " + BaseHealth;
    }
}
