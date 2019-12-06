using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品类基类：几乎所有的物品衍生类，技能类都继承于此类
/// </summary>
public class ItemBase {
    public int UID { get; set; }            //物品标识
    public string Name { get; set; }        //物品名称
    public int Quality { get; set; }        //物品品质
    public int Price { get; set; }          //物品价格
    public string Describe { get; set; }    //物品描述
    public Sprite Sprite { get; set; }      //物品贴图
}
