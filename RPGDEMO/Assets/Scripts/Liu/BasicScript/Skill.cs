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
public class Skill : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private int SlotIndex = -1;
    private Transform SkillCopy;
    private Transform SkillSpare;
    private Transform Parent;

    public void Start() {
        SkillCopy = this.transform.parent.GetChild(0);
        SkillSpare = this.transform.parent.GetChild(1);
        Parent = this.transform.parent;
    }
    public void Update() {
        
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData) {
        //鼠标左键按住
        if (Input.GetMouseButton(0)) {
            this.transform.position = eventData.position;
            this.transform.SetParent(UIManager.Canvas.transform);
            this.transform.GetChild(0).GetComponent<Image>().raycastTarget = false; //关闭当前组件中Image的射线检测
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
            this.transform.SetParent(Parent);
            this.transform.position = this.transform.parent.position;
            this.transform.GetChild(0).GetComponent<Image>().raycastTarget = true; //开启当前组件中Image的射线检测
        }
    }

    public void ToEmpty(int slotIndex){
        SkillCopy.transform.SetParent(DataManager.SlotArr[slotIndex].transform);
        SkillCopy.transform.position = SkillCopy.transform.parent.position;
        SkillCopy.transform.GetChild(0).GetComponent<Image>().raycastTarget = true;                  //开启当前组件中Image的射线检测
    }
}
