using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 将物体如NPC的名字显示在UI界面上
/// </summary>
public class NPCName : MonoBehaviour {
    public string Name;         //要显示的文本
    public GameObject Text;     //游戏中的文本对象预制体
    private GameObject text;    //游戏中的文本对象实体
    private Collider MyCollider;  //物体模型的Collider
    private float npcHeight;    //物体模型的高度

    public bool isRendering;    //是否渲染，是否在摄像机前
    float curtTime = 0f;        //最近时间
    //float lastTime = 0f;        //最后时间
    void Start() {
        float size_y;           //模型y轴高度
        float scal_y;           //模型y轴缩放比例
        MyCollider = transform.GetComponentInParent<Collider>();    //物品Collider组件

        size_y = MyCollider.bounds.size.y;
        scal_y = MyCollider.transform.localScale.y;
        npcHeight = (size_y * scal_y);

        text = GameObject.Instantiate(Text, GameObject.Find("NPCName").transform);      //通过文本对象预制体复制一个实体，父物体设置为“NPCName”
        text.name = Name;   //文本对象名
        text.transform.GetChild(0).GetComponent<Text>().text = Name;    //文本对象内容
        //物体头顶位置3d坐标
        Vector3 worldPosition = new Vector3(this.transform.position.x, this.transform.position.y + npcHeight, this.transform.position.z);
        Vector2 position = Camera.main.WorldToScreenPoint(worldPosition);   //转换为2d屏幕坐标

        text.transform.position = position;     //移动到指定坐标
        text.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate() {
            //得到NPC头顶在3D世界中的坐标
            //默认NPC坐标点在脚底下，所以这里加上npcHeight它模型的高度即可
            Vector3 worldPosition = new Vector3(this.transform.position.x, this.transform.position.y + npcHeight, this.transform.position.z);
            //根据NPC头顶的3D坐标换算成它在2D屏幕中的坐标
            Vector2 targetPosition = Camera.main.WorldToScreenPoint(worldPosition);
            Vector2 position = text.transform.position;
            //物体是否在摄像机前
            if (IsInView(worldPosition)) {
                if (text != null) {
                    //主角与物体之间距离小于20
                    if (Vector3.Distance(GameObject.Find("Hoshi").transform.position, MyCollider.transform.position) <= 20) {
                        if (!text.activeSelf) {
                            text.SetActive(true);
                        }
                    } else {
                        if (text.activeSelf) {
                            text.SetActive(false);
                        }
                    }
                }
                position.x = Mathf.Lerp(position.x, targetPosition.x, 0.5f);
                position.y = Mathf.Lerp(position.y, targetPosition.y, 0.5f);
                text.transform.position = position;
            } else {
                if (text.activeSelf) {
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
}
