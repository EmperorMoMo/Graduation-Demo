using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品基础类：几乎所有的物品衍生类都继承于此类
/// 不挂载在物体上
/// </summary>
public class ItemBase {
    public int UID { get; set; }            //物品标识
    public string Name { get; set; }        //物品名称
    public int Quality { get; set; }        //物品品质
    public int Price { get; set; }          //物品价格
    public string Describe { get; set; }    //物品描述
    public string SpriteName { get; set; }  //贴图名字
    public int StackMax { get; set; }       //堆叠上限 

    //默认构造方法
    public ItemBase() { }

    //物品构造函数
    public ItemBase(int uid, string name, int quality, int price, string describe, string spriteName, int stackMax) {
        UID = uid;
        Name = name;
        Quality = quality;
        Price = price;
        Describe = describe;
        SpriteName = spriteName;
        StackMax = stackMax;
    }

    //构造函数，堆叠上限测试
    public ItemBase(int uid, int stackMax) {
        UID = uid;
        StackMax = stackMax;
    }
}
