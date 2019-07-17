using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mining_Class : MonoBehaviour
{
    
    public float nextPay;
    public int moneyToGive;
    public int nextMoneyToGive;
    public float paytime;

	// Use this for initialization
	void Start ()
    {
        paytime = 1;
        nextMoneyToGive = 1;
        moneyToGive = nextMoneyToGive;
        nextPay = paytime;
        StartCoroutine(PayMe());
	}
	
	// Update is called once per frame
	void Update ()
    {
     
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            UpgradeItems();
        }
	}

    public IEnumerator PayMe()
    {
        yield return new WaitForSeconds(nextPay);
        AddMoney();
        StartCoroutine(PayMe());
    }
    public void AddMoney()
    {
        PlayerStatManager.Instance.Money += moneyToGive;
        nextPay = paytime; 
    }

    public void UpgradeItems()
    {
        paytime -= 0.01f;
        nextPay = paytime;
        moneyToGive += nextMoneyToGive;
        nextMoneyToGive += 2;
        

    }
}
