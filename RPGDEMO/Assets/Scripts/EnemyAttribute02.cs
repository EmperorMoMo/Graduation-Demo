using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute02 : MonoBehaviour
{
    public float MAX_HP = 1000;
    public float HP;
    public float Aggressivity = 50;
    public float Armor = 3;
    //private Animation animation;
    public GameObject Effect;
    private Transform hitpoint;
    private SimpleFSM01 sim01;
    // Start is called before the first frame update
    void Awake()
    {
        HP = MAX_HP;
        hitpoint = this.transform.GetChild(2).GetComponent<Transform>();
        //animation = GetComponent<Animation>();
        sim01 = GetComponent<SimpleFSM01>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Enemy_Attacked(float _Aggressivity)
    {
        InstantiateEffect();
        sim01._isAttacked = true;
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

        print(gameObject.name + "还剩：" + HP);
    }

    void InstantiateEffect()
    {
        var instance = Instantiate(Effect, hitpoint.position, hitpoint.rotation);
        Destroy(instance, 1.0f);
    }
}
