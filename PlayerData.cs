using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Money, gems;
	
    public PlayerData()
    {
        GetSaves.Instance.Values(this);
    }
}
