using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCName : MonoBehaviour {
    public string Name;
    public GameObject Text;
    private GameObject text;
    private float npcHeight;
    private Collider collider;

    void Start() {
        float size_y;
        float scal_y;
        collider = GetComponent<Collider>();

        size_y = collider.bounds.size.y;
        scal_y = collider.transform.localScale.y;
        npcHeight = (size_y * scal_y);
        text = GameObject.Instantiate(Text, GameObject.Find("NPCName").transform);
        text.name = Name;
        text.transform.GetChild(0).GetComponent<Text>().text = Name;

        Vector3 worldPosition = new Vector3(collider.transform.position.x, collider.transform.position.y + npcHeight, collider.transform.position.z);
        Vector2 position = Camera.main.WorldToScreenPoint(worldPosition);

        text.transform.position = position;
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        //得到NPC头顶在3D世界中的坐标
        //默认NPC坐标点在脚底下，所以这里加上npcHeight它模型的高度即可
        Vector3 worldPosition = new Vector3(collider.transform.position.x, collider.transform.position.y + npcHeight, collider.transform.position.z);
        //根据NPC头顶的3D坐标换算成它在2D屏幕中的坐标
        Vector2 targetPosition = Camera.main.WorldToScreenPoint(worldPosition);
        text.transform.position = targetPosition;   
    }

    public void OnBecameVisible() {
        if (text != null) {
            if (!text.activeSelf) {
                text.SetActive(true);
            }
        }
    }

    public void OnBecameInvisible() {
        if (text != null) { 
            if (text.activeSelf) {
                text.SetActive(false);
            }
        }
    }
}
