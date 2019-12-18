using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttribute2 : MonoBehaviour
{
    public float MAX_HP = 5000;
    public float HP;
    public float Aggressivity = 1500;
    public float Armor = 50;
    //private Animation animation;
    public GameObject Effect;
    private Transform hitpoint;
    private MOUNTAINFSM Msim;
    private CharacterAttribute CA;
    // Start is called before the first frame update
    void Awake()
    {
        HP = MAX_HP;
        hitpoint = this.transform.GetChild(3).GetComponent<Transform>();
        CA = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        Msim = GetComponent<MOUNTAINFSM>();
    }
    //public void monster_Level()
    //{
    //    if (CA.Level % 10 == 0)
    //    {
    //        level++;
    //        MAX_HP *= level;
    //        Armor *= level;
    //        Aggressivity *= level;
    //    }
    //}
    // Update is called once per frame
    void Update()
    {

    }

    public void Boss_Attacked(float _Aggressivity)
    {
        if (HP > 0)
        {
            InstantiateEffect();
            //if (isme)
            //    sim._isAttacked = true;
            //else
            //    sim01._isAttacked = true;
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
        }
        //print(gameObject.name+"还剩："+HP);
    }

    void InstantiateEffect()
    {
        var instance = Instantiate(Effect, hitpoint.position, hitpoint.rotation);
        Destroy(instance, 1.0f);
    }
}
