using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSaves : MonoBehaviour
{
    public static GetSaves Instance;
    public delegate void GetValues();
    public event GetValues SaveAll;
    private bool StartNewGame = false;
    public int money;
    public GameObject NewGameButton;
    void Awake ()
    {
        Instance = this;
    }
	
    public void Values(PlayerData sender)
    {
        sender.Money = money;
    }

    private void OnApplicationQuit()
    {
        if(!StartNewGame)
            SaveGame();
    }
    public void SaveGame()
    {
        SaveAll();
    }
    public void NewGame()
    {
        StartNewGame = true;
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void OpenPanel()
    {
        NewGameButton.SetActive(true);
    }
    public void ClosePanel()
    {
        NewGameButton.SetActive(false);
    }
}
