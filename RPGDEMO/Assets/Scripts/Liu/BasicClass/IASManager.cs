﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品网格向导
/// 负责创建物品网格，移动物品，更改物品与网格的对应关系等
/// </summary>
public class IASManager : MonoBehaviour {
    public GameObject slotPrefab;
    public GameObject itemPrefab;
    private static GameObject SlotPrefab;       //生成网格的预制体
    private static GameObject ItemPrefab;       //生成物品的预制体

    public void Awake(){
        SlotPrefab = slotPrefab;
        ItemPrefab = itemPrefab;
    }
    public void Start() {
        CreateSlot();
        CreateEquipmentSlot();
        CreateItem(1000);
        CreateItem(1000);
        CreateItem(1000);
        CreateItem(1000);
        CreateItem(1000);
        CreateItem(1000);
        CreateItem(1000);
        CreateItem(1000);
        CreateItem(1001);
        CreateItem(1002);
        CreateItem(1003);
        CreateItem(1004);
        CreateItem(1005);
        
    }
    public void Update() {

    }

    //创建背包网格
    private static void CreateSlot(){
        for (int i = 0; i < 80; i++) {
            GameObject slot = GameObject.Instantiate(SlotPrefab, UIManager.Backpage.transform.GetChild(1).GetChild(0));
            slot.name = "Slot" + i;                                 //GameObject名
            slot.AddComponent<Slot>().Index = i;                    //网格指向的ItemArr
            DataManager.SlotArr[i] = slot.GetComponent<Slot>();     //将Slot脚本存入数组

        }
    }

    //创建装备栏网格
    private static void CreateEquipmentSlot() {
        Transform Equips = UIManager.EquipmentPanel.transform.GetChild(2).GetChild(0);
        for (int i = 90; i < 98; i++) {
            GameObject slot = Equips.GetChild(i - 90).gameObject;       //对应装备栏
            slot.AddComponent<Slot>().Index = i;                        //网格的索引
            DataManager.SlotArr[i] = slot.GetComponent<Slot>();         //将Slot脚本存入数组
        }
    }

    //创建物品
    public static void CreateItem(int uid) {
        int index = FetchUtils.FetchEmpty();
        if (index == -1) {
            Debug.Log("物品栏已满，无法生成");
            return;
        }
        CreateItem(uid, index);
    }

    public static void CreateItem(int uid, int Index) {
        GameObject item = GameObject.Instantiate(ItemPrefab, DataManager.SlotArr[Index].transform);     //指定位置生成指定物品
        Equipment equipment = item.AddComponent<Equipment>();           //挂载Equipment脚本
        equipment.itemBase = FetchUtils.FetchEquipmentsBase(uid);       //为其中的物品基础类赋值
        equipment.SlotIndex = Index;                                //网格索引指向该网格
        DataManager.ItemArr[Index] = equipment;                     //将Equipment脚本存入数组

        item.GetComponent<Image>().sprite = equipment.itemBase.Sprite;  //显示贴图
        equipment.ShowCount();                                          //显示数量
    }

    //将物品放在空网格上
    public static void ToEmpty(Item item, Slot slot) {
        DataManager.ItemArr[slot.Index] = item;                 //网格指向的Arr指向的物品更新
        DataManager.ItemArr[item.SlotIndex] = null;             //原存储物品的Arr的指向置为空
        item.SlotIndex = slot.Index;                            //拖拽体指向的Slot更新
    }

    //将物品交换位置
    public static void Exchange(Item item, Slot slot) {
        Item curItem = DataManager.ItemArr[slot.Index];         //网格指向的物体
        Slot preSlot = DataManager.SlotArr[item.SlotIndex];
        DataManager.ItemArr[slot.Index] = item;                 //网格指向的Arr指向的物体更新
        curItem.SlotIndex = item.SlotIndex;                     //当前物体指向的网格更新为物体指向的网格
        item.SlotIndex = slot.Index;                            //物体指向的网格更新
        DataManager.ItemArr[preSlot.Index] = curItem;           //前网格指向的Arr指向的物体更新

        curItem.ReplaceParent();                                //当前物品的父物体更新
    }

    //物品堆叠
    public static void Stack(Item item, Slot slot) {
        Item curItem = DataManager.ItemArr[slot.Index];         //网格指向的物体
        //若堆叠总量小于堆叠上限
        if (item.curStack + curItem.curStack <= item.itemBase.StackMax) {
            curItem.curStack += item.curStack;                  //更新当前物品的数量
            DataManager.ItemArr[item.SlotIndex] = null;         //指向物品的Arr置为空
            Destroy(item.gameObject);
        } else {
            //若堆叠数量大于堆叠上限
            item.curStack = (item.curStack + curItem.curStack) - item.itemBase.StackMax;
            curItem.curStack = item.itemBase.StackMax;
            item.ShowCount();
        }
        curItem.ShowCount();
    }

    //拆分物品
    public static void SplitItem(Item item, Slot slot, int count) {
        CreateItem(item.itemBase.UID, slot.Index);              //指定位置创建新物品
        Item newItem = DataManager.ItemArr[slot.Index];         //新物品
        newItem.curStack = count;               
        item.curStack -= count;
        newItem.ShowCount();
        item.ShowCount();
    }

    public static void Equip(Equipment equipment){
        Slot EquipSlot = DataManager.SlotArr[equipment.equipemtnBase.Position + 90];
        //如果该装备在对应的装备栏上
        Debug.Log(equipment.SlotIndex + "//" + EquipSlot.Index);
        if (equipment.SlotIndex == EquipSlot.Index) {
            //放置在空位
            int index = FetchUtils.FetchEmpty();
            ToEmpty(equipment, DataManager.SlotArr[index]);
            equipment.ReplaceParent();
        } else {
        //该装备不在对应的装备栏上
            //装备栏上存在其他物品
            if (DataManager.SlotArr[EquipSlot.Index] != null) { 
                //交换位置
                IASManager.Exchange(equipment, EquipSlot);
            }
        }
    }
}
