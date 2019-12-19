using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备基础类：继承于物品基础类基类
/// 不挂载在物体上
/// 实例化对象即为物品实体，在拥有物品的固有属性的基础上拥有装备的特有属性
/// </summary>
public class ScrollBase : ItemBase{
    public int TarUID { get; set; }
    public List<int[]> Mats { get; set; }

    //默认构造方法
    public ScrollBase() { }

    //装备构造函数
    public ScrollBase(int uid, string name, int quality, int price, int stackMax, string describe, string sprite,
        int tarUID, List<int[]> mats) : base(uid, name, quality, price, stackMax, describe) {
            TarUID = tarUID;
            Mats = mats;
            Sprite = Resources.Load<Sprite>("Consum/" + sprite);
    }
}
