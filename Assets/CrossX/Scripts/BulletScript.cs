using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

    public float destroyAfter;
    public bool destroyOnImpact = false;
    public float destroyTime;

    private void Start()
    {
        StartCoroutine(DestroyAfter());
    }

    //If the bullet collides with anything
    private void OnCollisionEnter(Collision collision)
    {
        //If destroy on impact is false, start 
        //coroutine with random destroy timer
        if (!destroyOnImpact)
        {
            StartCoroutine(DestroyTimer(destroyTime));
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log(111);
        if (collision.transform.tag == "Zombie")
        {
            Debug.Log(2222);
            //Toggle "isHit" on target object
            collision.transform.gameObject.GetComponent
                <ZombieManager>().isDead = true;
            //Destroy bullet object
            StartCoroutine(DestroyTimer(destroyTime));
        }
    }

    private IEnumerator DestroyTimer(float time)
    {
        yield return new WaitForSeconds
            (time);
        //Destroy bullet object
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(destroyAfter);
        //Destroy bullet object
        Destroy(gameObject);
    }
}