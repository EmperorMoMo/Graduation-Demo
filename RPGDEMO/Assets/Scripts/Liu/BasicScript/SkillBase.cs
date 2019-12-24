using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能基础类
/// </summary>
public class SkillBase {
    public int UID { get; set; }            //物品标识
    public string Name { get; set; }        //物品名称
    public bool IsActive { get; set; }
    public int LvLimit { get; set; }
    public int ConValue { get; set; }
    public int CDTime { get; set; }
    public string Describe { get; set; }
    public Sprite Sprite { get; set; }      //贴图贴图

    //默认构造方法
    public SkillBase() { }

    //物品构造函数
    public SkillBase(int uid, string name, bool isActive, int lvlimit, int convalue, int cdTime, string describe, string sprite) {
        UID = uid;
        Name = name;
        IsActive = isActive;
        LvLimit = lvlimit;
        ConValue = convalue;
        CDTime = cdTime;
        Describe = describe;
        Sprite = Resources.Load<Sprite>("Skill/" + sprite);
    }
}
