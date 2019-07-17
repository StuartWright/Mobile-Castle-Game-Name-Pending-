using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour
{

	public void ClosePanel()
    {
        CameraManager.Instance.StopAnims();
        WaveManager.Instance.WaveActive = false;
        gameObject.SetActive(false);
    }
}
