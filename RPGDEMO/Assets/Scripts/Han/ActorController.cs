using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public enum State
    {
        normalAtk,
        skill_One,
        skill_Two,
        skill_Three,
        pickup,
    }
    
    public GameObject model;
    public PlayerInput pi;
    private Transform cameraTransform;

    private Transform flashPoint;
    //private GameObject cameraTransform;
    public float walkSpeed = 2.0f;
    public float runMultiplier = 2.0f;
    public float smoothtime = 0.2f;
    private float currentvelocity;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private float rollVec = 1f;
    private float currentVelocity;
    //private float lerpTarget;//状态机权重

    private bool lockPlanar = false;
    private bool lockCamera = false;
    public bool isDam;
    private bool stillRun;

    [Header("=====  Attack =====")]
    private float normalDis;//普攻距离

    private float skill_OneDis;//技能距离
    public State str;//当前是何种攻击方式

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        flashPoint = GameObject.Find("flashPoint").GetComponent<Transform>();

        normalDis = 3f;
        skill_OneDis = 4f;
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

        if (pi.roll)
        {
            anim.SetTrigger("roll");
        }

        if (pi.attack)
        {
            anim.SetTrigger("attack");
        }

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

        if (pi.skill_1)
        {
            anim.SetTrigger("skill_1");
        }

        if (pi.skill_2)
        {
            anim.SetTrigger("skill_2");
        }

        if (pi.skill_3)
        {
            anim.SetTrigger("skill_3");
        }

        if (pi.pickup)
        {
            anim.SetTrigger("pickup");
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

    public void OnAttack1Enter()
    {
        pi.inputEnabled = false;
        lockCamera = true;
        //lerpTarget = 1.0f;
        str = State.normalAtk;
        //Select(str);
    }
    
    public void OnAttack2Enter()
    {
        str = State.normalAtk;
        lockCamera = true;
        //Select(str);
    }

    public void OnAttackIdleEnter()
    {
        pi.inputEnabled = true;
        lockCamera = false;
        //lerpTarget = 0f;
    }

    public void OnSkillOneEnter()
    {
        pi.inputEnabled = false;
        lockCamera = true;
        str = State.skill_One;
    }

    public void OnSkillTwoEnter()
    {
        pi.inputEnabled = false;
        lockCamera = true;
        str = State.skill_Two;
    }

    public void OnSkillThreeEnter()
    {
        pi.inputEnabled = false;
        lockCamera = true;
        str = State.skill_Three;
    }

    public void OnPickUpEnter()
    {
        pi.inputEnabled = false;
        lockCamera = true;
        str = State.pickup;
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
                    if (cols[i].tag == "Enemy")
                    {
                        //print("test3"+cols[i].name);
                        float dir = Vector3.Distance(model.transform.position, cols[i].transform.position);
                        float angle = Vector3.Angle(model.transform.forward,
                            cols[i].transform.position - model.transform.position);
                        if (str == State.normalAtk)
                        {
                            //print("test4");
                            if (dir < normalDis && angle < 90)
                            {
                                tempList.Add(cols[i].gameObject);
                                //print("test5");
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

                        if (str == State.pickup)
                        {
                            if (dir < normalDis && angle < 60)
                            {
                                tempList.Add(cols[i].gameObject);
                                isDam = true;
                            }
                        }
                    }
                }
            }

            radius += 1;
        }

        foreach (var objects in tempList)
        {
            if (objects.GetComponent<Rigidbody>() != null && str == State.normalAtk)
            {
                objects.GetComponent<Rigidbody>().freezeRotation = true;

                objects.GetComponent<Rigidbody>().AddExplosionForce(180, transform.position, 5, 180);
                //isDam = true;
                
            }

            if (objects.GetComponent<Rigidbody>() != null && str == State.skill_One)
            {
                objects.GetComponent<Rigidbody>().freezeRotation = true;

                objects.GetComponent<Rigidbody>().AddExplosionForce(165, transform.position, 4, 150);
            }

            if (objects.GetComponent<Rigidbody>() != null && str == State.pickup)
            {
                objects.transform.position = new Vector3(objects.transform.position.x, 2.5f,
                    objects.transform.position.z);
                //objects.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                //objects.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePosition;
                
            }
            objects.GetComponent<EnemyAI>().Damage();
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
