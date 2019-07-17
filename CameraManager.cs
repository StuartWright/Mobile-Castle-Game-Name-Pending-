using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public float dragSpeed = 50;
    private Vector3 dragOrigin;
    private bool IsEnemyCastleScreen;
    private BattleManager BM;
    public Animator Anim;

    void Start ()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        BM = GetComponent<BattleManager>();
        BM.StartBattle += BattleScreen;
        BM.ReturnToCastle += MoveFromBattleScreen;
        Anim = GetComponent<Animator>();
        /*
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 850.0f / 480.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1f)
        {
            Rect rect = camera.rect;

            rect.width = 1;
            rect.height = scaleheight;
            rect.x = 1f; //0
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
        */
    }
    /*
    private void Update()
    {
        if(IsEnemyCastleScreen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, 0);
            //pos.x = Mathf.Clamp(transform.position.x, 300, 500);
            transform.Translate(move, Space.World);
            
        }
        
    }
    */
    public void MinerScreen()
    {
        Anim.SetBool("MoveFromMine", false);
        Anim.SetBool("MoveToMine", true);        
    }
    public void MoveFromMine()
    {
        Anim.SetBool("MoveToMine", false);
        Anim.SetBool("MoveFromMine", true);
    }
    public void PlayerCastle()
    {
        Anim.applyRootMotion = false;
        transform.position = new Vector3(425, 242, -1);
        IsEnemyCastleScreen = false;
        //BattleManager.Instance.ReturnToCastle -= PlayerCastle;
    }
    public void EnemyCastles()
    {
        Anim.applyRootMotion = true;
        transform.position = new Vector3(419, 816, -1);
        IsEnemyCastleScreen = true;
    }
    public void BattleScreen()
    {
        StopAnims();
        Anim.applyRootMotion = false;
        transform.position = new Vector3(425, 242, -1);
        Anim.SetBool("MoveToBattle", true);
    }
    public void MoveFromBattleScreen()
    {
        Anim.SetBool("MoveFromBattle", true);
    }
    public void StopAnims()
    {
        Anim.SetBool("MoveToBattle", false);
        Anim.SetBool("MoveFromBattle", false);
        Anim.SetBool("MoveToMine", false);
        Anim.SetBool("MoveFromMine", false);
    }
}
