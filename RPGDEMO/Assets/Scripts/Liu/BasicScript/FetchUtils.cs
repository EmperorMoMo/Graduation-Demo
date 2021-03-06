﻿using System.Collections;
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

    //在装备词典中寻找指定UID的装备对象
    public static ConsumBase FetchConsumsBase(int uid) {
        foreach (int key in DataManager.ConsumDic.Keys) {
            if (key == uid) {
                return DataManager.ConsumDic[key];
            }
        }
        return null;    //未找到返回null
    }

    public static ItemBase FetchMaterial(int uid) {
        foreach (int key in DataManager.MaterialDic.Keys) {
            if (key == uid) {
                return DataManager.MaterialDic[key];
            }
        }
        return null;    //未找到返回null
    }

    public static ScrollBase FetchScrollBase(int uid) {
        foreach (int key in DataManager.ScrollDic.Keys) {
            if (key == uid) {
                return DataManager.ScrollDic[key];
            }
        }
        return null;    //未找到返回null
    }

    public static Consum FetchLastConsum(int uid, out int Count) {
        Count = 0;
        Consum consum = null;
        foreach (Item i in DataManager.ItemArr) {
            if (i != null) {
                if ((i as Consum) != null) {
                    if (i.itemBase.UID == uid) {
                        Count += i.curStack;
                        consum = (Consum)i;
                        Debug.Log(consum.consumBase.ConType);
                    }
                }
            }
        }
        return consum;
    }

    public static int FetchMatCount(int uid) {
        int count = 0;
        foreach (Item i in DataManager.ItemArr) {
            if (i != null) {
                if ((i as Materia) != null) {
                    if (i.itemBase.UID == uid) {
                        count += i.curStack;
                    }
                }
            }
        }
        return count;
    }

    public static Materia FetchLastMaterial(int uid) {
        Materia materia = null;
        foreach (Item i in DataManager.ItemArr) {
            if (i != null) {
                if ((i as Materia) != null) {
                    if (i.itemBase.UID == uid) {
                        materia = (Materia)i;
                    }
                }
            }
        }
        return materia;
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
