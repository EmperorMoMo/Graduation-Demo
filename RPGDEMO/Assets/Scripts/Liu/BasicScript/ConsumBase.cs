using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消耗基础类：继承于物品基础类基类
/// 不挂载在物体上
/// 实例化对象即为物品实体，在拥有物品的固有属性的基础上拥有消耗品的特有属性
/// </summary>
public class ConsumBase : ItemBase {
    public string ConType { get; set; }     //回复类型
    public int ReValue { get; set; }        //回复数值
    public int Duration { get; set; }       //持续时间

    public ConsumBase() { }

    public ConsumBase(int uid, string name, int quality, int price, int stackMax, string describe, string sprite,
        string conType, int reValue, int duration) : base(uid, name, quality, price, stackMax, describe) {
            ConType = ConType;
            ReValue = reValue;
            Duration = duration;
            Sprite = Resources.Load<Sprite>("Consum/" + sprite);
    }
}
