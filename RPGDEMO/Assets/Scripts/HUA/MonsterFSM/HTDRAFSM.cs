using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTDRAFSM : MonsterFSM
{
    //枚举需要实现的状态
    public enum FSMState
    {
        idle,//巡逻状态
        chase,//追逐状态
        walk,
        attack,//攻击状态
        dead//死亡状态
    }
    public float walkDistance = 6;
    //追逐距离
    public float chaseDistance = 20;
    //攻击距离
    public float attackDistance = 3;
    //抵达距离
    public float arriveDistance = 2;
    //保存AI对象当前状态
    public FSMState curState;
    //AI对象移动速度
    public float walkSpeed = 100;
    public float runSpeed = 200;
    //AI对象转向速度
    public float turnSpeed = 10;
    //AI对象动画播放组件
    private Animation _animation;
    //AI对象角色控制器组件
    private CharacterController _controller;
    //角色当前所在路径点
    private int _currentPoint = 0;
    //角色是否还存活
    private bool _isDead;
    //实现基类初始状态机方法
    void GetMessage(string s)
    {
        _animation.Play(s);
    }
    protected override void Initialize()
    {
        monsterHP = 100;
        //先将AI对象的当前状态设置为巡逻状态
        curState = FSMState.idle;
        //没有死亡
        _isDead = false;
        //上一次攻击时间
        elapsedTime = 0;
        //攻击间隔时间
        attackRate = 5.3f;
        //获取AI对象控制器组件
        _controller = GetComponent<CharacterController>();
        //获取AI对象动画播放器组件
        _animation = GetComponent<Animation>();
        //获取AI对象巡逻点
        pointList = GameObject.FindGameObjectsWithTag("Player");
        //获取玩家对象标签和位置
        GameObject obj = GameObject.FindGameObjectWithTag("TestPlayer");
        playerTransform = obj.transform;
        //获取初始路径点
        //FindNextPoint();
        i = 1;
    }
    ////获取下一个路径点的方法
    //private void FindNextPoint()
    //{
    //    //随机获取路径点
    //    int i = Random.Range(0, pointList.Length);
    //    //将取到的路径点赋值给目标位置
    //    targetPoint = pointList[i].transform.position;
    //}
    //巡逻状态实现方法
    private void IdleState()
    {
        ////如果AI对象当前位置与目标点的位置小于等于抵达距离时，转向下一个巡逻点
        //if (Vector3.Distance(transform.position, targetPoint) <= arriveDistance)
        //    FindNextPoint();
        ////判断AI对象与玩家之间的距离是否满足追逐状态，如果AI对象现在的距离与玩家的距离小于等于追逐距离时，将当前状态转换为追逐状态
        //else if (Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance)
        //    curState = FSMState.chase;
        ////根据目标位置控制AI对象转向，根据目标位置与AI对象当前位置差值获取转向角度，再实现转向
        //Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        ////用每一帧的时间乘以转向速度来控制转向
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        ////控制AI对象向前移动
        //_controller.SimpleMove(transform.forward * Time.deltaTime * walkSpeed);
        ////播放AI对象移动动画
        //_animation.Play("Run");
        _animation.Play("Idle");
        if (Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance)
            curState = FSMState.chase;
    }
    //追逐状态方法
    private void ChaseState()
    {
        //切换至追逐状态时，将目标位置置换成玩家位置
        targetPoint = playerTransform.position;
        //计算AI对象与玩家的距离
        float distance = Vector3.Distance(transform.position, targetPoint);
        //如果AI对象与玩家距离小于攻击距离时，将当前状态切换成攻击状态
        if (distance <= walkDistance)
            curState = FSMState.walk;
        //if (distance <= attackDistance)
        //    curState = FSMState.attack;
        ////如果当前距离已经大于巡逻状态，将当前状态切换成巡逻状态
        else if (distance > chaseDistance)
            curState = FSMState.idle;
        //根据目标位置控制AI对象转向，根据目标位置与AI对象当前位置差值获取转向角度，再实现转向
        Vector3 a = new Vector3(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(a);
        //Quaternion rotation = Quaternion.LookRotation(targetPoint - transform.position);
        //用每一帧的时间乘以转向速度来控制转向
        //Transform tran = transform;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        //if ((transform.rotation.y - tran.rotation.y) > 1)
        //    _animation.Play("turn45Right3toxicSpitCombo");
        //控制AI对象朝目标方向移动
        //if (distance <= walkDistance)
        //{
        //    _controller.SimpleMove(transform.forward * Time.deltaTime * walkSpeed);
        //    _animation.Play("Walk");
        //}
        //播放移动动画
        _controller.SimpleMove(transform.forward * Time.deltaTime * runSpeed);
        _animation.Play("Run");
    }
    private void WalkState()
    {
        targetPoint = playerTransform.position;
        float distance = Vector3.Distance(transform.position, targetPoint);
        if (distance <= attackDistance)
            curState = FSMState.attack;
        //如果当前距离已经大于巡逻状态，将当前状态切换成巡逻状态
        else if (distance > walkDistance)
            curState = FSMState.chase;
        //根据目标位置控制AI对象转向，根据目标位置与AI对象当前位置差值获取转向角度，再实现转向
        Vector3 a = new Vector3(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(a);
        //Quaternion rotation = Quaternion.LookRotation(targetPoint - transform.position);
        //用每一帧的时间乘以转向速度来控制转向
        //Transform tran = transform;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        _controller.SimpleMove(transform.forward * Time.deltaTime * walkSpeed);
        _animation.Play("Walk");
    }
    //攻击状态
    private void AttackState()
    {
        //将目标位置复制成玩家位置
        targetPoint = playerTransform.position;
        //计算两者之间的距离
        float distance = Vector3.Distance(transform.position, targetPoint);
        //判断是否满足攻击需求
        //如果距离已经大于攻击距离且小于等于追逐距离
        if (distance > attackDistance && distance <= walkDistance)
        {
            //将当前状态转换成追逐状态
            curState = FSMState.walk;
            //退出计算
            return;
        }
        //如果距离已经大于追逐距离，将AI对象状态切换成巡逻状态
        else if (distance > walkDistance)
        {
            //将当前状态切换成巡逻状态
            curState = FSMState.chase;
            //退出计算
            return;
        }
        //计算转向，攻击状态需要AI对象正面朝向玩家
        //根据目标位置控制AI对象转向，根据目标位置与AI对象当前位置差值获取转向角度，再实现转向
        Vector3 a = new Vector3(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(a);
        //Quaternion rotation = Quaternion.LookRotation(targetPoint - transform.position);
        //用每一帧的时间乘以转向速度来控制转向
        //Transform tran=transform;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        //if ((transform.rotation.y - tran.rotation.y)> 1)
        //    _animation.Play("turn45Right3toxicSpitCombo");
        //如果上一次攻击时间大于等于间隔时间，则认为可以攻击
        if (elapsedTime >= attackRate)
        {
            if (i == 1)
            {
                _animation.Play("Attack01");
                if (elapsedTime > 6.4)
                {
                    elapsedTime = 0;
                    i = Random.Range(1, 4);
                }
            }
            if (i == 2)
            {
                _animation.Play("Attack02");
                if (elapsedTime > 7.9)
                {
                    elapsedTime = 0;
                    i = Random.Range(1, 4);
                }
            }
            if (i == 3)
            {
                _animation.Play("Attack03");
                if (elapsedTime > 6.6)
                {
                    elapsedTime = 0;
                    i = Random.Range(1, 4);
                }
            }
            //将上一次攻击时间置零

        }
        //否则播放站立动画
        else
        {
            _animation.Play("Idle");
        }
    }
    //死亡状态方法实现
    private void DeadState()
    {
        if(monsterHP<=0)
        _animation.Play("Dead");
    }
    //重写父类方法
    //用于根据当前状态调用相应事件以及实现方法
    protected override void FSMUpdate()
    {
        //将当前状态传入
        switch (curState)
        {
            //判断相应的状态,调用相应方法
            //巡逻状态
            case FSMState.idle:
                IdleState();
                break;
            //追逐状态
            case FSMState.chase:
                ChaseState();
                break;
            //攻击状态
            case FSMState.walk:
                WalkState();
                break;
            case FSMState.attack:
                AttackState();
                break;
            //死亡状态
            case FSMState.dead:
                DeadState();
                break;
        }
        //计算攻击状态中上一次攻击时间
        elapsedTime += Time.deltaTime;
    }
}
