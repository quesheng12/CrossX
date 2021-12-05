using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    public GameObject door1;
    public GameObject door2;

    public Canvas lockedTip;

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.name);
        if (coll.tag == "Key")
        {
            door1.GetComponent<Animator>().SetBool("isUnlocked", true);
            door2.GetComponent<Animator>().SetBool("isUnlocked", true);
            // Destroy(coll.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>());
            // Destroy(coll.gameObject);
            Destroy(gameObject);
        }
        else
        {
            lockedTip.enabled = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        lockedTip.enabled = false;
    }
}
