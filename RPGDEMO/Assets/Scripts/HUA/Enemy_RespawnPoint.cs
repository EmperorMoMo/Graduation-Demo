using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RespawnPoint : MonoBehaviour
{
    public float spawnRange = 40;//怪物出生点范围
    public GameObject enemy;//要初始化的怪物
    private Transform target;//目标
    private GameObject currentEnemy;//当前的怪物
    private bool isOutsideRange = true;//是否在怪物出生点的外面
    private Vector3 distanceToPlayer;//怪物与角色的距离
    public int Enemy = 15;
    private bool first_IN = true;
    public List<GameObject> gameObjects=new List<GameObject>();
    //private CharacterAttribute ca;
    //private EnemyAttribute ea;
    //public int mons_level=1;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("TestPlayer").transform;
        //ca = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        //mons_level += ca.Level / 10;
        //ea = GameObject.Find("mon_orgeHitter(Clone)").GetComponent<EnemyAttribute>();
    }

    // Update is called once per frame
    void Update()
    {
            distanceToPlayer = transform.position - target.position;
            if (distanceToPlayer.magnitude < spawnRange && first_IN)//说明主角已经进入到怪物出生点范围
            {
                for (int i = 0; i < Enemy; i++)
                {
                    currentEnemy = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
                //currentEnemy.GetComponent<EnemyAttribute>().playerLevel = ca.Level;
                //if (ca.Level>=10)
                //{
                //    currentEnemy.GetComponent<EnemyAttribute>().MAX_HP =10*(ca.Level / 10);
                //    currentEnemy.GetComponent<EnemyAttribute>().Aggressivity =50* (ca.Level / 10);
                //    currentEnemy.GetComponent<EnemyAttribute>().Armor = 3* (ca.Level / 10);
                //}
                    gameObjects.Add(currentEnemy);
                    isOutsideRange = false;
                }
                first_IN = false;
                // if(!currentEnemy)
                //currentEnemy = Instantiate(enemy,transform.position, transform.rotation) as GameObject;
                // isOutsideRange = false;
            }
            isOutsideRange = true;
        if (gameObjects.Count<=0)
        {
            first_IN = true;
        }
    }
}
