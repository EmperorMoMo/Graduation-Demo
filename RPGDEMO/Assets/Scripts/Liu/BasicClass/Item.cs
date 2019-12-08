﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// 物品类
/// 挂载在物体上
/// </summary>
public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public ItemBase itemBase;               //物品基类对象

    public int SlotIndex = -1;              //物品对应网格的索引

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData) {
        //鼠标左键按住
        if (Input.GetMouseButton(0)) {
            this.transform.SetParent(this.transform.parent.parent);     //将GameObject上移一层
            this.transform.position = eventData.position;               //设置为鼠标位置
            this.transform.GetComponent<Image>().raycastTarget = false; //关闭当前组件中Image的射线检测
        }
    }

    //在拖拽中
    public void OnDrag(PointerEventData eventData) {
        //鼠标左键按住
        if (Input.GetMouseButton(0)) {
            this.transform.position = eventData.position;
        }
    }

    //结束拖拽
    public void OnEndDrag(PointerEventData eventData) {
        //鼠标左键弹起
        if (Input.GetMouseButtonUp(0)) {
            Debug.Log("text");
            this.transform.SetParent(DataManager.SlotGOList[SlotIndex].transform);      //将该物品的父物体设置为索引网格
            this.transform.position = this.transform.parent.position;                   //将该物体移动到父物体的位置
            this.transform.GetComponent<Image>().raycastTarget = true;                  //开启当前组件中Image的射线检测
        }
    }
}
