using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFSM_far01 : FSM
{
    //枚举需要实现的状态
    public enum FSMState
    {
        patrol,//巡逻状态
        chase,//追逐状态
        attack,//攻击状态
        dead//死亡状态
    }
    public bool _isAttacked;
    public GameObject Effect;
    private Transform EffectPoint1;
    public SimpleFSM simpleFSM;
    public Vector3 velocity;
    //追逐距离
    public float chaseDistance = 10;
    //攻击距离
    public float attackDistance = 1;
    //抵达距离
    public float arriveDistance = 2;
    //保存AI对象当前状态
    public FSMState curState;
    //AI对象移动速度
    public float walkSpeed = 80;
    //AI对象转向速度
    public float turnSpeed = 10;
    //AI对象动画播放组件
    private Animation _animation;
    //AI对象角色控制器组件
    //private CharacterController _controller;
    //角色当前所在路径点
    private Rigidbody rigidbody;
    private int _currentPoint = 0;
    private EnemyAttribute1 ea1;
    private CharacterAttribute ca;
    public float damageTime = 0;
    //角色是否还存活
    private bool _isDead;
    //实现基类初始状态机方法
    protected override void Initialize()
    {
        EffectPoint1 = this.transform.GetChild(0).GetComponent<Transform>();
        ea1 = GetComponent<EnemyAttribute1>();
        ca = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        //先将AI对象的当前状态设置为巡逻状态
        curState = FSMState.patrol;
        //没有死亡
        _isDead = false;
        //上一次攻击时间
        elapsedTime = 0;
        //攻击间隔时间
        attackRate = 3f;
        //获取AI对象控制器组件
        //_controller = GetComponent<CharacterController>();
        //获取AI对象动画播放器组件
        _animation = GetComponent<Animation>();
        rigidbody = GetComponent<Rigidbody>();
        //获取AI对象巡逻点
        pointList = GameObject.FindGameObjectsWithTag("Patrol");
        //获取玩家对象标签和位置
        GameObject obj = GameObject.FindGameObjectWithTag("TestPlayer");
        playerTransform = obj.transform;
        //获取初始路径点
        FindNextPoint();
    }
    //获取下一个路径点的方法
    private void FindNextPoint()
    {
        //随机获取路径点
        int i = Random.Range(0, pointList.Length);
        //将取到的路径点赋值给目标位置
        targetPoint = pointList[i].transform.position;
    }
    //巡逻状态实现方法
    private void PatrolState()
    {
        Vector3 trans = new Vector3(transform.position.x, 0, transform.position.z);
        if (!_isAttacked)
        {
            if (ca.Cur_HP > 0)
            {
                //如果AI对象当前位置与目标点的位置小于等于抵达距离时，转向下一个巡逻点
                if (Vector3.Distance(transform.position, targetPoint) <= arriveDistance)
                    FindNextPoint();
                //判断AI对象与玩家之间的距离是否满足追逐状态，如果AI对象现在的距离与玩家的距离小于等于追逐距离时，将当前状态转换为追逐状态
                else if (Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance)
                    curState = FSMState.chase;
                Vector3 a = new Vector3(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
                Quaternion rotation = Quaternion.LookRotation(a);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
                //_controller.SimpleMove(transform.forward * Time.deltaTime * walkSpeed);
                rigidbody.velocity = transform.forward * Time.deltaTime * walkSpeed;
                //播放AI对象移动动画
                _animation.Play("Run");
            }
            else
                _animation.Play("Idle");
            if (ea1.HP <= 0)
                curState = FSMState.dead;
        }
        else
        {
            damageTime += Time.deltaTime;
            _animation.Play("Damage");
            if (damageTime >= 0.35f)
            {
                _isAttacked = false;
                damageTime = 0;
                transform.position = trans;
            }
        }
    }
    //追逐状态方法
    private void ChaseState()
    {
        //切换至追逐状态时，将目标位置置换成玩家位置
        targetPoint = playerTransform.position;
        //计算AI对象与玩家的距离
        Vector3 trans = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(transform.position, targetPoint);
        if (!_isAttacked)
        {
            if (ca.Cur_HP > 0)
            {
                //如果AI对象与玩家距离小于攻击距离时，将当前状态切换成攻击状态
                if (distance <= attackDistance)
                    curState = FSMState.attack;
                //如果当前距离已经大于巡逻状态，将当前状态切换成巡逻状态
                else if (distance >= chaseDistance)
                    curState = FSMState.patrol;
                Vector3 a = new Vector3(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
                Quaternion rotation = Quaternion.LookRotation(a);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
                //_controller.SimpleMove(transform.forward * Time.deltaTime * walkSpeed);
                rigidbody.velocity = transform.forward * Time.deltaTime * walkSpeed;
                //播放移动动画
                _animation.Play("Run");
            }
            else
                _animation.Play("Idle");
            if (ea1.HP <= 0)
                curState = FSMState.dead;
        }
        else
        {
            damageTime += Time.deltaTime;
            _animation.Play("Damage");
            if (damageTime >= 0.35f)
            {
                _isAttacked = false;
                damageTime = 0;
                transform.position = trans;
            }
        }
    }
    //攻击状态
    private void AttackState()
    {
        //将目标位置复制成玩家位置
        targetPoint = playerTransform.position;
        //计算两者之间的距离
        float distance = Vector3.Distance(transform.position, targetPoint);
        Vector3 trans = new Vector3(transform.position.x, 0, transform.position.z);
        if (!_isAttacked)
        {
            if (ca.Cur_HP > 0)
            {
                //判断是否满足攻击需求
                //如果距离已经大于攻击距离且小于等于追逐距离
                if (distance > attackDistance && distance <= chaseDistance)
                {
                    //将当前状态转换成追逐状态
                    curState = FSMState.chase;
                    //退出计算
                    return;
                }
                //如果距离已经大于追逐距离，将AI对象状态切换成巡逻状态
                else if (distance > chaseDistance)
                {
                    //将当前状态切换成巡逻状态
                    curState = FSMState.patrol;
                    //退出计算
                    return;
                }
                //计算转向，攻击状态需要AI对象正面朝向玩家
                //根据目标位置控制AI对象转向，根据目标位置与AI对象当前位置差值获取转向角度，再实现转向
                Vector3 a = new Vector3(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
                Quaternion rotation = Quaternion.LookRotation(a);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
                //如果上一次攻击时间大于等于间隔时间，则认为可以攻击
                if (elapsedTime >= attackRate)
                {
                    _animation.Play("Attack02");
                    //将上一次攻击时间置零
                    if (elapsedTime > 3.95f)
                        elapsedTime = 0;
                }
                //否则播放站立动画
                else
                {
                    _animation.Play("Idle");
                }
            }
            else
                _animation.Play("Idle");
            if (ea1.HP <= 0)
                curState = FSMState.dead;
        }
        else
        {
            damageTime += Time.deltaTime;
            _animation.Play("Damage");
            if (damageTime >= 0.35f)
            {
                _isAttacked = false;
                damageTime = 0;
                transform.position = trans;
            }
        }
    }
    //死亡状态方法实现
    IEnumerator die()
    {
        _animation.Play("Dead");
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
    private void DeadState()
    {
        StartCoroutine(die());
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
            case FSMState.patrol:
                PatrolState();
                break;
            //追逐状态
            case FSMState.chase:
                ChaseState();
                break;
            //攻击状态
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
    public void InstantiateEffect1()
    {
        var instance = Instantiate(Effect, EffectPoint1.position, EffectPoint1.rotation);
            Destroy(instance, 1f);
    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log(other.name);
    //    if (other.tag == "Player")
    //    {
    //        //other.GetComponent<>
    //        Debug.Log("======");
    //        des = true;
    //    }
    //}
}
