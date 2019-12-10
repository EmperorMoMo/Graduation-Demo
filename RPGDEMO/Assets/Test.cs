using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private ActorController ac;

    private Rigidbody rig;

    private EnemyAI ai;

    private Collider col;

    //public ParticleSystem part;

    //public List<ParticleCollisionEvent> collisionEvents;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GameObject.Find("PlayerHandle").GetComponent<ActorController>();
        rig = GetComponent<Rigidbody>();

        ai = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();

        col = this.GetComponent<Collider>();

        //part = GetComponent<ParticleSystem>();
        //collisionEvents=new List<ParticleCollisionEvent>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        print(other.gameObject.name);
        //Physics.IgnoreCollision(other.GetComponent<Collider>(), col);
        rig.GetComponent<Rigidbody>().freezeRotation = true;
        rig.AddExplosionForce(75, transform.position, 3, 20);
        ai.Damage();
        //ac.Select(ac.str);
    }

    //void OnParticleTrigger()
    //{
    //    print("触发了");

    //}

    //void OnParticleCollision(GameObject other)
    //{
    //    int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

    //    Rigidbody rb = other.GetComponent<Rigidbody>();
    //    int i = 0;

    //    while (i<numCollisionEvents)
    //    {
    //        if (rb)
    //        {
    //            Vector3 pos = collisionEvents[i].intersection;
    //            Vector3 force = collisionEvents[i].velocity * 10;

    //            rb.AddForce(force);
    //        }

    //        i++;
    //    }
    //}
}
