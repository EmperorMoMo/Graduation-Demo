using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备基础类
/// 不挂载在物体上
/// </summary>
public class EquipmentBase : ItemBase{
    public int Position { get; set; }           //装备部位
    public BaseAttribute Attr { get; set; }     //装备属性
    public int LvLimit { get; set; }            //等级限制
    public Sprite sprite { get; set; }          //装备贴图

    public EquipmentBase() { }

    public EquipmentBase(int uid, string name, int quality, int price, string describe, string spriteName, int stackMax, 
        int position, BaseAttribute attr, int lvlimit) :
        base(uid, name, quality, price,describe, spriteName, stackMax){
            Position = position;
            Attr = attr;
            LvLimit = lvlimit;
            sprite = Resources.Load<Sprite>("Equipment/" + spriteName);
            
    }
}
