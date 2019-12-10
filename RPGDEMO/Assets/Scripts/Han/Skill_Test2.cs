﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Test2 : MonoBehaviour
{
    private Vector3 target_2;
    private EnemyAI ai;

    private Collider box;
    // Start is called before the first frame update
    void Awake()
    {
        target_2 = GameObject.Find("Target_2").transform.position;
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float t1 = 0f;
        t1 += 1f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target_2, t1 * 3.3f);
        Destroy(this.gameObject, 1f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            ai = col.GetComponent<EnemyAI>();
            print("=============================");
            //col.GetComponent<Rigidbody>().freezeRotation = true;
            //col.GetComponent<Rigidbody>().AddExplosionForce(300, col.transform.position, 3, 200);
            //col.GetComponent<Rigidbody>().AddForce(col.transform.position*100f);
            float t2 = 0f;
            t2 += 1f * Time.deltaTime;
            col.transform.position = Vector3.Lerp(col.transform.position, -col.transform.forward* 90f, t2*0.8f);

            //col.transform.Translate(Vector3.Lerp(col.transform.position, -col.transform.forward, t2),
            //    Space.World);
            ai.Damage();
        }

        //if (col.tag == "wall")
        //{
        //    Destroy(this.gameObject);
        //}
    }

}
