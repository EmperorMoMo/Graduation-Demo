using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private ActorController ac;

    private Rigidbody rig;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GameObject.Find("PlayerHandle").GetComponent<ActorController>();
        rig = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        print(other.gameObject.name);
        rig.GetComponent<Rigidbody>().freezeRotation = true;
        rig.AddExplosionForce(75, transform.position, 3, 20);
        //ac.Select(ac.str);
    }
}
