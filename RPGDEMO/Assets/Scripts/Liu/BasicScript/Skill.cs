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
public class Skill : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler{
    public int SkillID;
    public bool isActive;

    public int SlotIndex = -1;
    public Transform SkillCopy;
    public Transform SkillSpare;
    public Transform Parent;
    public Image Cover;

    public bool isEnter = false;
    public bool isShow = false;
    private float stayTime = 0.5f;
    private float stayTimer = 0;
    private Vector3 mousePosition;


    public void Start() {
        if (isActive) {
            DataManager.SkillDic[SkillID] = this;
        }
    }
    public void Update() {
        if (isEnter) {
            if (!isShow) {
                stayTimer += Time.deltaTime;
                if (stayTimer > stayTime) {
                    InfoPanel.ShowSkill(DataManager.SkillBaseDic[SkillID]);
                    mousePosition = Input.mousePosition;
                    isShow = true;
                    stayTimer = 0;
                }
            }
        }

        if (isShow) {
            if (mousePosition != Input.mousePosition) {
                InfoPanel.HidePanel();
                isShow = false;
            }
        }
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData) {
        //鼠标左键按住
        if (Input.GetMouseButton(0) && isActive) {
            this.transform.position = eventData.position;
            this.transform.SetParent(UIManager.Canvas.transform);
            this.transform.GetChild(0).GetComponent<Image>().raycastTarget = false; //关闭当前组件中Image的射线检测
        }
    }

    //在拖拽中
    public void OnDrag(PointerEventData eventData) {
        //鼠标左键按住
        if (Input.GetMouseButton(0) && isActive) {
            this.transform.position = eventData.position;

        }
    }

    //结束拖拽
    public void OnEndDrag(PointerEventData eventData) {
        //鼠标左键弹起
        if (Input.GetMouseButtonUp(0) && isActive) {
            this.transform.SetParent(Parent);
            this.transform.position = this.transform.parent.position;
            this.transform.GetChild(0).GetComponent<Image>().raycastTarget = true; //开启当前组件中Image的射线检测
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        isEnter = false;
        InfoPanel.HidePanel();
        isShow = false;
    }


    public void ToEmpty(int slotIndex){
        SkillCopy.transform.SetParent(DataManager.SlotArr[slotIndex].transform);
        SkillCopy.transform.position = SkillCopy.transform.parent.position;
        SkillCopy.transform.GetChild(0).GetComponent<Image>().raycastTarget = true;                  //开启当前组件中Image的射线检测

        this.SlotIndex = slotIndex;
        DataManager.SlotArr[slotIndex].QuickBarID = SkillID;
        DataManager.SaveQuick();
    }

    public void Homing() {
        if (SlotIndex == -1) {
            return;
        }
        SkillCopy.transform.SetParent(Parent);
        SkillCopy.transform.position = Parent.position;

        DataManager.SlotArr[SlotIndex].QuickBarID = -1;
        this.SlotIndex = -1;
    }

    public void ExChange(Skill curSkill, int slotIndex) {
        if (SlotIndex != -1) {
            int perSlotIndex = SlotIndex;
            Homing();
            curSkill.Homing();
            curSkill.ToEmpty(perSlotIndex);
        } else {
            curSkill.Homing();
        }
        ToEmpty(slotIndex);
    }
}
