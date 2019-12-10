using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float HP = 100f;

    private Transform playerTrans;

    // Start is called before the first frame update
    void Awake()
    {
        playerTrans = GameObject.Find("Follow").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation=Quaternion.identity;
        rotation = Quaternion.LookRotation(playerTrans.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*5f);
    }

    public void Damage()
    {
        HP = HP - 5f;
        if (HP < 0)
        {
            Debug.LogError("HP values is Error!");
        }
        else
        {
            print(HP);
        }
    }
}
