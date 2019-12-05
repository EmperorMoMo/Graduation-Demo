using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate02 : MonoBehaviour
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
        Player.transform.position = new Vector3(38, 0, 57);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
