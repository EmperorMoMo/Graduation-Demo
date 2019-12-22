using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActorController : MonoBehaviour
{
    public enum State
    {
        normalAtk,
        normalAtk1,
        skill_One,
        skill_Two,
        skill_Three,
    }
    
    public GameObject model;
    public PlayerInput pi;
    private Transform cameraTransform;
    private CharacterAttribute ca;
    private GameObject Increase_Point;
    public GameObject IncreaseSkill;
    public GameObject LeftSword;

    //private GameObject cameraTransform;
    public float walkSpeed = 2.0f;
    public float runMultiplier = 2.0f;
    public float smoothtime = 0.2f;
    private float currentvelocity;
    private float Aggressivity_Temp;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private float rollVec = 1f;
    private float currentVelocity;

    public float Increase_time = 0f;
    //private float lerpTarget;//状态机权重

    private bool lockPlanar = false;
    private bool lockCamera = false;
    public bool isDam;
    private bool stillRun;
    public bool _isAttacked;
    public bool canAttacked;
    public bool die;
    public bool increase;

    //右键变身代码
    public bool change = false;
    public bool _isChange;
    public float nengliang = 0f;
    public float ChangeTimer = 0f;
    public float Armor_temp = 0f;


    [Header("=====  Attack =====")]
    private float normalDis;//普攻距离

    private float skill_OneDis;//技能距离
    public State str;//当前是何种攻击方式

    [Header("===== CD Manager =====")]
    private bool skill_one_CD = true;
    private bool skill_two_CD = true;
    private bool skill_three_CD = true;
    private bool skill_four_CD = true;
    private bool roll_CD = true;

    public float CD_skill_one = 0;         //10
    public float CD_skill_two = 0;         //8
    public float CD_skill_three = 0;       //15
    public float CD_skill_four = 0;        //45
    private float CD_roll = 0;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        Increase_Point = GameObject.Find("IncreasePoint");
        ca = GetComponent<CharacterAttribute>();
        LeftSword=GameObject.Find("LeftSword");
        LeftSword.SetActive(false);

        normalDis = 4f;
        skill_OneDis = 4f;
        _isAttacked = false;

        Increase_Point = Instantiate(IncreaseSkill, Increase_Point.transform.position, Increase_Point.transform.rotation);
        Increase_Point.transform.parent = model.transform;
        Increase_Point.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
            //cameraTransform=GameObject.Find("Main Camera").transform;
            print("null");
        }
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.run ? 2.0f : 1.0f), 0.4f));
        if (pi.jump)
        {
            anim.SetTrigger("jump");
        }

        if (pi.roll&&roll_CD)
        {
            anim.SetTrigger("roll");
            canAttacked = false;
            roll_CD = false;
        }
        if (roll_CD == false)
        {
            CD_roll += Time.deltaTime;
            if (CD_roll > 2)
            {
                roll_CD = true;
                CD_roll = 0;
            }
        }

        ///
        ///普通攻击动画播放代码区域
        /// 

        nengliang = Mathf.Clamp(nengliang, 0, 100);
        if (pi.attack)
        {
            anim.SetTrigger("attack");
        }
        
        if (nengliang == 100)
        {
            change = true;
        }

        if (pi.attack1&&change==true)
        {
            anim.SetTrigger("attack1");
            anim.SetBool("change",true);
            _isChange = true;
        }

        if (_isChange)
        {
            ChangeTimer += Time.deltaTime;
            if (ChangeTimer > 8.5)
            {
                _isChange = false;
                change = false;
                nengliang = 0f;
                ChangeTimer = 0f;
            }
        }

        if (change == false)
        {
            anim.SetBool("change",false);
        }
        ///
        ///
        /// 

        if (pi.Dmag > 0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
        }

        if (lockPlanar == false)
        {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.run ? runMultiplier : 1.0f);
        }

        
        if ( (pi.Dup!=0||pi.Dright!=0)&& lockCamera == false )
        {
            float targetrotation = cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrotation,
                                        ref currentvelocity, smoothtime);
        }

        if (pi.skill_1&&skill_one_CD)
        {
            pi.canUseSkill = false;
            anim.SetTrigger("skill_1");
            canAttacked = false;
            skill_one_CD = false;
        }
        if (skill_one_CD == false)
        {
            CD_skill_one += Time.deltaTime;
            if (CD_skill_one > 10)
            {
                skill_one_CD = true;
                CD_skill_one = 0;
            }
        }
        
        if (pi.skill_2&&skill_two_CD)
        {
            pi.canUseSkill = false;
            anim.SetTrigger("skill_2");
            canAttacked = false;
            skill_two_CD = false;
        }
        if (skill_two_CD == false)
        {
            CD_skill_two += Time.deltaTime;
            if (CD_skill_two > 8)
            {
                skill_two_CD = true;
                CD_skill_two = 0;
            }
        }

        if (pi.skill_3&&skill_three_CD)
        {
            pi.canUseSkill = false;
            anim.SetTrigger("skill_3");
            canAttacked = false;
            skill_three_CD = false;
        }
        if (skill_three_CD == false)
        {
            CD_skill_three += Time.deltaTime;
            if (CD_skill_three > 15)
            {
                skill_three_CD = true;
                CD_skill_three = 0;
            }
        }

        if (_isAttacked)
        {
            anim.SetTrigger("isattacked");

        }

        if (die)
        {
            anim.SetTrigger("die");
            pi.inputEnabled = false;
            lockPlanar = true;
            canAttacked = false;
            lockCamera = true;
            pi.Dmag = 0;
        }

        if (pi.increaseskill_1&&skill_four_CD)
        {
            Increase_Point.SetActive(true);
            Aggressivity_Temp = ca.finalAttribute.Aggressivity;
            ca.finalAttribute.Aggressivity += Aggressivity_Temp * 0.5f;
            increase = true;
            pi.increaseskill_1 = false;
            skill_four_CD = false;
        }
        if (skill_four_CD == false)
        {
            CD_skill_four += Time.deltaTime;
            if (CD_skill_four > 45)
            {
                skill_four_CD = true;
                CD_skill_four = 0;
            }
        }

        if (increase)
        {
            Increase_time += Time.deltaTime;
            if (Increase_time > 10)
            {
                Increase_Point.SetActive(false);
                ca.finalAttribute.Aggressivity -= Aggressivity_Temp * 0.5f;
                Increase_time = 0;
                increase = false;
            }
        }

        if (pi.reBirth && die)
        {
            die = false;
            anim.ResetTrigger("die");
            ca.Cur_HP = 1f;
            anim.SetBool("rebirth", true);
            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadSceneAsync("LoadingScene");
        }
    }

    void FixedUpdate()
    {
        //rigid.position += planarVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec=Vector3.zero;
        rollVec = 1f;
    }

    /// 
    /// 
    ///

    public void OnGround()
    {
        pi.inputEnabled = true;
        lockCamera = false;
        canAttacked = true;
        pi.canUseSkill = true;
        LeftSword.SetActive(false);
        anim.SetBool("rebirth", false);

        lockPlanar = false;
        //this.gameObject.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePosition;
    }

    public void OnJumpEnter()
    {
        //print("on jump enter!");
        pi.inputEnabled = false;
        lockPlanar = true;
        lockCamera = true;
        thrustVec = new Vector3(0, 3f, 0);
    }

    public void OnJumpUpdate()
    {

    }

    public void OnJumpExit()
    {
        //print("on jump exit!");
        pi.inputEnabled = true;
        lockPlanar = false;
        lockCamera = false;
    }

    public void OnRollEnter()
    {
        Vector3 temp=new Vector3();
        temp = planarVec;
        planarVec=Vector3.zero;

        pi.inputEnabled = false;
        lockPlanar = true;
        lockCamera = true;
        if (pi.run)
        {
            rollVec = 2.5f;
            stillRun = true;
        }
        else
        {
            rollVec = 5f;
        }
        planarVec = temp;
        planarVec *= rollVec;
    }

    public void OnRollExit()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
        lockCamera = false;
        anim.ResetTrigger("attack");
        planarVec = Vector3.zero;
        if (stillRun)
        {
            pi.run = true;
        }
    }

    /// <summary>
    /// 普通攻击信号处理区域
    /// </summary>
    
    public void OnAttackEnter01()
    {
        pi.inputEnabled = false;
        //lockCamera = true;
        str = State.normalAtk;
        pi.canUseSkill = false;
    }

    public void OnAttackEnter02()
    {
        pi.inputEnabled = false;
        //lockCamera = true;
        str = State.normalAtk;
        pi.canUseSkill = false;
    }

    public void OnAttackEnter06()
    {
        pi.inputEnabled = false;
        //lockCamera = true;
        str = State.normalAtk;
        pi.canUseSkill = false;
    }

    public void OnAttackEnter07()
    {
        pi.inputEnabled = false;
        //lockCamera = true;
        str = State.normalAtk;
        pi.canUseSkill = false;
    }

    public void OnChangeAttackEnter05()
    {
        pi.inputEnabled = false;
        str = State.normalAtk1;
    }

    public void OnChangeAttackEnter01()
    {
        pi.inputEnabled = false;
        str = State.normalAtk1;
    }

    public void OnEnter()
    {
        Armor_temp = ca.finalAttribute.Armor*0.5f;
        ca.finalAttribute.Armor += Armor_temp;
        pi.canUseSkill = false;
        canAttacked = false;
        pi.inputEnabled = false;
        LeftSword.SetActive(true);
    }

    public void OnChangeIdleEnter()
    {
        pi.inputEnabled = true;
    }

    public void OnExit()
    {
        ca.finalAttribute.Armor -= Armor_temp;
        pi.canUseSkill = true;
        canAttacked = true;
    }

    public void OnSkillOneEnter()
    {
        pi.inputEnabled = false;
        lockCamera = true;
        str = State.skill_One;
        nengliang += 15;
    }

    public void OnSkillTwoEnter()
    {
        pi.inputEnabled = false;
        lockCamera = true;
        str = State.skill_Two;
        nengliang += 10;
    }

    public void OnSkillThreeEnter()
    {
        pi.inputEnabled = false;
        lockCamera = true;
        str = State.skill_Three;
        nengliang += 20;
    }

    public void OnAttackedEnter()
    {
        pi.inputEnabled = false;
        pi.Dmag = 0;
    }

    public void OnAttackedUpdate()
    {
        _isAttacked = false;
        anim.ResetTrigger("isattacked");
    }

    public void OnDieEnter()
    {
        anim.ResetTrigger("isattacked");

    }

    public void Select(State str)
    {
        isDam = false;
        int radius = 12;
        List<GameObject> tempList=new List<GameObject>();
        while (radius < 13)
        {
            Collider[] cols = Physics.OverlapSphere(model.transform.position, radius);
            if (cols.Length > 0)
            {
                //print("test1");
                for (int i = 0; i < cols.Length; i++)
                {
                    //print("test2"+cols[i].name);
                    if (cols[i].tag == "Enemy"||cols[i].tag == "Enemy1"||cols[i].tag=="Enemy2")
                    {
                        //print("test3"+cols[i].name);
                        float dir = Vector3.Distance(model.transform.position, cols[i].transform.position);
                        float angle = Vector3.Angle(model.transform.forward,
                            cols[i].transform.position - model.transform.position);
                        if (str == State.normalAtk)
                        {
                            if (dir < normalDis && angle < 90)
                            {
                                tempList.Add(cols[i].gameObject);
                                isDam = true;
                            }
                        }
                        if (str == State.skill_One)
                        {
                            if (dir < skill_OneDis && angle < 360)
                            {
                                tempList.Add(cols[i].gameObject);
                                isDam = true;
                            }
                        }
                        if (str == State.normalAtk1)
                        {
                            if (dir < normalDis && angle < 90)
                            {
                                tempList.Add(cols[i].gameObject);
                                isDam = true;
                            }
                        }
                    }

                    if (cols[i].tag == "Boss"||cols[i].tag=="Boss2")
                    {
                        float dir = Vector3.Distance(model.transform.position, cols[i].transform.position);
                        float angle = Vector3.Angle(model.transform.forward,
                            cols[i].transform.position - model.transform.position);

                        if (str == State.normalAtk)
                        {
                            //print("test4");
                            if (dir < normalDis && angle < 90)
                            {
                                if (cols[i].tag == "Boss")
                                {
                                    cols[i].GetComponent<BossAttribute>().Boss_Attacked(ca.finalAttribute.Aggressivity*0.5f);
                                    isDam = true;
                                }
                                else
                                {
                                    cols[i].GetComponent<BossAttribute2>().Boss_Attacked(ca.finalAttribute.Aggressivity*0.5f);
                                    isDam = true;
                                }
                            }
                        }

                        if (str == State.normalAtk1)
                        {
                            //print("test4");
                            if (dir < normalDis && angle < 90)
                            {
                                if (cols[i].tag == "Boss")
                                {
                                    cols[i].GetComponent<BossAttribute>().Boss_Attacked(ca.finalAttribute.Aggressivity*0.5f);
                                    isDam = true;
                                }
                                else
                                {
                                    cols[i].GetComponent<BossAttribute2>().Boss_Attacked(ca.finalAttribute.Aggressivity * 0.5f);
                                    isDam = true;
                                }
                            }
                        }

                        if (str == State.skill_One)
                        {
                            if (dir < skill_OneDis && angle < 360)
                            {
                                if (cols[i].tag == "Boss")
                                {
                                    cols[i].GetComponent<BossAttribute>().Boss_Attacked(ca.finalAttribute.Aggressivity*0.5f);
                                    isDam = true;
                                }
                                else
                                {
                                    cols[i].GetComponent<BossAttribute2>().Boss_Attacked(ca.finalAttribute.Aggressivity*0.5f);
                                    isDam = true;
                                }
                            }
                        }
                    }
                }
            }

            radius += 1;
        }

        foreach (var objects in tempList)
        {
            if (str == State.normalAtk)
            {
                if (objects.tag == "Enemy")
                {
                    objects.GetComponent<EnemyAttribute>().Enemy_Attacked(ca.finalAttribute.Aggressivity * 0.5f);
                }
                else if (objects.tag == "Enemy1")
                {
                    objects.GetComponent<EnemyAttribute1>().Enemy_Attacked1(ca.finalAttribute.Aggressivity * 0.5f);
                }
                else if (objects.tag == "Enemy2")
                {
                    objects.GetComponent<EnemyAttribute2>().Enemy_Attacked2(ca.finalAttribute.Aggressivity * 0.5f);
                }
            }

            if (str == State.normalAtk1)
            {
                if (objects.tag == "Enemy")
                {
                    objects.GetComponent<EnemyAttribute>().Enemy_Attacked(ca.finalAttribute.Aggressivity * 0.5f);
                }
                else if (objects.tag == "Enemy1")
                {
                    objects.GetComponent<EnemyAttribute1>().Enemy_Attacked1(ca.finalAttribute.Aggressivity * 0.5f);
                }
                else if (objects.tag == "Enemy2")
                {
                    objects.GetComponent<EnemyAttribute2>().Enemy_Attacked2(ca.finalAttribute.Aggressivity * 0.5f);
                }
            }

            if (objects.GetComponent<Rigidbody>() != null && str == State.skill_One)
            {
                objects.GetComponent<Rigidbody>().freezeRotation = true;

                objects.GetComponent<Rigidbody>().AddExplosionForce(165, transform.position, 4, 150);

                if (objects.tag == "Enemy")
                {
                    objects.GetComponent<EnemyAttribute>().Enemy_Attacked(ca.finalAttribute.Aggressivity * 0.75f);
                }
                else if (objects.tag == "Enemy1")
                {
                    objects.GetComponent<EnemyAttribute1>().Enemy_Attacked1(ca.finalAttribute.Aggressivity * 0.75f); 
                }
                else if (objects.tag == "Enemy2")
                {
                    objects.GetComponent<EnemyAttribute2>().Enemy_Attacked2(ca.finalAttribute.Aggressivity * 0.75f);
                }
            }

            //objects.GetComponent<EnemyAI>().Damage();
        }

    }

    public void CD_Manager(bool _name,float _timer,float _time)
    {
        if (_name==false)
        {
            _timer += Time.deltaTime;
            Debug.Log("_timer:"+_timer);
            if (_timer > _time)
            {
                _name = true;
                _timer = 0;
            }
        }
    }

    ///
    ///寻找敌人的另一种方法
    /// 
    //public void Select(State str)
    //{
    //    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
    //    List<GameObject> tempList=new List<GameObject>();
    //    for (int i = 0; i < enemy.Length; i++)
    //    {
    //        float dir = Vector3.Distance(model.transform.position, enemy[i].transform.position);
    //        if (str == State.normalAtk)
    //        {
    //            float angle = Vector3.Angle(model.transform.forward, enemy[i].transform.position - model.transform.position);
    //            Debug.Log(angle);
    //            if (dir < normalDis && angle < 60)
    //            {
    //                tempList.Add(enemy[i]);
    //            }
    //            else
    //            {
    //                isDam = false;
    //            }
    //        }
    //    }

    //    foreach (var objects in tempList)
    //    {
    //        if (objects.GetComponent<Rigidbody>() != null && str == State.normalAtk)
    //        {
    //            objects.GetComponent<Rigidbody>().freezeRotation = true;

    //            objects.GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 3, 100);
    //            isDam = true;

    //        }
    //        objects.GetComponent<EnemyAI>().Damage();

    //        //Stop(0.1f);
    //    }
    //}

}
