using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float HP = 1000f;

    private Transform playerTrans;

    private Rigidbody rig;

    public GameObject Effect;

    private Transform hitpoint;
    // Start is called before the first frame update
    void Awake()
    {
        playerTrans = GameObject.Find("Follow").GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        hitpoint = this.transform.GetChild(1).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation=Quaternion.identity;
        rotation = Quaternion.LookRotation(playerTrans.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*5f);
    }

    public void Damage()
    {
        InstantiateEffect();
        HP = HP - 5f;
        if (HP < 0)
        {
            Debug.LogError("HP values is Error!");
        }
        else
        {
            print(gameObject.name+"还剩："+HP+"HP");
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "wall")
    //    {
    //        //ContactPoint contact = collision.contacts[0];
    //        ////Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //        //Vector3 pos1 = contact.point;
    //        ////Vector3 m_forceHit1 = new Vector3(-pos1.normalized.x, -pos1.normalized.y, -pos1.normalized.z) * 40;
    //        ////this.gameObject.GetComponent<Rigidbody>().AddForce(m_forceHit1, ForceMode.Force);
    //        //transform.position = pos1;
    //        //transform.position = collision.transform.position;

    //        //this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 100f);

    //        //rig.AddExplosionForce(100,collision.transform.position,3);
    //        transform.position = Vector3.Lerp(transform.position,
    //            (collision.transform.position - transform.position) * 0.3f, Time.deltaTime*3.8f);
    //    }
    //}

    void InstantiateEffect()
    {
        var instance = Instantiate(Effect, hitpoint.position, hitpoint.rotation);
        Destroy(instance,1.0f);
    }
}
