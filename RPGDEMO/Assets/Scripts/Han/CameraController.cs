using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float yaw;
    private float pitch;

    public float mousemoveSpeed = 2f;
    public float temp=3.0f;

    private Transform playerTransform;

    private PlayerInput pi;
    private bool lockCamera;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GameObject.Find("PlayerHandle").GetComponent<PlayerInput>();
        playerTransform = GameObject.Find("Follow").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockCamera)
        {
            yaw += Input.GetAxis("Mouse X") * mousemoveSpeed;
            pitch -= Input.GetAxis("Mouse Y") * mousemoveSpeed;
            transform.eulerAngles = new Vector3(pitch, yaw, 0);
        }

        //这里这个3是距离，相机跟角色的距离；
        transform.position = playerTransform.position - transform.forward * temp;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 70)
            {
                Camera.main.fieldOfView += 3;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView >= 30)
            {
                Camera.main.fieldOfView -= 3;
            }
        }

        
        if (Input.GetKey(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pi.enabled = false;
            lockCamera = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pi.enabled = true;
            lockCamera = false;
        }

        RaycastHit[] hits;
        Vector3 dir = -(playerTransform.position - transform.position).normalized;
        hits = Physics.RaycastAll(playerTransform.position, dir,
            Vector3.Distance(playerTransform.position, transform.position));
        //Debug.DrawRay(playerTransform.position, dir, Color.green);
        if (hits.Length > 0 && hits[0].collider.tag != "MainCamera")
        {
            transform.position = hits[0].point;
        }

    }
}
