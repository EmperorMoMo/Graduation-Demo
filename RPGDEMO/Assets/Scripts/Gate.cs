using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
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
            SceneManager.LoadScene("CheckPoint01");
            DontDestroyOnLoad(Player);
            transformChange();
        }
    }
    void transformChange()
    {
        Player.transform.position = new Vector3(-12, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
