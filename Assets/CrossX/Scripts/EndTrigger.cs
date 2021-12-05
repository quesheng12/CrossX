using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public Canvas endCanvas;
    public MasterController hero;

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.name);
        if (coll.tag == "Player")
        {
            endCanvas.enabled = true;
            hero.gameOver = true;
        }
    }
}
