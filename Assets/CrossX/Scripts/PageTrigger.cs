using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageTrigger : MonoBehaviour
{
    public GameObject Key;
    public Text note;
    private bool isTriggered = false;
    private float lerp = 0;


    private void Update()
    {
        if (isTriggered)
        {
            note.color = Color32.Lerp(Color.black, Color.red, lerp);
            lerp += 0.01f;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.name);
        if (coll.tag == "Pen")
        {
            isTriggered = true;
            Invoke("Triggered", 1.5f);
        }
    }

    void Triggered()
    {
        Key.GetComponent<Animator>().SetBool("show", true);
        Key.GetComponent<MeshRenderer>().enabled = true;
        // door2.GetComponent<Animator>().SetBool("isUnlocked", true);
        Key.GetComponent<Rigidbody>().useGravity = true;
        Destroy(Key.GetComponent<Animator>());
        // Destroy(coll.gameObject);
        // Destroy(gameObject);
    }
}
