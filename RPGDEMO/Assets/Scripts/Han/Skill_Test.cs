using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Test : MonoBehaviour
{
    private Animator anim;

    private EnemyAttribute ai;
    private BossAttribute ba;

    private CharacterAttribute ca;
    //private Vector3 currentVelocity;
    //private Transform trans;

    private Vector3 target;

    private bool canCollider;

    // Start is called before the first frame update
    void Awake()
    {
        canCollider = false;
        //trans = GetComponent<Transform>();
        target = GameObject.Find("TargetPoint_").transform.position;
        ca = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    canCollider = false;
    //    StartCoroutine(stayDelay());
    //}

    IEnumerator stayDelay()
    {
        yield return new WaitForSeconds(2.2f);
        canCollider = true;
        //transform.Translate(transform.forward * Time.deltaTime*9f,Space.World);
        float t1 = 0f;
        t1 += 1f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target, t1*4f);
        Destroy(this.gameObject, 1.5f);
    }

    void FixedUpdate()
    {
        StartCoroutine(stayDelay());
        if (canCollider)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 5.5f);
            if (cols.Length > 0)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].tag == "Enemy")
                    {
                        //ai=cols[i].GetComponent<EnemyAI>();
                        ai = cols[i].GetComponent<EnemyAttribute>();
                        float t2 = 0f;
                        t2 += 1f * Time.fixedDeltaTime;
                        cols[i].transform.position = Vector3.Lerp(cols[i].transform.position, (-cols[i].transform.forward * 4f + Vector3.up) * 10f, t2);
                        ai.Enemy_Attacked(ca.finalAttribute.Aggressivity*0.5f);
                    }

                    if (cols[i].tag == "Boss")
                    {
                        ba = cols[i].GetComponent<BossAttribute>();
                        ba.Boss_Attacked(ca.finalAttribute.Aggressivity * 0.5f);
                    }
                }
            }
        }
        
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

    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.tag == "Enemy" && canCollider)
    //    {
    //        ai = col.GetComponent<EnemyAI>();
    //        //print("=============================");
    //        //col.GetComponent<Rigidbody>().freezeRotation = true;
    //        //col.GetComponent<Rigidbody>().AddExplosionForce(300, col.transform.position, 3, 200);
    //        //col.GetComponent<Rigidbody>().AddForce(col.transform.position*100f);
    //        float t2 = 0f;
    //        t2 += 1f * Time.deltaTime;
    //        col.transform.position = Vector3.Lerp(col.transform.position,(-col.transform.forward*4f+Vector3.up*4f)*10f, t2);
    //        ai.Damage();
    //    }
    //}

}
