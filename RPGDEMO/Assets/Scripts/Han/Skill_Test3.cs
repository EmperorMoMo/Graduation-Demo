using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Test3 : MonoBehaviour
{
    private ActorController ac;

    private EnemyAttribute ai;
    private BossAttribute ba;
    private BossAttribute2 ba2;
    private EnemyAttribute1 ai1;
    private EnemyAttribute2 ai2;

    private CharacterAttribute ca;
    
    float timeTemp = 0f;
    float time = 0;

    private bool canStop;

    private bool waiting;

    public GameObject Effect;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GameObject.Find("PlayerHandle").GetComponent<ActorController>();
        ca = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
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
                    timeTemp += Time.fixedDeltaTime;
                    ai = cols[i].GetComponent<EnemyAttribute>();
                    Rigidbody rig = cols[i].GetComponent<Rigidbody>();
                    if (time < 1.8f)
                    {
                        //cols[i].transform.position = new Vector3(cols[i].transform.position.x, 1.75f, cols[i].transform.position.z);
                        cols[i].transform.position = Vector3.Lerp(cols[i].transform.position, transform.position,
                            Time.fixedDeltaTime * 1.25f);
                        rig.constraints = RigidbodyConstraints.FreezePositionY;

                    }
                    else
                    {
                        rig.constraints = ~RigidbodyConstraints.FreezePosition;
                    }

                    if (timeTemp >= 0.2f)
                    {
                        ai.Enemy_Attacked(ca.finalAttribute.Aggressivity * 0.3f);
                        timeTemp = 0;
                        if (canStop)
                        {
                            Stop(0.3f);
                        }
                    }
                }

                if (cols[i].tag == "Boss")
                {
                    timeTemp += Time.fixedDeltaTime;
                    ba = cols[i].GetComponent<BossAttribute>();
                    if (timeTemp >= 0.2f)
                    {
                        ba.Boss_Attacked(ca.finalAttribute.Aggressivity * 0.3f);
                        timeTemp = 0;
                        if (canStop)
                        {
                            Stop(0.3f);
                        }
                    }
                }

                if (cols[i].tag == "Boss2")
                {
                    timeTemp += Time.fixedDeltaTime;
                    ba2 = cols[i].GetComponent<BossAttribute2>();
                    if (timeTemp >= 0.2f)
                    {
                        ba2.Boss_Attacked(ca.finalAttribute.Aggressivity * 0.3f);
                        timeTemp = 0;
                        if (canStop)
                        {
                            Stop(0.3f);
                        }
                    }
                }

                if (cols[i].tag == "Enemy1")
                {
                    timeTemp += Time.fixedDeltaTime;
                    ai1 = cols[i].GetComponent<EnemyAttribute1>();
                    Rigidbody rig = cols[i].GetComponent<Rigidbody>();
                    if (time < 1.8f)
                    {
                        //cols[i].transform.position = new Vector3(cols[i].transform.position.x, 1.75f, cols[i].transform.position.z);
                        cols[i].transform.position = Vector3.Lerp(cols[i].transform.position, transform.position,
                            Time.fixedDeltaTime * 1.25f);
                        rig.constraints = RigidbodyConstraints.FreezePositionY;

                    }
                    else
                    {
                        rig.constraints = ~RigidbodyConstraints.FreezePosition;
                    }

                    if (timeTemp >= 0.2f)
                    {
                        ai1.Enemy_Attacked1(ca.finalAttribute.Aggressivity * 0.3f);
                        timeTemp = 0;
                        if (canStop)
                        {
                            Stop(0.3f);
                        }
                    }
                }

                if (cols[i].tag == "Enemy2")
                {
                    timeTemp += Time.fixedDeltaTime;
                    ai2 = cols[i].GetComponent<EnemyAttribute2>();
                    Rigidbody rig = cols[i].GetComponent<Rigidbody>();
                    if (time < 1.8f)
                    {
                        //cols[i].transform.position = new Vector3(cols[i].transform.position.x, 1.75f, cols[i].transform.position.z);
                        cols[i].transform.position = Vector3.Lerp(cols[i].transform.position, transform.position,
                            Time.fixedDeltaTime * 1.25f);
                        rig.constraints = RigidbodyConstraints.FreezePositionY;

                    }
                    else
                    {
                        rig.constraints = ~RigidbodyConstraints.FreezePosition;
                    }

                    if (timeTemp >= 0.2f)
                    {
                        ai2.Enemy_Attacked2(ca.finalAttribute.Aggressivity * 0.3f);
                        timeTemp = 0;
                        if (canStop)
                        {
                            Stop(0.3f);
                        }
                    }
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
