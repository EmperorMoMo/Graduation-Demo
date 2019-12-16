using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Variable变量区
    [Header("=====  key settings =====")]
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;
    public string keyE;
    public string keyF;

    [Header("=====  output signals =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    //1.pressing signal按压型
    public bool run;

    public bool hide;
    //2.trgger once type signal一次性触发
    public bool jump;

    public bool roll;

    public bool skill_1;

    public bool skill_2;

    public bool skill_3;

    public bool increaseskill_1;

    public bool pickup;

    public bool canUseSkill = true;
    //3.double trigger双击型
    public bool lastAttack;
    public bool attack;

    [Header("=====  others =====")]
    public bool inputEnabled = true;

    public float targetDup;
    public float targetDright;
    private float velocityDup;
    private float velocityDright;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnabled == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt(((Dup2 * Dup2) + (Dright2 * Dright2)));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        if (Input.GetKeyDown(keyA))
        {
            run = true;
        }
        else if (targetDup==0&&targetDright==0)
        {
            run = false;
        }
        
        jump = Input.GetKeyDown(keyB);
        //if (jump)
        //{
        //    print("Jump trigger!!!!");
        //}
        if (targetDup != 0||targetDright != 0)
        {
            roll = Input.GetKeyDown(keyC);
        }
        else
        {
            roll = false;
        }

        //hide = Input.GetKeyDown(KeyCode.Z);

        bool newAttack = Input.GetMouseButton(0);
        if (newAttack != lastAttack && newAttack == true)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;
        
        pickup = Input.GetMouseButtonDown(1);

        if (canUseSkill)
        {
            skill_1 = Input.GetKeyDown(keyD);

            skill_2 = Input.GetKeyDown(keyE);

            skill_3 = Input.GetKeyDown(keyF);

            increaseskill_1 = Input.GetKeyDown(KeyCode.Alpha1);
        }

    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output=Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }
}
