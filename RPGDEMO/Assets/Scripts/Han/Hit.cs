using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    private ActorController ac;

    private bool waiting;

    public bool shakeCamera;
    // Start is called before the first frame update
    void Awake()
    {
        ac = this.transform.GetComponentInParent<ActorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Hit1()
    {
        if (ac.str == ActorController.State.normalAtk)
        {
            //Debug.Log(ac.str);
            ac.Select(ac.str);
            //Debug.Log(ac.isDam);
            if (ac.isDam)
            {
                shakeCamera = true;
                Stop(0.15f);
            }
        }
    }

    public void Stop(float duration)
    {
        if (waiting)
        {
            return;
        }

        Time.timeScale = 0.1f;

        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        waiting = false;
        shakeCamera = false;
    }
}
