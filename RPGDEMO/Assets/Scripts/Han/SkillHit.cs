using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHit : MonoBehaviour
{
    private ActorController ac;

    private bool waiting;
    // Start is called before the first frame update
    void Awake()
    {
        ac = this.GetComponentInParent<ActorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Skill_One()
    {
        if (ac.str == ActorController.State.skill_One)
        {
            ac.Select(ac.str);
            if (ac.isDam)
            {
                Stop(0.08f);
            }
        }
    }

    public void Stop(float duration)
    {
        if (waiting)
        {
            return;
        }

        Time.timeScale = 0.0f;

        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        waiting = false;
    }
}
