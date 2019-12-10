using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 网格类
/// 挂载在物体上
/// </summary>
public class Slot : MonoBehaviour, IDropHandler {
    private int SlotIndex = -1;         //该网格的网格索引
    public int ListIndex = -1;          //该网格存放物体的List索引

    //鼠标在该网格上落下
    public void OnDrop(PointerEventData eventData) {
        GameObject dragItemGO = eventData.pointerDrag;              //鼠标拖拽的物品对象
        Item dragItem = dragItemGO.GetComponent<Item>();            //拖拽体的Item脚本
        Slot preSlot = DataManager.SlotGOList[dragItem.SlotIndex]
            .GetComponent<Slot>();                                  //原网格的Slot脚本
        
        //该网格上不存在物品
        if (ListIndex == -1) {
            //如果按住ctrl键,拆分
            if (Input.GetKey(KeyCode.LeftControl) && dragItem.curStack > 1) {
                Split.slot = this;
                Split.item = dragItem;
                GameObject.Find("Backpage").transform.GetChild(3).gameObject.SetActive(true);
            } else { 
            //放置在空位
                DataManager.ItemGOList[SlotIndex] = dragItemGO;         //网格对应List中的物体指向拖拽体
                DataManager.ItemGOList[preSlot.ListIndex] = null;       //原存储拖拽体的List的指向置为空
                preSlot.ListIndex = -1;                                 //原网格的指向置为空
                dragItem.SlotIndex = SlotIndex;                         //拖拽体指向的Slot更新
                ListIndex = SlotIndex;                                  //网格指向List
            }
        } else { 
        //该网格上存在物品
            Item curItem = DataManager.ItemGOList[ListIndex]
                .GetComponent<Item>();                              //网格当前指向的物体
            //如果是相同物品，则进行堆叠操作
            if (dragItem.itemBase.UID == curItem.itemBase.UID) {
                dragItem.Stack(curItem);
            } else {
            //否则进行换位操作
                DataManager.ItemGOList[ListIndex] = dragItemGO;         //网格对应List指向的物体更新为拖拽体
                curItem.SlotIndex = dragItem.SlotIndex;                 //当前物体指向的网格更新为拖拽体指向的网格
                Debug.Log(preSlot.ListIndex);
                DataManager.ItemGOList[preSlot.ListIndex]
                    = curItem.gameObject;                               //原网格指向的物品更新为当前物体
                dragItem.SlotIndex = SlotIndex;                         //拖拽体指向的网格更新为当前网格
                curItem.ReplaceParent();                                //当前物品的父物体更新
            }
        }
    }

    public void SplitItem(Item item, int count) {
        CreateManager CM = GameObject.Find("CreateManager").GetComponent<CreateManager>();
        CM.CreateItem(SlotIndex, item.itemBase.UID, item.itemBase.StackMax);
        Item newItem = DataManager.ItemGOList[ListIndex].GetComponent<Item>();
        newItem.curStack = count;
        item.curStack -= newItem.curStack;
        newItem.ShowCount();
        item.ShowCount();
    }

    //设置网格索引的初始值
    public void SetSlotIndex(int i) {
        SlotIndex = i;
    }
}
