﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public enum State
    {
        normalAtk,
        skillAtk,
    }
    
    public GameObject model;
    public PlayerInput pi;
    private Transform cameraTransform;
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
    private float lerpTarget;//状态机权重

    private bool lockPlanar = false;
    private bool lockCamera = false;
    public bool isDam;

    [Header("=====  Attack =====")]
    private float normalDis;//普攻距离

    private float skillDis;//技能距离
    public State str;//当前是何种攻击方式

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        normalDis = 3f;
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

        
        if ( (pi.targetDup!=0||pi.targetDright!=0)&& lockCamera == false )
        {
            float targetrotation = cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrotation,
                                        ref currentvelocity, smoothtime);
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
    public void OnJumpEnter()
    {
        //print("on jump enter!");
        pi.inputEnabled = false;
        lockPlanar = true;
        lockCamera = true;
        thrustVec = new Vector3(0, 3.6f, 0);
    }

    public void OnJumpExit()
    {
        //print("on jump exit!");
        pi.inputEnabled = true;
        lockPlanar = false;
        lockCamera = false;
        anim.ResetTrigger("attack");
    }

    public void OnRollEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        lockCamera = true;
        if (pi.run == true)
        {
            rollVec = 2f;
        }
        else
        {
            rollVec = 4f;
        }
        planarVec *= rollVec;
        //print("on roll enter!");
    }

    public void OnRollExit()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
        lockCamera = false;
        anim.ResetTrigger("attack");
    }

    public void OnGround()
    {
        pi.inputEnabled = true;
        lockCamera = false;
    }

    public void OnAttack1Enter()
    {
        pi.inputEnabled = false;
        lockCamera = true;
        lerpTarget = 1.0f;
        str = State.normalAtk;
        //Select(str);
    }

    public void OnAttack1Update()
    {
        //float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        //currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.25f);
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
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
        lerpTarget = 0f;
    }

    public void OnAttackIdleUpdate()
    {
        //float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        //currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.25f);
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }
    
    public void Select(State str)
    {
        int radius = 1;
        List<GameObject> tempList=new List<GameObject>();
        while (radius < 50)
        {
            Collider[] cols = Physics.OverlapSphere(model.transform.position, radius);
            if (cols.Length > 0)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].tag.Equals("Enemy"))
                    {
                        float dir = Vector3.Distance(model.transform.position, cols[i].transform.position);
                        if (str == State.normalAtk)
                        {
                            float angle = Vector3.Angle(model.transform.forward,
                                cols[i].transform.position - model.transform.position);
                            if (dir < normalDis && angle < 90)
                            {
                                tempList.Add(cols[i].gameObject);
                                isDam = true;
                            }
                            else
                            {
                                isDam = false;
                            }
                        }
                    }
                }
            }

            radius += 2;
        }

        foreach (var objects in tempList)
        {
            if (objects.GetComponent<Rigidbody>() != null && str == State.normalAtk)
            {
                objects.GetComponent<Rigidbody>().freezeRotation = true;

                objects.GetComponent<Rigidbody>().AddExplosionForce(10, transform.position, 3, 10);
                //isDam = true;

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
