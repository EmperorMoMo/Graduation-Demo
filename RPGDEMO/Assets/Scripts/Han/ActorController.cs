using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    private Transform cameraTransform;
    public float walkSpeed = 2.0f;
    public float runMultiplier = 2.0f;
    public float smoothtime = 0.2f;
    private float currentvelocity;
    
    //public EffectInfo[] Effects;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private float rollVec = 1f;
    private float currentVelocity;
    private float lerpTarget;//状态机权重

    private bool lockPlanar = false;
    public bool lockCamera = false;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
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

        
        if ((pi.Dup != 0 || pi.Dright != 0 )&& lockCamera == false )
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
    }

    public void OnAttack1Enter()
    {
        pi.inputEnabled = false;
        lerpTarget = 1.0f;
        //InstantiateEffect(0);
    }

    public void OnAttack1Update()
    {
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.25f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }
    
    public void OnAttack2Enter()
    {
        //InstantiateEffect(1);
    }

    public void OnAttackIdleEnter()
    {
        pi.inputEnabled = true;
        print("on AttackIdleEnter!!!");
        lerpTarget = 0f;
    }

    public void OnAttackIdleUpdate()
    {
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.25f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }

    //[System.Serializable]
    //public class EffectInfo
    //{
    //    public GameObject Effect;
    //    public Transform StartPositionRotation;
    //    public float DestroyAfter = 10;
    //    public bool UseLocalPosition = true;
    //}

    //void InstantiateEffect(int EffectNumber)
    //{
    //    var instance = Instantiate(Effects[EffectNumber].Effect, Effects[EffectNumber].StartPositionRotation.position,
    //        Effects[EffectNumber].StartPositionRotation.rotation);

    //    Destroy(instance, Effects[EffectNumber].DestroyAfter);
    //}
}
