using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC02 : MonoBehaviour
{
    private GameObject player;
    private Canvas canvas;
    private CharacterAttribute ca;
    private CameraController cc;
    public float distance = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("TestPlayer");
        canvas = this.transform.GetChild(2).GetComponent<Canvas>();
        ca = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        cc= GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
    public void huifu()
    {
        if (ca.Gold >= 500)
        {
            ca.Cur_HP = ca.finalAttribute.HP;
            ca.Gold -= 500;
            canvas.gameObject.SetActive(false);
            cc.hide_Cursor();
        }
        else
        {
            TipFrame.NoMoney();
        }
    }
    public void guihuan()
    {
        canvas.gameObject.SetActive(false);
        cc.hide_Cursor();
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                canvas.gameObject.SetActive(true);
                cc.show_Cursor();
            }
        }
    }
}
