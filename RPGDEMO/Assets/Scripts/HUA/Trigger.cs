using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("TestPlayer");
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag=="TestPlayer")
        {
            SendMessageUpwards("GetMessage","Breath");
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
