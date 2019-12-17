using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Test2 : MonoBehaviour
{
    private Vector3 target_2;
    private EnemyAttribute ai;
    private BossAttribute ba;
    private EnemyAttribute1 ai1;
    private EnemyAttribute2 ai2;

    private CharacterAttribute ca;
    //private Collider box;
    //// Start is called before the first frame update
    void Awake()
    {
        target_2 = GameObject.Find("Target_1").transform.position;
        ca = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        //box = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 2.5f);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Enemy")
                {
                    ai = cols[i].GetComponent<EnemyAttribute>();
                    float t2 = 0f;
                    t2 += 1f * Time.fixedDeltaTime;
                    cols[i].transform.position = Vector3.Lerp(cols[i].transform.position, -cols[i].transform.forward*20f, t2*1f);
                    ai.Enemy_Attacked(ca.finalAttribute.Aggressivity*0.3f);
                }

                if (cols[i].tag == "Boss")
                {
                    ba = cols[i].GetComponent<BossAttribute>();
                    ba.Boss_Attacked(ca.finalAttribute.Aggressivity * 0.3f);
                }

                if (cols[i].tag == "Enemy1")
                {
                    ai1 = cols[i].GetComponent<EnemyAttribute1>();
                    float t2 = 0f;
                    t2 += 1f * Time.fixedDeltaTime;
                    cols[i].transform.position = Vector3.Lerp(cols[i].transform.position, -cols[i].transform.forward * 20f, t2 * 1f);
                    ai1.Enemy_Attacked1(ca.finalAttribute.Aggressivity * 0.3f);
                }

                if (cols[i].tag == "Enemy2")
                {
                    ai2 = cols[i].GetComponent<EnemyAttribute2>();
                    float t2 = 0f;
                    t2 += 1f * Time.fixedDeltaTime;
                    cols[i].transform.position = Vector3.Lerp(cols[i].transform.position, -cols[i].transform.forward * 20f, t2 * 1f);
                    ai2.Enemy_Attacked2(ca.finalAttribute.Aggressivity * 0.3f);
                }
            }
        }
    }

    void Update()
    {
        float t1 = 0f;
        t1 += 1f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target_2, t1 * 3.8f);
        Destroy(this.gameObject, 0.9f);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    float t1 = 0f;
    //    t1 += 1f * Time.deltaTime;
    //    transform.position = Vector3.Lerp(transform.position, target_2, t1 * 3.3f);
    //    Destroy(this.gameObject, 1f);
    //}

    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.tag == "Enemy")
    //    {
    //        ai = col.GetComponent<EnemyAI>();
    //        print("=============================");
    //        //col.GetComponent<Rigidbody>().freezeRotation = true;
    //        //col.GetComponent<Rigidbody>().AddExplosionForce(300, col.transform.position, 3, 200);
    //        //col.GetComponent<Rigidbody>().AddForce(col.transform.position*100f);
    //        float t2 = 0f;
    //        t2 += 1f * Time.deltaTime;
    //        col.transform.position = Vector3.Lerp(col.transform.position, -col.transform.forward* 90f, t2*0.8f);

    //        //col.transform.Translate(Vector3.Lerp(col.transform.position, -col.transform.forward, t2),
    //        //    Space.World);
    //        ai.Damage();
    //    }

    //    //if (col.tag == "wall")
    //    //{
    //    //    Destroy(this.gameObject);
    //    //}
    //}

}
