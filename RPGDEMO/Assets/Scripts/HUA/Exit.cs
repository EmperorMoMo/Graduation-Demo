using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
