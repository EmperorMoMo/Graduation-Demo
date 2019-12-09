using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据类，存储物品，网格信息等，方便索引查找，做持久化操作等
/// </summary>
public class DataManager : MonoBehaviour
{
    public static GameObject[] SlotGOList = new GameObject[80];     //存放所有的Slot游戏对象
    public static GameObject[] ItemGOList = new GameObject[80];     //存放所有的Item游戏对象
}
