using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC01 : MonoBehaviour
{
    private GameObject player;
    public float distance = 1;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("TestPlayer");
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!UIManager.Shoppage.activeSelf)
                {
                    UIManager.ShowPanel(UIManager.Shoppage);
                }
                else
                {
                    UIManager.ClosePanel(UIManager.Shoppage);
                }
            }
        }
    }
}
