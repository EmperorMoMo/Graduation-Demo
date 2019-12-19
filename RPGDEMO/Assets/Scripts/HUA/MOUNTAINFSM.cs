using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOUNTAINFSM : MonsterFSM
{
    public enum FSMState
    {
        fly,
        idle,
        attack_far,
        attack_near,
        attack,
        dead
    }
    GameObject go;
    MeshFilter mf;
    MeshRenderer mr;
    Shader shader;
    private bool jizhi=false;
    public float Time_damage = 0;
    public float alert_Distance = 20;
    public float attack_far_Distance = 15;
    public float attack_near_Distance = 10;
    public float attack_Distance = 5;
    public FSMState curState;
    public float fallSpeed = 1;
    public float turnSpeed = 10;
    private Animation animation;
    private CharacterController controller;
    private bool isDead;
    private bool isFall = false;
    public GameObject Effect3;
    private Transform EffectPoint3;
    public GameObject Effect2;
    private Transform EffectPoint2;
    public GameObject Effect;
    private Transform EffectPoint1;
    private CharacterAttribute ca;
    private BossAttribute2 ba2;
    protected override void Initialize()
    {
        monsterHP = 100;
        curState = FSMState.fly;
        isDead = false;
        elapsedTime = 0;
        attackRate = 3;
        animation = GetComponent<Animation>();
        controller = GetComponent<CharacterController>();
        ca = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        ba2 = GetComponent<BossAttribute2>();
        GameObject obj = GameObject.FindGameObjectWithTag("TestPlayer");
        playerTransform = obj.transform;
        animation["FlyStationaryToLanding"].speed = 1.7f;
        EffectPoint1 = GameObject.Find("EffectPoint1").transform;
        EffectPoint2 = GameObject.Find("EffectPoint2").transform;
        EffectPoint3 = GameObject.Find("EffectPoint3").transform;
    }
    private void Damage(float s)
    {
        ca.Character_Attacked(ba2.Aggressivity * s);
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
            go.transform.position = new Vector3(0f, 0.1f, 0.5f);
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
    //private IEnumerator fallTime()
    //{
    //    Vector3 monsterTransform = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
    //    animation.Play("FlyStationaryToFall");
    //    yield return new WaitForSeconds(1f);
    //    controller.SimpleMove(monsterTransform * Time.deltaTime * fallSpeed);
    //    curState = FSMState.idle;
    //}
    private IEnumerator cd()
    {
        yield return new WaitForSeconds(1f);
        animation.Play("idleLookAround");
    }
    private void cdClear()
    {
        elapsedTime = 0;
        StartCoroutine(cd());
    }
    //private IEnumerator cd01()
    //{
    //    yield return new WaitForSeconds(0);
    //    animation.Play("idleLookAround");
    //}
    private void cdClear01()
    {
        elapsedTime = 0;
        animation.Play("idleLookAround");
        //StartCoroutine(cd01());
    }
    private void cdClear02()
    {
        elapsedTime = 0;
        animation.Play("idleBreathe");
        //StartCoroutine(cd01());
    }
    private void fallDown()
    {
        Vector3 monsterTransform = new Vector3(transform.position.x, 0, transform.position.z);
        transform.position = monsterTransform;
    }
    private void idle()
    {
        animation.Play("idleLookAround");
        isFall = true;
    }
    private void FlyState()
    {
        if (ca.Cur_HP > 0)
        {
            Vector3 monsterTransform = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
            //Vector3 monster = new Vector3(transform.position.x, 10, transform.position.z);
            if (Vector3.Distance(playerTransform.position, monsterTransform) <= alert_Distance)
            {
                curState = FSMState.idle;
                //StartCoroutine(fallTime());
            }
            //controller.SimpleMove(monster * Time.deltaTime * fallSpeed);
            animation.Play("FlyStationary");
            isFall = false;
        }
        else
        {
            animation.Play("idleLookAround");
            if (go != null&&jizhi==false)
            {
                Destroy(go);
            }
        }
        if (ba2.HP <= 0)
            curState = FSMState.dead;
    }
    //private void FallState()
    //{
    //    Vector3 monsterTransform = new Vector3(transform.position.x, 0, transform.position.z);
    //    if (Vector3.Distance(playerTransform.position, monsterTransform) <= alert_Distance)
    //    {
    //        curState = FSMState.idle;
    //    }
    //    animation.Play("FlyStationaryToFall");
    //}
    private void IdleState()
    {
        if (ca.Cur_HP > 0)
        {
            if (ba2.HP == ba2.MAX_HP * 0.75 || ba2.HP == ba2.MAX_HP * 0.5 || ba2.HP == ba2.MAX_HP * 0.25)
            {
                jizhi = true;
                waitTime();
            }
            Vector3 monsterTransform = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
            Vector3 m = new Vector3(transform.position.x, 6, transform.position.z);
            //if (Vector3.Distance(playerTransform.position, monsterTransform) <= attack_far_Distance && Vector3.Distance(playerTransform.position, monsterTransform) >= attack_near_Distance)
            //{
            //    curState = FSMState.attack_far;
            //}
            if (Vector3.Distance(playerTransform.position, monsterTransform) > alert_Distance)
            {
                curState = FSMState.fly;
                transform.position = m;
            }
            if (Vector3.Distance(playerTransform.position, monsterTransform) <= attack_far_Distance && Vector3.Distance(playerTransform.position, monsterTransform) > attack_near_Distance)
            {
                curState = FSMState.attack_far;
            }
            Vector3 a = new Vector3(playerTransform.position.x - transform.position.x, 0, playerTransform.position.z - transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(a);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
            if (!isFall)
                animation.Play("FlyStationaryToLanding");
            if (isFall)
                animation.Play("idleLookAround");
        }
        else
        {
            animation.Play("idleLookAround");
            if (go != null && jizhi == false)
            {
                Destroy(go);
            }
        }
        if (ba2.HP <= 0)
            curState = FSMState.dead;
    }
    private void FarAttackState()
    {
        if (ca.Cur_HP > 0)
        {
            if (ba2.HP == ba2.MAX_HP * 0.75 || ba2.HP == ba2.MAX_HP * 0.5 || ba2.HP == ba2.MAX_HP * 0.25)
            {
                jizhi = true;
                waitTime();
            }
            Vector3 monsterTransform = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
            Vector3 a = new Vector3(playerTransform.position.x - transform.position.x, 0, playerTransform.position.z - transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(a);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
            if (elapsedTime >= 4)
            {
                animation.Play("spitFireBall");
            }
            if (!animation.IsPlaying("spitFireBall"))
            {
                if (Vector3.Distance(playerTransform.position, monsterTransform) <= attack_near_Distance && Vector3.Distance(playerTransform.position, monsterTransform) > attack_Distance)
                {
                    elapsedTime = 3.5f;
                    curState = FSMState.attack_near;
                }
                if (Vector3.Distance(playerTransform.position, monsterTransform) > attack_far_Distance && Vector3.Distance(playerTransform.position, monsterTransform) <= alert_Distance)
                {
                    curState = FSMState.idle;
                }
            }
        }
        else
        {
            animation.Play("idleLookAround");
            if (go != null && jizhi == false)
            {
                Destroy(go);
            }
        }
        if (ba2.HP <= 0)
            curState = FSMState.dead;
    }
    private void NearAttackState()
    {
        if (ca.Cur_HP > 0)
        {
            if (ba2.HP == ba2.MAX_HP * 0.75 || ba2.HP == ba2.MAX_HP * 0.5 || ba2.HP == ba2.MAX_HP * 0.25)
            {
                jizhi = true;
                waitTime();
            }
            Vector3 monsterTransform = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
            if (go != null && jizhi == false)
            {
                Destroy(go);
            }
            if (elapsedTime >= 6f)
            {
                animation.Play("spreadFire");
                InstantiateEffect();
            }
            else
            {
                animation.Play("idleLookAround");
            }
            if (!animation.IsPlaying("spreadFire"))
            {
                if (Vector3.Distance(playerTransform.position, monsterTransform) > attack_near_Distance && Vector3.Distance(playerTransform.position, monsterTransform) <= attack_far_Distance)
                {
                    curState = FSMState.attack_far;
                    elapsedTime = 4;
                }
                if (Vector3.Distance(playerTransform.position, monsterTransform) <= attack_Distance)
                {
                    Vector3 a1 = new Vector3(playerTransform.position.x - transform.position.x, 0, playerTransform.position.z - transform.position.z);
                    Quaternion rotation1 = Quaternion.LookRotation(a1);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation1, Time.deltaTime * turnSpeed * 3f);
                    curState = FSMState.attack;
                }
                Vector3 a = new Vector3(playerTransform.position.x - transform.position.x, 0, playerTransform.position.z - transform.position.z);
                Quaternion rotation = Quaternion.LookRotation(a);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
            }
        }
        else
        {
            animation.Play("idleLookAround");
            if (go != null && jizhi == false)
            {
                Destroy(go);
            }
        }
        if (ba2.HP <= 0)
            curState = FSMState.dead;
    }
    private void AttackState()
    {
        if (ca.Cur_HP > 0)
        {
            if (ba2.HP == ba2.MAX_HP * 0.75 || ba2.HP == ba2.MAX_HP * 0.5 || ba2.HP == ba2.MAX_HP * 0.25)
            {
                jizhi = true;
                waitTime();
            }
            Vector3 monsterTransform = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
            if (elapsedTime >= 2)
            {
                animation.Play("bite");
                ToDrawSectorSolid(transform, transform.localPosition, 45, 5);
                if (UmbrellaAttack(transform, playerTransform, 45, 5))
                {
                    if (Time_damage > 1f)
                    {
                        Damage(1);
                        Time_damage = 0;
                    }
                    Time_damage += Time.fixedDeltaTime;
                }
            }
            if (!animation.IsPlaying("bite"))
            {
                if (Vector3.Distance(playerTransform.position, monsterTransform) > attack_Distance)
                {

                    curState = FSMState.attack_near;
                    elapsedTime = 3f;
                }
                if (go != null)
                {
                    Destroy(go);
                }
                Vector3 a = new Vector3(playerTransform.position.x - transform.position.x, 0, playerTransform.position.z - transform.position.z);
                Quaternion rotation = Quaternion.LookRotation(a);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
            }
        }
        else
        {
            animation.Play("idleLookAround");
            if (go != null && jizhi == false)
            {
                Destroy(go);
            }
        }
        if (ba2.HP <= 0)
            curState = FSMState.dead;
    }
    IEnumerator die()
    {
        animation.Play("death");
        if (animation.IsPlaying("death"))
        {
            if (go != null)
            {
                Destroy(go);
            }
        }
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
    private void DeadState()
    {
        StartCoroutine(die());
    }
    protected override void FSMUpdate()
    {
        //将当前状态传入
        switch (curState)
        {
            //判断相应的状态,调用相应方法
            //巡逻状态         
            case FSMState.fly:
                FlyState();
                break;
            //追逐状态
            case FSMState.idle:
                IdleState();
                break;
            //攻击状态
            case FSMState.attack_far:
                FarAttackState();
                break;
            case FSMState.attack_near:
                NearAttackState();
                break;
            //死亡状态
            case FSMState.attack:
                AttackState();
                break;
            case FSMState.dead:
                DeadState();
                break;
        }
        //计算攻击状态中上一次攻击时间
        elapsedTime += Time.deltaTime;
        //Debug.Log(elapsedTime);
    }
    IEnumerator waitTime()
    {
        ToDrawSectorSolid(playerTransform, playerTransform.localPosition, 360, 3);
        yield return new WaitForSeconds(1.5f);
        InstantiateEffect3();
        if (go != null)
        {
            Destroy(go);
        }
        jizhi = false; 
    }
    public void InstantiateEffect()
    {
        var instance = Instantiate(Effect, EffectPoint1.position, EffectPoint1.rotation);
        Destroy(instance, 0.2f);
    }
    public void InstantiateEffect2()
    {
        var instance = Instantiate(Effect2, EffectPoint2.position, EffectPoint2.rotation);
        Destroy(instance, 1f);
    }
    public void InstantiateEffect3()
    {
        var instance = Instantiate(Effect3, playerTransform.position, EffectPoint3.rotation);
        Destroy(instance, 4f);
    }
}
