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

    public BaseAttribute EquipAttribute;
    public Attribute Main_Attribute;


    public float MAX_HP=250;//最大生命值
    public float HP;//生命值
    public float MAX_MP=500;//最大魔法值
    public float MP;//魔法值
    public float ReplyHP=1;//生命回复
    public float ReplyMP=5;//魔法回复
    public float Aggressivity=22;//攻击力
    public float Armor=2;//护甲值
    public float Exp=0;//经验值
    public int Level=1;//等级
    public float Strength=18;//力量
    public float Agile=10;//敏捷
    public float Intellect=15;//智力
    
    bool _Upgrade;//判断是否升级了

    private ActorController ac;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<ActorController>();
        HP = MAX_HP;
        MP = MAX_MP;
        print(HP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Character_HP(float _HP)
    {
        Debug.Log("text3:" + _HP);
        MAX_HP += _HP;
        //return MAX_HP;
    }

    public void Character_MP(float _MP)
    {
        MAX_MP += _MP;
        //return MAX_MP;
    }

    public void Character_ReplyHP()
    {

    }

    public void Character_ReplyMP()
    {

    }

    public void Character_Aggressivity(float _Aggressivity)
    {
        Aggressivity += _Aggressivity;
        //return Aggressivity;
    }

    public void Character_Armor()
    {

    }

    public void Character_Exp(float _Exp)
    {
        Exp += _Exp;
        //return Exp;
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
            Character_Strength(25);
            Character_Agile(20);
            Character_Intellect(20);
            _Upgrade = false;
        }
    }

    public void Character_Strength(float _Strength)
    {
        Strength += _Strength;
        ReplyHP += Strength / 10;
        MAX_HP += Strength * 10;
        if (Main_Attribute==Attribute.Strength)
        {
            Aggressivity += Strength;
        }
        //return Strength;
    }

    public void Character_Agile(float _Agile)
    {
        Agile += _Agile;
        Armor += Agile / 10;
        if (Main_Attribute == Attribute.Agile)
        {
            Aggressivity += Agile;
        }
        //return Agile;
    }

    public void Character_Intellect(float _Intellect)
    {
        Intellect += _Intellect;
        ReplyMP += Intellect * 4;
        MAX_MP += Intellect * 20;
        //return Intellect;
    }

    public void Character_Attacked(float _Aggressivity)
    {
        if (ac.canAttacked == true)
        {
            ac._isAttacked = true;
        }
        if (Armor == 0)
        {
            HP -= _Aggressivity;
        }

        if (Armor < 0)
        {
            HP -= _Aggressivity * 2;
        }

        if (Armor > 0)
        {
            HP -= (_Aggressivity - (_Aggressivity * ((Armor * 6) / (100 + Armor * 6))));
        }

        HP = Mathf.Clamp(HP, 0, MAX_HP);
        if (HP <= 0)
        {
            ac.die = true;
            this.gameObject.GetComponent<CharacterAttribute>().enabled = false;
        }
        print("HoShi还剩："+HP);
    }

    public void ChangeAttribute(BaseAttribute equipAttribute)
    {
        Debug.Log("text2");
        Character_HP(equipAttribute.HP);
        Character_MP(equipAttribute.MP);
        Character_Aggressivity(equipAttribute.Aggressivity);
        Character_Strength(equipAttribute.Strength);
        Character_Agile(equipAttribute.Agile);
        Character_Intellect(equipAttribute.Intellect);
        Debug.Log("Max_HP" + MAX_HP);
    }
}
