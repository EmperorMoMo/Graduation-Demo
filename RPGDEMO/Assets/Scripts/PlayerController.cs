using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float currentvelocity;
    private float velocityDup;
    private float velocityDright;
    public float smoothtime = 0.2f;
    public float walkSpeed = 3.0f;

    public float InputX;
    public float InputZ;

    public float Dup;
    public float Dright;
    public float targetDup;
    public float targetDright;
    public float Dmag;
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    private CharacterController controller;
    private Animator anim;
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 input =new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector2 inputdir = Vector2.zero;
        inputdir.x = InputX;
        inputdir.y = InputZ;
        inputdir.Normalize();

        targetDup = (Input.GetKey("w") ? 1.0f : 0) - (Input.GetKey("s") ? 1.0f : 0);
        targetDright= (Input.GetKey("d") ? 1.0f : 0) - (Input.GetKey("a") ? 1.0f : 0);
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
        Dmag = Mathf.Sqrt(((Dup * Dup) + (Dright * Dright)));

        float targetrotation = Mathf.Atan2(inputdir.x, inputdir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

        //判断是否为0
        if (inputdir!=Vector2.zero)
        {
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrotation,
                                        ref currentvelocity, smoothtime);
            controller.Move(transform.forward * walkSpeed*Time.deltaTime);
            anim.SetFloat("forward",Dmag);

        }
        else
        {
            
        }


    }
}
