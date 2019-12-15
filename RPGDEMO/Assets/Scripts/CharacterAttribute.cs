﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute : MonoBehaviour
{
    public enum Attribute
    {
        Strength,
        Agile,
        Intellect
    }
    public Attribute Main_Attribute;

    public BaseAttribute baseAttribute = new BaseAttribute();
    public BaseAttribute equipAttribute = new BaseAttribute();
    public BaseAttribute finalAttribute = new BaseAttribute();

    public float Cur_HP;//生命值
    public float Cur_MP;//魔法值
    public float Exp=0;//经验值
    public int Level=1;//等级
    
    bool _Upgrade;//判断是否升级了

    private ActorController ac;
    // Start is called before the first frame update
    void Awake()
    {
        baseAttribute.HP = 250;
        baseAttribute.MP = 500;
        baseAttribute.ReHP = 1;
        baseAttribute.ReMP = 5;
        baseAttribute.Aggressivity = 22;
        baseAttribute.Armor = 2;
        baseAttribute.Strength = 18;
        baseAttribute.Agile = 10;
        baseAttribute.Intellect = 15;

        ac = GetComponent<ActorController>();
        Cur_HP = baseAttribute.HP + equipAttribute.HP;
        Cur_MP = baseAttribute.MP + equipAttribute.MP;
        print(Cur_HP);
    }

    // Update is called once per frame
    void Update()
    {
        Character_Level();
        //Debug.Log(baseAttribute.HP);
    }
    
    public void Character_Exp(float _Exp)
    {
        Exp += _Exp;
    }

    public void Character_Level()
    {
        if (Exp >= (100 * (Level*2)))
        {
            Exp = 0;
            Level += 1;
            _Upgrade = true;
        }

        if (_Upgrade)
        {
            ChangeBaseAttribute();
            _Upgrade = false;
        }
    }

    public void Strength()
    {
        finalAttribute.ReHP += finalAttribute.Strength / 10;
        finalAttribute.HP += finalAttribute.Strength * 10;
        if (Main_Attribute == Attribute.Strength)
        {
            finalAttribute.Aggressivity += finalAttribute.Strength;
        }
    }

    public void Agile()
    {
        finalAttribute.Armor += finalAttribute.Agile / 10;
        if (Main_Attribute == Attribute.Agile)
        {
            finalAttribute.Aggressivity += finalAttribute.Agile;
        }
    }

    public void Intellect()
    {
        finalAttribute.ReMP += finalAttribute.Intellect * 3;
        finalAttribute.MP += finalAttribute.Intellect * 20;
        if (Main_Attribute == Attribute.Intellect)
        {
            finalAttribute.Aggressivity += finalAttribute.Intellect;
        }
    }

    public void Character_Attacked(float _Aggressivity)
    {
        if (ac.canAttacked == true)
        {
            ac._isAttacked = true;
        }
        if (finalAttribute.Armor == 0)
        {
            Cur_HP -= _Aggressivity;
        }

        if (finalAttribute.Armor < 0)
        {
            Cur_HP -= _Aggressivity * 2;
        }

        if (finalAttribute.Armor > 0)
        {
            Cur_HP -= (_Aggressivity - (_Aggressivity * ((finalAttribute.Armor * 6) / (100 + finalAttribute.Armor * 6))));
        }

        Cur_HP = Mathf.Clamp(Cur_HP, 0, finalAttribute.HP);
        if (Cur_HP <= 0)
        {
            ac.die = true;
            //this.gameObject.GetComponent<CharacterAttribute>().enabled = false;
        }
        print("HoShi还剩："+Cur_HP);
    }

    public void ChangeEquipAttribute(BaseAttribute _equipAttribute)
    {
        equipAttribute = _equipAttribute;
        Debug.Log(equipAttribute.HP);
        ChangeAttribute();
    }

    public void ChangeBaseAttribute()
    {
        baseAttribute.Strength += 25;
        baseAttribute.Agile += 20;
        baseAttribute.Intellect += 20;

        ChangeAttribute();
    }

    public void ChangeAttribute()
    {
        finalAttribute.HP = baseAttribute.HP + equipAttribute.HP;
        finalAttribute.MP = baseAttribute.MP + equipAttribute.MP;
        finalAttribute.ReHP = baseAttribute.ReHP + equipAttribute.ReHP;
        finalAttribute.ReMP = baseAttribute.ReMP + equipAttribute.ReMP;
        finalAttribute.Aggressivity = baseAttribute.Aggressivity + equipAttribute.Aggressivity;
        finalAttribute.Armor = baseAttribute.Armor + equipAttribute.Armor;
        finalAttribute.Strength = baseAttribute.Strength + equipAttribute.Strength;
        finalAttribute.Agile = baseAttribute.Agile + equipAttribute.Agile;
        finalAttribute.Intellect = baseAttribute.Intellect + equipAttribute.Intellect;
        Strength();
        Agile();
        Intellect();
        Debug.Log("final:"+finalAttribute.HP);
        Debug.Log("base:"+baseAttribute.HP);
        Debug.Log("equip:" + equipAttribute.HP);
    }

}
