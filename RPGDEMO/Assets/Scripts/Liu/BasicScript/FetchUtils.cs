using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fetch工具类，用于获取或寻找想要的对象
/// </summary>
public class FetchUtils {

    //在物品EquipmentArr中寻找空位置
    public static int FetchEmpty() {
        for (int i = 0; i < 80; i++) {
            if (DataManager.ItemArr[i] == null) {
                return i;
            }
        }
        return -1;  //没有空位置返回-1
    }

    //在装备词典中寻找指定UID的装备对象
    public static EquipmentBase FetchEquipmentsBase(int uid) {
        foreach (int key in DataManager.EquipmentDic.Keys) {
            if (key == uid) {
                return DataManager.EquipmentDic[key];
            }
        }
        return null;    //未找到返回null
    }

    //在物品List中寻找所有特定UID的Item游戏对象
    //public List<Item> FetchByUID(int uid){
        //List<Item> items = new List<Item>();
        //for (int i = 0; i < 80; i++) {
        //    if (DataManager.ItemGOList[i].GetComponent<Item>().itemBase.UID == uid) {
        //        items.Add(DataManager.ItemGOList[i].GetComponent<Item>());
        //    }
        //}
        //return items;
    //}
}
