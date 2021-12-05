using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private Animator animator;

    public Transform StartPosition;

    [Tooltip("The point this enemy should go")]
    public Transform Target;

    public float speed = 0.01f;

    public bool isDead = false;

    private bool isArrived = false;

    public List<Rigidbody> RagdollRigidbodys = new List<Rigidbody>();
    public List<Collider> RagdollColliders = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InitRagdoll();
        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Target.position) <= 3 && !isArrived)
        {
            isArrived = true;
            animator.SetBool("isArrived", true);
        }

        if (isDead)
        {

            animator.SetBool("ZombieDead", true);
            EnableRagdoll();
            // animator.Play("Z_FallingBack", 0, 0f);
        }

        if (!isArrived && !isDead && Vector3.Distance(transform.position, Target.position) <= 20)
        {
            animator.SetBool("findHero", true);
            Debug.Log(Target.position);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.position.x, transform.position.y, Target.position.z), speed);

            //Rotate
            Vector3 vec = (Target.position - transform.position);
            Quaternion rotate = Quaternion.LookRotation(vec);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotate, 0.03f);
        }
    }




    void InitRagdoll()
    {
        Rigidbody[] Rigidbodys = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < Rigidbodys.Length; i++)
        {
            if (Rigidbodys[i] == GetComponent<Rigidbody>())
            {
                //排除正常状态的Rigidbody
                continue;
            }
            //添加Rigidbody和Collider到List
            RagdollRigidbodys.Add(Rigidbodys[i]);
            Rigidbodys[i].isKinematic = true;
            Collider RagdollCollider = Rigidbodys[i].gameObject.GetComponent<Collider>();
            RagdollCollider.isTrigger = true;
            RagdollColliders.Add(RagdollCollider);
        }
    }
    
    public void EnableRagdoll()
    {
        //开启布娃娃状态的所有Rigidbody和Collider
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = false;
            RagdollColliders[i].isTrigger = false;
        }
        //关闭正常状态的Collider
        GetComponent<Collider>().enabled = false;
        //下一帧关闭正常状态的动画系统
        StartCoroutine(SetAnimatorEnable(false));
    }

    public void DisableRagdoll()
    {
        //关闭布娃娃状态的所有Rigidbody和Collider
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = true;
            RagdollColliders[i].isTrigger = true;
        }
        //开启正常状态的Collider
        GetComponent<Collider>().enabled = true;
        //下一帧开启正常状态的动画系统
        StartCoroutine(SetAnimatorEnable(true));
    }
    IEnumerator SetAnimatorEnable(bool Enable)
    {
        yield return new WaitForEndOfFrame();
        animator.enabled = Enable;
    }
}