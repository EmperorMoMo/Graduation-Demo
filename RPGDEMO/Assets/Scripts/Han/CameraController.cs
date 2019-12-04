using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float yaw;
    private float pitch;

    public float mousemoveSpeed = 2f;

    public Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * mousemoveSpeed;
        pitch -= Input.GetAxis("Mouse Y") * mousemoveSpeed;
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        //这里这个3是距离，相机跟角色的距离；
        transform.position = playerTransform.position - transform.forward * 3;

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
    }
}
