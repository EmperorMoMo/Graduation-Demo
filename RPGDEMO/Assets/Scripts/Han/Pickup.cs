using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private ActorController ac;
    private PlayerInput pi;

    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponentInParent<ActorController>();
        pi = GetComponentInParent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PickUp()
    {
        if (ac.str == ActorController.State.pickup)
        {
            ac.Select(ac.str);
        }
    }
}
