using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events_ : MonoBehaviour
{
    private GameObject playerHandle;

    public Material glowMaterial;
    // Start is called before the first frame update
    void Awake()
    {
        playerHandle=GameObject.Find("PlayerHandle");
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

}
