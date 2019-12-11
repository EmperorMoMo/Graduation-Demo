using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventEffects : MonoBehaviour {
    //public GameObject EffectPrefab;
    //public Transform EffectStartPosition;
    //public float DestroyAfter = 10;
    //[Space]
    //public GameObject EffectPrefabWorldSpace;
    //public Transform EffectStartPositionWorld;
    //public float DestroyAfterWorld = 10;
    private bool waiting;

    public EffectInfo[] Effects;

    [System.Serializable]

    public class EffectInfo
    {
        public GameObject Effect;
        public Transform StartPositionRotation;
        public float DestroyAfter = 10;
        public bool UseLocalPosition = true;
        
    }

    //   // Update is called once per frame
    //   void CreateEffect () {
    //       var effectOBJ = Instantiate(EffectPrefab, EffectStartPosition);
    //       effectOBJ.transform.localPosition = Vector3.zero;
    //       Destroy(effectOBJ, DestroyAfter);        		
    //}

    //   void CreateEffectWorldSpace()
    //   {
    //       var effectOBJ = Instantiate(EffectPrefabWorldSpace, EffectStartPositionWorld.transform.position, EffectStartPositionWorld.transform.rotation);

    //       Destroy(effectOBJ, DestroyAfterWorld);
    //   }
    void Awake()
    {

    }
            
    void InstantiateEffect(int EffectNumber)
    {
        if(Effects == null || Effects.Length <= EffectNumber)
        {
            Debug.LogError("Incorrect effect number or effect is null");
        }

        if (EffectNumber == 6)
        {
            Stop(0.6f,0.45f);
        }

        var instance = Instantiate(Effects[EffectNumber].Effect, Effects[EffectNumber].StartPositionRotation.position, Effects[EffectNumber].StartPositionRotation.rotation);

        if (Effects[EffectNumber].UseLocalPosition)
        {
            instance.transform.parent = Effects[EffectNumber].StartPositionRotation.transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion();
        }
        Destroy(instance, Effects[EffectNumber].DestroyAfter);


    }

    public void Stop(float duration,float p)
    {
        if (waiting)
        {
            return;
        }

        Time.timeScale = p;

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
