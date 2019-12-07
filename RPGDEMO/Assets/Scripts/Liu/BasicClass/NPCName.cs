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

    public bool isRendering;
    float curtTime = 0f;
    float lastTime = 0f;
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
        text.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate() {
            //得到NPC头顶在3D世界中的坐标
            //默认NPC坐标点在脚底下，所以这里加上npcHeight它模型的高度即可
            Vector3 worldPosition = new Vector3(collider.transform.position.x, collider.transform.position.y + npcHeight, collider.transform.position.z);
            //根据NPC头顶的3D坐标换算成它在2D屏幕中的坐标
            Vector2 targetPosition = Camera.main.WorldToScreenPoint(worldPosition);

            if (IsInView(worldPosition)) {
                if (text != null) {
                    //Debug.Log(Vector3.Distance(GameObject.Find("Hoshi").transform.position, collider.transform.position));
                    if (Vector3.Distance(GameObject.Find("Hoshi").transform.position, collider.transform.position) <= 20) {
                        if (!text.activeSelf) {
                            //Debug.Log("近True");
                            text.SetActive(true);
                        }
                    } else {
                        if (text.activeSelf) {
                            //Debug.Log("远False");
                            text.SetActive(false);
                        }
                    }
                }
            text.transform.position = targetPosition;
            } else {
                if (text.activeSelf) {
                    //Debug.Log("不在摄像机前");
                    text.SetActive(false);
                }
            }
    }

    void OnWillRenderObject() {
        curtTime = Time.time;
    }

    public bool IsInView(Vector3 worldPos) {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);     
        //判断物体是否在相机前面
        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1) { 
            return true;
        } else {
            return false;
        }
    }

    //public void OnBecameVisible() {
    //    isIn = true;
    //}

    //public void OnBecameInvisible() {
    //    isIn = false;
    //    if (text != null) {
    //        text.SetActive(false);
    //        Debug.Log("在视野外设置为False");
    //    }
    //}
}
