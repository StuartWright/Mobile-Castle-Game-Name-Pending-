using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEverything : MonoBehaviour
{
    public static FindEverything Instance;

    public GameObject FriendlyBase;
    public GameObject EnemyBase;
	void Start ()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
            Destroy(gameObject);

        FriendlyBase = GameObject.Find("FriendlyBase");
        EnemyBase = GameObject.Find("EnemyBase");
	}
}
