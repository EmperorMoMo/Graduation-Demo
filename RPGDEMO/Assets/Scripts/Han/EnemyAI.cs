using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float HP = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
