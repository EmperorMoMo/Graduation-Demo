using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour
{
    public float MAX_HP=500;
    public float HP;
    public float Aggressivity=50;
    public float Armor=3;
    //private Animation animation;
    public GameObject Effect;
    private Transform hitpoint;
    private SimpleFSM sim;
    private CharacterAttribute CA;
    //private SimpleFSM01 sim01;
    private bool isme = false;
    public int level=1;
    // Start is called before the first frame update
    void Awake()
    {
        HP = MAX_HP;
        hitpoint= this.transform.GetChild(2).GetComponent<Transform>();
        CA = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        sim = GetComponent<SimpleFSM>();
        //animation = GetComponent<Animation>();
        //if(this.gameObject.name== "mon_orcWarrior")
        //{
        //    sim = GetComponent<SimpleFSM>();
        //    isme = true;
        //}
        //else
        //sim01 = GetComponent<SimpleFSM01>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CA.Level%10==0)
        {
            level++;
            MAX_HP*=level;
            Armor *= level;
            Aggressivity *= level;
        }
    }

    public void Enemy_Attacked(float _Aggressivity)
    {
        if(HP>0)
        {
            InstantiateEffect();
            sim._isAttacked = true;
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
        }
        else
        {
            sim._isAttacked = false;
        }
        print(gameObject.name+"还剩："+HP);
    }

    void InstantiateEffect()
    {
        var instance = Instantiate(Effect, hitpoint.position, hitpoint.rotation);
        Destroy(instance, 1.0f);
    }
}
