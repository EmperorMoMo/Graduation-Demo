using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowName : MonoBehaviour {
    public string Name;
    public GameObject TextPrefab;
    private GameObject text;

    void Start() {
        text = GameObject.Instantiate(TextPrefab, this.transform);
        this.transform.GetComponentsInChildren<Text>()[0].text = Name;
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        text.transform.position = position;
    }
    void Update() {
        this.transform.GetComponentsInChildren<Canvas>()[0].transform.rotation = Camera.main.transform.rotation;
    }
}
