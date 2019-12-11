using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Test3 : MonoBehaviour
{
    private ActorController ac;

    private EnemyAI ai;
    
    float timeTemp = 0f;
    float time = 0;

    private bool canStop;

    private bool waiting;

    public GameObject Effect;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GameObject.Find("PlayerHandle").GetComponent<ActorController>();
        canStop = true;
        
        var instance = Instantiate(Effect, transform.position+Vector3.up, transform.rotation);
        Destroy(instance,0.25f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        Collider[] cols = Physics.OverlapSphere(transform.position, 4f);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Enemy")
                {
                    ai = cols[i].GetComponent<EnemyAI>();
                    Rigidbody rig = cols[i].GetComponent<Rigidbody>();
                    if (time < 1.8f)
                    {
                        cols[i].transform.position = new Vector3(cols[i].transform.position.x, 1.75f, cols[i].transform.position.z);
                        rig.constraints = RigidbodyConstraints.FreezePositionY;

                    }
                    else
                    {
                        rig.constraints = ~RigidbodyConstraints.FreezePosition;
                    }
                    if (timeTemp >= 0.15f)
                    {
                        //cols[i].transform.position += Vector3.up * 0.5f;
                        if (canStop)
                        {
                            Stop(0.3f);
                        }
                        ai.Damage();
                        timeTemp = 0;
                    }

                    timeTemp += Time.fixedDeltaTime;
                }
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
        canStop = false;
    }
}
