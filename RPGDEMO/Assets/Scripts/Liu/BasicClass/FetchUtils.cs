using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fetch工具类，用于获取寻找
/// </summary>
public class FetchUtils {

    //在物品List中寻找空位置
    public int FetchEmpty() {
        for (int i = 0; i < 80; i++) {
            if (DataManager.ItemGOList[i] == null) {
                return i;
            }
        }
        return -1;
    }

    //在物品List中寻找所有特定UID的Item游戏对象
    public List<Item> FetchByUID(int uid){
        List<Item> items = new List<Item>();
        for (int i = 0; i < 80; i++) {
            if (DataManager.ItemGOList[i].GetComponent<Item>().itemBase.UID == uid) {
                items.Add(DataManager.ItemGOList[i].GetComponent<Item>());
            }
        }
        return items;
    }
}
