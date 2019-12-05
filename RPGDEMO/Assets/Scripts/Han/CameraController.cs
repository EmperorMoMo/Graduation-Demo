using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float yaw;
    private float pitch;

    public float mousemoveSpeed = 2f;
    public float temp=3.0f;

    public Transform playerTransform;

    public PlayerInput pi;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * mousemoveSpeed;
        pitch -= Input.GetAxis("Mouse Y") * mousemoveSpeed;
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //if (pi.hide)
        //{
        //    Cursor.visible = true;
        //}

        //if (Check(-1.7f)&&temp>0.5f)
        //{
        //    temp -= 0.5f;
        //}
        //else if (!Check(1.7f) && temp < 3f)
        //{
        //    temp += 0.5f;
        //}

        RaycastHit[] hits;
        Vector3 dir = -(playerTransform.position - transform.position).normalized;
        hits = Physics.RaycastAll(playerTransform.position, dir,
            Vector3.Distance(playerTransform.position, transform.position));
        Debug.DrawRay(playerTransform.position, dir, Color.green);
        if (hits.Length > 0 && hits[0].collider.tag != "MainCamera")
        {
            transform.position = hits[0].point;
        }
        //{
        //    string name = hit.collider.gameObject.tag;
        //    if (name != "MainCamera")
        //    {
        //        transform.position = hit.point;
        //    }
        //    else
        //    {
        //        transform.position = playerTransform.position - transform.forward * temp;
        //    }
        //}

    }
    

    //bool Check(float p)
    //{
    //    Vector3 dir = -(playerTransform.position - transform.position).normalized;
    //    hits = Physics.RaycastAll(playerTransform.position, dir,
    //        Vector3.Distance(playerTransform.position, transform.position) + p);

    //    Debug.DrawRay(playerTransform.position, dir, Color.red,
    //        Vector3.Distance(playerTransform.position, transform.position));
    //    if (hits.Length > 0 && hits[0].collider.tag != "Player")
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}
