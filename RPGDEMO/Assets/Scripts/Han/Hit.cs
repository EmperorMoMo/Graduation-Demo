using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    private ActorController ac;
    // Start is called before the first frame update
    void Awake()
    {
        ac = this.transform.GetComponentInParent<ActorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Hit1()
    {
        if (ac.str == ActorController.State.normalAtk&&ac.isDam)
        {
            ac.Stop(0.1f);
        }
    }
}
