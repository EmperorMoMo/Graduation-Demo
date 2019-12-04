using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate06 : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("TestPlayer");
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "TestPlayer")
        {
            transformChange();
        }
    }
    void transformChange()
    {
        Player.transform.position = new Vector3(55, 0, -370);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
