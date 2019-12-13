using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 物品类基类：所有的物品类子类都继承于此类
/// 一般不挂载在物体上
/// 拥有作为物品的一些逻辑关系
/// 其可执行所有物品公有一些基础操作
/// </summary>
public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public ItemBase itemBase;               //物品基础类对象
    public int curStack = 1;                //物品当前堆叠数量

    public int SlotIndex = -1;              //指向的网格索引

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData) {
        //鼠标左键按住
        if (Input.GetMouseButton(0)) {
            this.transform.SetParent(UIManager.Canvas.transform);       //将物品移至Canvas下
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
            ReplaceParent();
            this.transform.GetComponent<Image>().raycastTarget = true;                  //开启当前组件中Image的射线检测
        }
    }

    //更新父物体
    public void ReplaceParent() {
        this.transform.SetParent(DataManager.SlotArr[SlotIndex].transform);      //将该物品的父物体设置为索引网格
        this.transform.position = this.transform.parent.position;                   //将该物体移动到父物体的位置
    }

    //数量文本更新
    public void ShowCount() {
        string count;
        if (curStack != 1) {
            count = curStack.ToString();
        } else {
            count = "";
        }
        this.transform.GetComponentInChildren<Text>().text = count;
    }
}
