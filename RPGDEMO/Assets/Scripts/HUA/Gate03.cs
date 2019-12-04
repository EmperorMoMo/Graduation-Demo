using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate03 : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Test");
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Test")
        {
            transformChange();
        }
    }
    void transformChange()
    {
        Player.transform.position = new Vector3(24, 0,-370);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
