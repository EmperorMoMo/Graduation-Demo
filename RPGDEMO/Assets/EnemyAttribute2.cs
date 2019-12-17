using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute2 : MonoBehaviour
{
    public float MAX_HP = 200;
    public float HP;
    public float Aggressivity = 50;
    public float Armor = 3;
    //private Animation animation;
    public GameObject Effect;
    private Transform hitpoint;
    //private SimpleFSM sim;
    private SimpleFSM_far02 sim_far;
    private CharacterAttribute CA;
    //private SimpleFSM01 sim01;
    private bool isme = false;
    // Start is called before the first frame update
    void Awake()
    {
        HP = MAX_HP;
        hitpoint = this.transform.GetChild(2).GetComponent<Transform>();
        CA = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        //sim = GetComponent<SimpleFSM>();
        sim_far = GetComponent<SimpleFSM_far02>();
    }

    void Update()
    {

    }

    public void Enemy_Attacked2(float _Aggressivity)
    {
        if (HP > 0)
        {
            InstantiateEffect();
            sim_far._isAttacked = true;
            //sim._isAttacked = true;
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
        else
        {
            //sim._isAttacked = false;
            sim_far._isAttacked = false;
        }
        //print(gameObject.name+"还剩："+HP);
    }

    void InstantiateEffect()
    {
        var instance = Instantiate(Effect, hitpoint.position, hitpoint.rotation);
        Destroy(instance, 1.0f);
    }
}
