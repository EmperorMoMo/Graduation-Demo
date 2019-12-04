using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGate : MonoBehaviour
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
            SceneManager.LoadScene("MainScene");
            DontDestroyOnLoad(Player);
            transformChange();
        }
    }
    void transformChange()
    {
        Player.transform.position = new Vector3(37, 0, 57);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
