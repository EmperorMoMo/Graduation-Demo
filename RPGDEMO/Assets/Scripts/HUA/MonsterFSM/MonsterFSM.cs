using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    protected int i;
    protected float monsterHP;
    //获取玩家组件
    protected Transform playerTransform;
    //目标位置
    protected Vector3 targetPoint;
    //路径巡逻数组
    //protected GameObject[] pointList;
    //AI对象攻击速度
    protected float attackRate;
    //记录AI对象上次攻击时间
    protected float elapsedTime;
    //初始化状态机
    protected virtual void Initialize()
    {

    }
    //每一帧更新状态机
    protected virtual void FSMUpdate()
    {

    }
    //固定帧率更新状态机
    protected virtual void FSMFixedUpdate()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //调用初始化状态机
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //调用每一帧更新状态机
        FSMUpdate();
    }
    //执行固定帧率调用状态机
    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
