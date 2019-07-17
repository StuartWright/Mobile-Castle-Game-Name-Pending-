using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUnits : MonoBehaviour
{
    private float Timer = 2.5f;
    private float ArrowTimer = 3.5f;
    bool IsKnight;
    bool IsArrow;
    private void Start()
    {
        if (GetComponent<BaseKnightMovement>())
            IsKnight = true;
        else if (GetComponent<Arrow>())
            IsArrow = true;
    }
    void Update ()
    {
        if(IsKnight)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                GetComponent<IKillable>().KillThis();
            }
        }
        else if(IsArrow)
        {
            ArrowTimer -= Time.deltaTime;
            if (ArrowTimer <= 0)
            {
                GetComponent<IKillable>().KillThis();
            }
        }
	}
}
