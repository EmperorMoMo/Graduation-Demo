using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public bool isIn = false;

    public void OnBecameVisible() {
        isIn = true;
        Debug.Log("True");
    }

    public void OnBecameInvisible() {
        isIn = false;
        Debug.Log("False");
    }
}
