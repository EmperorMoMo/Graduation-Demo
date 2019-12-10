using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Test : MonoBehaviour
{
    private Animator anim;

    private EnemyAI ai;
    //private Vector3 currentVelocity;
    //private Transform trans;

    private Vector3 target;

    private bool canCollider;

    // Start is called before the first frame update
    void Awake()
    {
        //trans = GetComponent<Transform>();
        target = GameObject.Find("TargetPoint").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        canCollider = false;
        StartCoroutine(stayDelay());
    }

    IEnumerator stayDelay()
    {
        yield return new WaitForSeconds(2.2f);
        canCollider = true;
        //transform.Translate(transform.forward * Time.deltaTime*9f,Space.World);
        float t1 = 0f;
        t1 += 1f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target, t1*3f);
        Destroy(this.gameObject, 1f);
    }

    //void OnTriggerEnter(Collider[] col)
    //{
    //    if (col.Length > 0)
    //    {
    //        for (int i = 0; i < col.Length; i++)
    //        {
    //            if (col[i].tag == "Enemy")
    //            {
    //                print("=============================");
    //                col[i].GetComponent<Rigidbody>().freezeRotation = true;
    //                col[i].GetComponent<Rigidbody>().AddExplosionForce(75, transform.position, 3, 20);
    //            }
    //        }
    //    }
    //}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy" && canCollider)
        {
            ai = col.GetComponent<EnemyAI>();
            //print("=============================");
            //col.GetComponent<Rigidbody>().freezeRotation = true;
            //col.GetComponent<Rigidbody>().AddExplosionForce(300, col.transform.position, 3, 200);
            //col.GetComponent<Rigidbody>().AddForce(col.transform.position*100f);
            float t2 = 0f;
            t2 += 1f * Time.deltaTime;
            col.transform.position = Vector3.Lerp(col.transform.position,(-col.transform.forward*4f+Vector3.up*4f)*10f, t2);
            ai.Damage();
        }
    }

}
