﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate01 : MonoBehaviour
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
            mainCamera.cullingMask=1<<1;
            mainCamera.backgroundColor = Color.black;
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            DontDestroyOnLoad(Player);
            SceneManager.LoadScene("CheckPoint 1");
            transformChange();
        }
    }
    void transformChange()
    {
        Player.transform.position = new Vector3(21, 0, -215.5f);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
