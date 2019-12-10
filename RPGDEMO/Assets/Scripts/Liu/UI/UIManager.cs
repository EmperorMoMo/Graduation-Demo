using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject Funcation;

    void Start() {
        Funcation = this.transform.GetChild(0).gameObject;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (!Funcation.activeSelf) {
                Funcation.SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab)) {
            if (Funcation.activeSelf) {
                Funcation.SetActive(false);
            }
        }
    }
}
