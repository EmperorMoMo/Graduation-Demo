using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour
{
    public float MAX_HP=200;
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
    public int playerLevel;
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
        if (CA.Level >= 10)
        {
            level = (CA.Level / 10)+1;
            MAX_HP *= level*level*level*3f;
            Aggressivity *= level * level * level*2f;
            Armor *= level * level * level * 3f;
        }
        Debug.Log("怪物等级为：" + level);
        Debug.Log("怪物攻击力：" + Aggressivity);
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
            HP = Mathf.Clamp(HP, 0, MAX_HP);
        }
        else
        {
            sim._isAttacked = false;
        }
        //print(gameObject.name+"还剩："+HP);
    }

    void InstantiateEffect()
    {
        var instance = Instantiate(Effect, hitpoint.position, hitpoint.rotation);
        Destroy(instance, 1.0f);
    }
}
