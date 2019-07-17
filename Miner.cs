using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miner : MonoBehaviour, IPopUp
{
    public int GoldAmount;
    public float GemChance;
    public GameObject MineButton;
    public Animator Anim;
    public Transform StartPoint;
    public Transform MinePoint;
    public Transform Point;
    public Text PopUpText;
    public float Speed = 200;
    private bool IsMining;
    private bool Mined;
    public float WaitTime = 3;
    public int MinerNum;
    
    void Awake ()
    {
        //MineCostText.text = "Buy Mine: " + MineCost;
        Anim = GetComponent<Animator>();
        //GetSaves.Instance.SaveAll += Save;
        Point = MinePoint;
        if (PlayerPrefs.HasKey("AnimSpeed"))
            Load();
    }
    private void FixedUpdate()
    {
       float MinePointDistance = Vector2.Distance(gameObject.transform.position, MinePoint.transform.position);
       float StartPointDistance = Vector2.Distance(gameObject.transform.position, StartPoint.transform.position);
        transform.position = Vector2.MoveTowards(transform.position, Point.transform.position, Speed * Time.deltaTime);
        if(MinePointDistance <= 0)
        {
            Anim.SetBool("Mine", true);
            if (!IsMining)
            {
                IsMining = true;
                StartCoroutine(Mining());
            }
        }
        else if(StartPointDistance <= 0)
        {
            Point = MinePoint;
            Anim.SetBool("RunLeft", true);
            Anim.SetBool("RunRight", false);
            if(!Mined)
            HasMined();
        }
            
    }

    IEnumerator Mining()
    {
        yield return new WaitForSeconds(WaitTime);
        Point = StartPoint;
        Anim.SetBool("RunRight", true);
        Anim.SetBool("Mine", false);
        Anim.SetBool("RunLeft", false);
        IsMining = false;
        Mined = false;
    }
    public void HasMined()
    {
        PlayerStatManager.Instance.Money += GoldAmount;
        float RandomNum = Random.Range(0, 100);
        if (RandomNum <= GemChance)
            PlayerStatManager.Instance.Gems++;
        UIManager.Instance.UpdateGold();
        Mined = true;
        PopUp(GoldAmount);
    }

    public void PopUp(int Money)
    {
        PopUpText.gameObject.SetActive(true);
        PopUpText.text = "+" + Money + " Gold";
        StartCoroutine(StopPopup());
    }
    private IEnumerator StopPopup()
    {
        yield return new WaitForSeconds(1);
        PopUpText.gameObject.SetActive(false);
    }

    public void Save()
    {
        if(MinerNum == 0)
        {
            PlayerPrefs.SetInt("GoldAmount", GoldAmount);
            PlayerPrefs.SetFloat("GemChance", GemChance);
            PlayerPrefs.SetFloat("AnimSpeed", Speed);
        }
        else if(MinerNum == 1)
        {
            PlayerPrefs.SetInt("1GoldAmount", GoldAmount);
            PlayerPrefs.SetFloat("1GemChance", GemChance);
            PlayerPrefs.SetFloat("1AnimSpeed", Speed);
        }
        else if (MinerNum == 2)
        {
            PlayerPrefs.SetInt("2GoldAmount", GoldAmount);
            PlayerPrefs.SetFloat("2GemChance", GemChance);
            PlayerPrefs.SetFloat("2AnimSpeed", Speed);
        }
    }
    public void Load()
    {
        if(MinerNum == 0)
        {
            GoldAmount = PlayerPrefs.GetInt("GoldAmount");
            GemChance = PlayerPrefs.GetFloat("GemChance");
            Speed = PlayerPrefs.GetFloat("AnimSpeed");
        }
        else if (MinerNum == 1)
        {
            GoldAmount = PlayerPrefs.GetInt("1GoldAmount");
            GemChance = PlayerPrefs.GetFloat("1GemChance");
            Speed = PlayerPrefs.GetFloat("1AnimSpeed");
        }
        else if (MinerNum == 2)
        {
            GoldAmount = PlayerPrefs.GetInt("2GoldAmount");
            GemChance = PlayerPrefs.GetFloat("2GemChance");
            Speed = PlayerPrefs.GetFloat("2AnimSpeed");
        }
    }
}
