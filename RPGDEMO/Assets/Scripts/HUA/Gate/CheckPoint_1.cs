using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint_1 : MonoBehaviour
{
    private GameObject Player;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Player = GameObject.FindWithTag("TestPlayer");
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "TestPlayer")
        {
            mainCamera.cullingMask = 1 << 1;
            mainCamera.backgroundColor = Color.black;
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            DontDestroyOnLoad(Player);
            SceneManager.LoadScene("CheckPoint1.1");
            transformChange();
        }
    }
    void transformChange()
    {
        Player.transform.position = new Vector3(0.5f, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
