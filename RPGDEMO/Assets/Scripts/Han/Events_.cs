using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events_ : MonoBehaviour
{
    private GameObject playerHandle;

    public Material glowMaterial;

    private ActorController ac;

    private Animator anim;

    private bool waiting;

    public bool shakeCamera;
    // Start is called before the first frame update
    void Awake()
    {
        playerHandle=GameObject.Find("PlayerHandle");
        ac = this.transform.GetComponentInParent<ActorController>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void show()
    {
        GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
        Destroy(clone.GetComponent<Animator>());

        SkinnedMeshRenderer[] skinMeshList = clone.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var smr in skinMeshList)
        {
            //smr.enabled = false;
            smr.material = glowMaterial;
        }
        
        Destroy(clone,0.5f);
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

        Time.timeScale = 0.25f;

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

    public void ResetTrigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
    }
}
