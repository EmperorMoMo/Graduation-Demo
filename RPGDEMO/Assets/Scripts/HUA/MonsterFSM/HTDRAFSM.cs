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
    public float Time_damage = 0;
    public float SkillDistance = 5;
    public float SkillJiaodu = 60;
    public float walkDistance = 6;
    GameObject go;
    MeshFilter mf;
    MeshRenderer mr;
    Shader shader;
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
    //是否第一次进入攻击
    private bool flag = false;
    private CharacterAttribute ca;
    private BossAttribute ba;
    //实现基类初始状态机方法
    protected override void Initialize()
    {
        ca = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        ba = GetComponent<BossAttribute>();
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
        //pointList = GameObject.FindGameObjectsWithTag("Patrol");
        //获取玩家对象标签和位置
        GameObject obj = GameObject.FindGameObjectWithTag("TestPlayer");
        playerTransform = obj.transform;
        //获取初始路径点
        //FindNextPoint();
        i = 1;
    }
    private void Damage(int s)
    {
        ca.Character_Attacked(ba.Aggressivity*s);
    }
    public bool UmbrellaAttack(Transform attacker, Transform attacked, float angle, float radius)
    {
        Vector3 deltaA = attacked.position - attacker.position;
        float tmpAngel = Mathf.Acos(Vector3.Dot(deltaA.normalized, attacker.forward)) * Mathf.Rad2Deg;
        if (tmpAngel < angle * 0.5f && deltaA.magnitude < radius)
        {
            return true;
        }
        return false;
    }
    public void ToDrawSectorSolid(Transform t, Vector3 center, float angle, float radius)
    {
        int pointAmmount = 100;
        float eachAngle = angle / pointAmmount;
        Vector3 forward = t.forward;
        List<Vector3> Vertices = new List<Vector3>();
        Vertices.Add(center);
        for (int i = 0; i < pointAmmount; i++)
        {
            Vector3 pos = Quaternion.Euler(0f, -angle / 2 + eachAngle * (i - 1), 0f) * forward * radius + center;
            Vertices.Add(pos);
        }
        CreateMesh(Vertices);
    }
    private GameObject CreateMesh(List<Vector3> vertices)
    {
        int[] triangles;
        Mesh mesh = new Mesh();
        int triangleAmount = vertices.Count - 2;
        triangles = new int[3 * triangleAmount];
        for (int i = 0; i < triangleAmount; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }
        if (go == null)
        {
            go = new GameObject("mesh");
            go.transform.position = new Vector3(0f, 0f, 0f);
            mf = go.AddComponent<MeshFilter>();
            mr = go.AddComponent<MeshRenderer>();
            shader = Shader.Find("Unlit/Color");
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mf.mesh = mesh;
        mr.material.shader = shader;
        mr.material.color = Color.red;
        return go;
    }
    ////获取下一个路径点的方法
    //巡逻状态实现方法
    private void IdleState()
    {
        if (ca.Cur_HP > 0)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance)
                curState = FSMState.chase;
        }
        else
        {
            _animation.Play("Idle");
            if (go != null)
            {
                Destroy(go);
            }
        }
    }
    //追逐状态方法
    //private void animation_Manager(string name)
    //{
    //    AnimationEvent event0 = new AnimationEvent();
    //    event0.time = this._animation[name].length * 0.5f;
    //    event0.functionName = "takeDamage";
    //    _animation[name].clip.AddEvent(event0);
    //    _animation.Play(name);
    //}
    //private void takeDamage()
    //{
    //    print("test");
    //    this.transform.GetComponent<Damage>().TakeDamage(playerTransform.GetComponent<Status>());
    //}
    private void ChaseState()
    {
        //切换至追逐状态时，将目标位置置换成玩家位置
        targetPoint = playerTransform.position;
        //计算AI对象与玩家的距离
        float distance = Vector3.Distance(transform.position, targetPoint);
        if (ca.Cur_HP > 0)
        {
            //如果AI对象与玩家距离小于攻击距离时，将当前状态切换成攻击状态
            if (distance <= walkDistance)
                curState = FSMState.walk;
            ////如果当前距离已经大于巡逻状态，将当前状态切换成巡逻状态
            else if (distance >= chaseDistance)
                curState = FSMState.idle;
            //根据目标位置控制AI对象转向，根据目标位置与AI对象当前位置差值获取转向角度，再实现转向
            Vector3 a = new Vector3(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(a);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
            _controller.SimpleMove(transform.forward * Time.deltaTime * runSpeed);
            _animation.Play("Run");
            if (_animation.IsPlaying("Run"))
            {
                if (go != null)
                {
                    Destroy(go);
                }
            }
        }
        else
        {
            _animation.Play("Idle");
            if (go != null)
            {
                Destroy(go);
            }
        }

    }
    private void WalkState()
    {
        targetPoint = playerTransform.position;
        float distance = Vector3.Distance(transform.position, targetPoint);
        if (ca.Cur_HP > 0)
        {
            if (distance <= attackDistance)
            {
                flag = false;
                i = 1;
                curState = FSMState.attack;
            }
            //如果当前距离已经大于巡逻状态，将当前状态切换成巡逻状态
            else if (distance >= walkDistance)
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
            if (_animation.IsPlaying("Walk"))
            {
                if (go != null)
                {
                    Destroy(go);
                }
            }
        }
        else
        {
            _animation.Play("Idle");
            if (go != null)
            {
                Destroy(go);
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
        //判断是否满足攻击需求
        //如果距离已经大于攻击距离且小于等于追逐距离
        if(ca.Cur_HP>0)
        {
            if (distance > attackDistance && distance <= walkDistance)
            {
                if (!_animation.IsPlaying("Attack01") && !_animation.IsPlaying("Attack02") && !_animation.IsPlaying("Attack03"))
                {
                    //将当前状态转换成追逐状态
                    curState = FSMState.walk;
                    //退出计算
                    return;
                }
            }
            //如果距离已经大于追逐距离，将AI对象状态切换成巡逻状态
            else if (distance > walkDistance && !_animation.Play())
            {
                if (!_animation.IsPlaying("Attack01") && !_animation.IsPlaying("Attack02") && !_animation.IsPlaying("Attack03"))
                {
                    //将当前状态切换成巡逻状态
                    curState = FSMState.chase;
                    //退出计算
                    return;
                }
            }
            if (elapsedTime >= attackRate)
            {
                if (!flag)
                {
                    elapsedTime = 5.0f;
                    flag = true;
                }
                if (i == 1)
                {
                    _animation.Play("Attack01");
                    if (_animation.IsPlaying("Attack01"))
                    {
                        ToDrawSectorSolid(transform, transform.localPosition, 60, 5.6f);
                        if (UmbrellaAttack(transform, playerTransform, 60, 5.6f))
                        {
                            if (Time_damage > 1)
                            {
                                //takeDamage();
                                Damage(1);
                                Time_damage = 0;
                            }
                            Time_damage += Time.fixedDeltaTime;
                        }
                    }
                    if (elapsedTime > 6.4)
                    {
                        elapsedTime = 0;
                        i = Random.Range(1, 4);
                    }
                }
                if (i == 2)
                {
                    _animation.Play("Attack02");
                    if (_animation.IsPlaying("Attack02"))
                    {
                        ToDrawSectorSolid(transform, transform.localPosition, 60, 5.6f);
                        if (UmbrellaAttack(transform, playerTransform, 60, 5.6f))
                        {
                            if (Time_damage > 1)
                            {
                                //takeDamage();
                                Damage(3);
                                Time_damage = 0;
                            }
                            Time_damage += Time.fixedDeltaTime;
                        }
                    }
                    if (elapsedTime > 7.9)
                    {
                        elapsedTime = 0;
                        i = Random.Range(1, 4);
                    }
                }
                if (i == 3)
                {
                    _animation.Play("Attack03");
                    if (_animation.IsPlaying("Attack03"))
                    {
                        ToDrawSectorSolid(transform, transform.localPosition, 60, 5.6f);
                        if (UmbrellaAttack(transform, playerTransform, 60, 5.6f))
                        {
                            if (Time_damage > 1)
                            {
                                //takeDamage();
                                Damage(2);
                                Time_damage = 0;
                            }
                            Time_damage += Time.fixedDeltaTime;
                        }
                    }
                    if (elapsedTime > 6.6)
                    {
                        elapsedTime = 0;
                        i = Random.Range(1, 4);
                    }
                }
                Debug.Log(i);
            }
            else
            {
                _animation.Play("Idle");
            }
            if (_animation.IsPlaying("Idle"))
            {
                if (go != null)
                {
                    Destroy(go);
                }
                Vector3 a = new Vector3(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
                Quaternion rotation = Quaternion.LookRotation(a);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
            }
        }
        else
        {
            _animation.Play("Idle");
            if (go != null)
            {
                Destroy(go);
            }
        }
    }
    IEnumerator die()
    {
        _animation.Play("Dead");
        if (_animation.IsPlaying("Dead"))
        {
            if (go != null)
            {
                Destroy(go);
            }
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
    //死亡状态方法实现
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
