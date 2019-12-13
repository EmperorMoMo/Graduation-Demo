using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备基础类：继承于物品基础类基类
/// 不挂载在物体上
/// 实例化对象即为物品实体，在拥有物品的固有属性的基础上拥有装备的特有属性
/// </summary>
public class EquipmentBase : ItemBase{
    public int Position { get; set; }           //装备部位
    public BaseAttribute Attr { get; set; }     //装备属性
    public int LvLimit { get; set; }            //等级限制

    //默认构造方法
    public EquipmentBase() { }

    //装备构造函数
    public EquipmentBase(int uid, string name, int quality, int price, int stackMax, string describe, string sprite,
        int position, BaseAttribute attr, int lvlimit) : base(uid, name, quality, price, stackMax, describe, sprite) {
            Position = position;
            Attr = attr;
            LvLimit = lvlimit;
    }
}
