using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Slider : MonoBehaviour {
    private BossAttribute2 bossAttr;

    private Image image;
    private Text TextVale;
    private Text TextOutline;

    private float curHP = 0;
    private float HP = 0;
    public void Start() {
        bossAttr = GameObject.Find("MOUNTAIN_DRAGON_LEGACY").GetComponent<BossAttribute2>();

        image = this.transform.GetChild(0).GetComponent<Image>();
        TextVale = this.transform.GetChild(1).GetComponent<Text>();
        TextOutline = this.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        image.fillAmount = 0;
    }

    public void Update() {
        curHP = Mathf.Lerp(curHP, bossAttr.HP, 2f);
        HP = Mathf.Lerp(HP, bossAttr.MAX_HP, 2f);
        image.fillAmount = Mathf.Lerp(image.fillAmount, curHP / bossAttr.MAX_HP, 0.2f);
        TextVale.text = TextOutline.text = curHP + " / " + HP;
        if (bossAttr.HP <= 0) {
            Destroy(this.gameObject, 0.5f);
        }
    }
}
