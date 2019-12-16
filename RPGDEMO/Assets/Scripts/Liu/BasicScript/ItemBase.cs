using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品基础类基类：所有的物品基础类子类都继承于此类
/// 不挂载在物体上
/// 实例化对象即为物品实体，拥有物品的固有属性
/// </summary>
public class ItemBase {
    public int UID { get; set; }            //物品标识
    public string Name { get; set; }        //物品名称
    public int Quality { get; set; }        //物品品质
    public int Price { get; set; }          //物品价格
    public int StackMax { get; set; }       //堆叠上限
    public string Describe { get; set; }    //物品描述
    public Sprite Sprite { get; set; }      //贴图贴图

    //默认构造方法
    public ItemBase() { }

    //物品构造函数
    public ItemBase(int uid, string name, int quality, int price, int stackMax, string describe) {
        UID = uid;
        Name = name;
        Quality = quality;
        Price = price;
        StackMax = stackMax;
        Describe = describe;
    }
}
