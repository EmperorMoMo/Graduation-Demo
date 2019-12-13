using System.Collections;
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

    public float HP=250;//生命值
    public float MP=500;//魔法值
    public float ReplyHP=1;//生命回复
    public float ReplyMP=5;//魔法回复
    public float Aggressivity=25;//攻击力
    public float Armor=2;//护甲值
    public float Exp=0;//经验值
    public int Level=1;//等级
    public float Strength=18;//力量
    public float Agile=10;//敏捷
    public float Intellect=15;//智力
    
    bool _Upgrade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Character_HP(float _HP)
    {
        HP += _HP;
        return HP;
    }

    public float Character_MP(float _MP)
    {
        MP += _MP;
        return MP;
    }

    public void Character_ReplyHP()
    {

    }

    public void Character_ReplyMP()
    {

    }

    public float Character_Aggressivity(float _Aggressivity)
    {
        Aggressivity += _Aggressivity;
        return Aggressivity;
    }

    public void Character_Armor()
    {

    }

    public float Character_Exp(float _Exp)
    {
        Exp += _Exp;
        return Exp;
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
            Character_Strength(Level * 12);
            Character_Agile(Level * 10);
            Character_Intellect(Level * 12);
        }
    }

    public float Character_Strength(float _Strength)
    {
        Strength += _Strength;
        ReplyHP += Strength / 10;
        HP += Strength * 10;
        if (Main_Attribute==Attribute.Strength)
        {
            Aggressivity += Strength;
        }
        return Strength;
    }

    public float Character_Agile(float _Agile)
    {
        Agile += _Agile;
        Armor += Agile / 10;
        if (Main_Attribute == Attribute.Agile)
        {
            Aggressivity += Agile;
        }
        return Agile;
    }

    public float Character_Intellect(float _Intellect)
    {
        Intellect += _Intellect;
        ReplyMP += Intellect * 4;
        MP += Intellect * 20;
        return Intellect;
    }
}
