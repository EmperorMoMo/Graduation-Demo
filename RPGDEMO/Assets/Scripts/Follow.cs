using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Transform playerTransform;//获取坐标组件
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Test").transform;
        offset = transform.position - playerTransform.position;//偏移（position指位置坐标）
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}
