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
            DataManager.ItemGOList[SlotIndex] = dragItemGO;         //网格对应List中的物体指向拖拽体
            DataManager.ItemGOList[preSlot.ListIndex] = null;       //原存储拖拽体的List的指向置为空
            preSlot.ListIndex = -1;                                 //原网格的指向置为空
            dragItem.SlotIndex = SlotIndex;                         //拖拽体指向的Slot更新
            ListIndex = SlotIndex;                                  //网格指向List
        } else { 
        //该网格上存在物品

        }
    }

    //设置网格索引的初始值
    public void SetSlotIndex(int i) {
        SlotIndex = i;
    }
}
