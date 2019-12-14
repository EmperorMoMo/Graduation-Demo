using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttribute {
    public float HP { get; set; }               //生命值
    public float MP { get; set; }               //魔法值
    public float ReHP { get; set; }             //生命回复
    public float ReMP { get; set; }             //魔法回复
    public float Aggressivity { get; set; }     //攻击力
    public float Armor { get; set; }            //护甲值
    public float Strength { get; set; }         //力量
    public float Intellect { get; set; }        //智力
    public float Agile { get; set; }            //敏捷

    public BaseAttribute() { }

    public BaseAttribute(float hp, float mp, float rehp, float remp, float aggr, float armo, float stre, float inte, float agil) {
        HP = hp;
        MP = mp;
        ReHP = rehp;
        ReMP = remp;
        Aggressivity = aggr;
        Armor = armo;
        Strength = stre;
        Intellect = inte;
        Agile = agil;
    }
}

