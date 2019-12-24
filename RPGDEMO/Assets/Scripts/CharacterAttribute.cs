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

    public BaseAttribute baseAttribute = new BaseAttribute();
    public BaseAttribute equipAttribute = new BaseAttribute();
    public BaseAttribute finalAttribute = new BaseAttribute();

    public float Cur_HP;//生命值
    public float Cur_MP;//魔法值
    public float Exp=0;//经验值
    public int Gold = 0;//金币
    public int Level=1;//等级
    public float drug_ReHP = 0f;
    public float drug_ReMP = 0f;
    
    public bool _Upgrade;//判断是否升级了
    public bool _IsUseHPDrugs;
    public bool _IsUseMPDrugs;
    public GameObject Effect;

    public float time = 0;
    public float HPdrugsTime = 0;
    public float MPdrugsTime = 0;
    public float _timer = 0;

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
        ChangeAttribute();
        Cur_HP = finalAttribute.HP;
        Cur_MP = finalAttribute.MP;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Character_Level();
        if (!ac.die)
        {
            if (Cur_HP < finalAttribute.HP)
            {
                time += Time.fixedDeltaTime;
                if (time > 1)
                {
                    Cur_HP += (finalAttribute.ReHP + drug_ReHP);
                    //Debug.Log(Cur_HP);
                    time = 0;
                }

                Cur_HP = Mathf.Clamp(Cur_HP, 0, finalAttribute.HP);
            }
            else if (Cur_HP > finalAttribute.HP)
            {

                Cur_HP = finalAttribute.HP;
            }

            if (Cur_MP < finalAttribute.MP)
            {
                time += Time.fixedDeltaTime;
                if (time > 1)
                {
                    Cur_MP += (finalAttribute.ReMP + drug_ReMP);
                    Debug.Log(Cur_MP);
                    time = 0;
                }

                Cur_MP = Mathf.Clamp(Cur_MP, 0, finalAttribute.MP);
            }
            else if (Cur_MP > finalAttribute.MP)
            {
                Cur_MP = finalAttribute.MP;
            }
        }
    }

    void Update()
    {
        if (_IsUseHPDrugs)
        {
            HPdrugsTime += Time.deltaTime;
            if (HPdrugsTime>_timer)
            {
                drug_ReHP = 0;
                HPdrugsTime = 0;
                _IsUseHPDrugs = false;
            }
        }

        if (_IsUseMPDrugs)
        {
            MPdrugsTime += Time.deltaTime;
            if (MPdrugsTime > _timer)
            {
                drug_ReMP = 0;
                MPdrugsTime = 0;
                _IsUseMPDrugs = false;
            }
        }
    }
    
    public void Character_Exp(float _Exp)
    {
        Exp += _Exp;
    }

    public void Character_Gold(int _Gold)
    {
        Gold += _Gold;
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
            var instance = Instantiate(Effect, transform.position, transform.rotation) as GameObject;
            instance.transform.parent = transform;
            Destroy(instance,1.35f);
            ChangeBaseAttribute();
            Cur_HP = finalAttribute.HP;
            Cur_MP = finalAttribute.MP;
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
        finalAttribute.ReMP += finalAttribute.Intellect;
        finalAttribute.MP += finalAttribute.Intellect * 15;
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
            StartCoroutine(OnAttacked());
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
    }

    public void ChangeEquipAttribute(BaseAttribute _equipAttribute)
    {
        equipAttribute = _equipAttribute;
        ChangeAttribute();
    }

    public void ChangeBaseAttribute()
    {
        baseAttribute.Strength += 20;
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
    }

    public void UseDrug(string _drugs, int _num, int _time)
    {
        _timer = _time;
        if (string.Equals(_drugs , "HP"))
        {
            if (_time == 0)
            {
                if (Cur_HP < finalAttribute.HP)
                {
                    Cur_HP += _num;
                }
            }

            else
            {
                _IsUseHPDrugs = true;
                drug_ReHP += _num;
            }
        }

        if (string.Equals(_drugs, "MP"))
        {
            if (_time == 0)
            {
                if (Cur_MP < finalAttribute.MP)
                {
                    Cur_MP += _num;
                }
            }

            else
            {
                _IsUseMPDrugs = true;
                drug_ReMP += _num;
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name == "projectile06(Clone)")
        {
            EnemyAttribute1 ai1 = GameObject.Find("mon_trolCurer(Clone)").GetComponent<EnemyAttribute1>();
            Character_Attacked(ai1.Aggressivity);
        }

        if (other.name == "projectile05(Clone)")
        {
            EnemyAttribute2 ai2 = GameObject.Find("mon_goblinWizard(Clone)").GetComponent<EnemyAttribute2>();
            Character_Attacked(ai2.Aggressivity);
        }

        if (other.name == "FireFieldALT(Clone)")
        {
            BossAttribute2 ba2 = GameObject.Find("MOUNTAIN_DRAGON_LEGACY").GetComponent<BossAttribute2>();
            Character_Attacked(ba2.Aggressivity*10);
        }

        if (other.name == "projectile04(Clone)")
        {
            BossAttribute2 ba2 = GameObject.Find("MOUNTAIN_DRAGON_LEGACY").GetComponent<BossAttribute2>();
            Character_Attacked(ba2.Aggressivity*2000);
        }

        if (other.name == "FlamethrowerALT(Clone)")
        {
            BossAttribute2 ba2 = GameObject.Find("MOUNTAIN_DRAGON_LEGACY").GetComponent<BossAttribute2>();
            Character_Attacked(ba2.Aggressivity*10);
        }
    }

    IEnumerator OnAttacked()
    {
        ac.canAttacked = false;
        yield return new WaitForSeconds(1.8f);
        //ac.canAttacked = true;
    }

}
