using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttrPanel : MonoBehaviour {
    private GameObject _AttrPanel;
    private CharacterAttribute player;

    private Transform Stats;
    private Transform Level;
    private Transform HPBar;
    private Transform MPBar;
    private Transform AttrValue;

    private Text lvText;
    private Image expBar;
    private Text expPer;
    private Text expPro;
    private Text HPText;
    private Image HPBarValue;
    private Text MPText;
    private Image MPBarValue;
    private Text AttrAmount;

    private float curlv;
    private float curExp;
    private float curCurHP;
    private float curCurMP;
    private float curHP;
    private float curMP;
    private float curAgg;
    private float curArm;
    private float curStre;
    private float curAgil;
    private float curInte;

    private float tarlv;
    private float tarExp;
    private float tarCurHP;
    private float tarCurMP;
    private float tarHP;
    private float tarMP;
    private float tarAgg;
    private float tarArm;
    private float tarStre;
    private float tarAgil;
    private float tarInte;

    public static bool isReOpen = true;

    public void Start() {
        _AttrPanel = UIManager.AttributePanel;
        player = UIManager.PlayerHandle.GetComponent<CharacterAttribute>();

        Stats = _AttrPanel.transform.GetChild(1);
        Level = Stats.GetChild(0);
        HPBar = Stats.GetChild(1);
        MPBar = Stats.GetChild(2);
        AttrValue = Stats.GetChild(3);

        lvText = Level.GetChild(0).GetComponent<Text>();
        expBar = Level.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        expPer = Level.GetChild(1).GetChild(1).GetComponent<Text>();
        expPro = Level.GetChild(2).GetComponent<Text>();
        HPText = HPBar.GetChild(0).GetComponent<Text>();
        HPBarValue = HPBar.GetChild(1).GetComponent<Image>();
        MPText = MPBar.GetChild(0).GetComponent<Text>();
        MPBarValue = MPBar.GetChild(1).GetComponent<Image>();
        AttrAmount = AttrValue.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update(){
        tarlv = player.Level;
        tarExp = player.Exp;
        tarCurHP = player.Cur_HP;
        tarCurMP = player.Cur_MP;
        tarHP = player.finalAttribute.HP;
        tarMP = player.finalAttribute.MP;
        tarAgg = player.finalAttribute.Aggressivity;
        tarArm = player.finalAttribute.Armor;
        tarStre = player.finalAttribute.Strength;
        tarAgil = player.finalAttribute.Agile;
        tarInte = player.finalAttribute.Intellect;

        if (isReOpen) {
            ToZero();
            isReOpen = false;
        } else {
            lvText.text = "Lv: <color=#00B4FF>" + (int)curlv + "</color>";
            expBar.fillAmount = 1 - (curExp / (tarlv * 200));
            expPer.text = (int)curExp + " / " + (int)curlv * 200;
            expPro.text = (int)(curExp / (tarlv * 2)) + "%";
            HPText.text = (int)curCurHP + " / " + (int)curHP;
            HPBarValue.fillAmount = curCurHP / tarHP;
            MPText.text = (int)curCurMP + " / " + (int)curMP;
            MPBarValue.fillAmount = curCurMP / tarMP;
            AttrAmount.text = (int)curHP + "\n" + (int)curMP + "\n\n"
                + (int)curAgg + "\n" + (int)curArm + "\n\n"
                + (int)curStre + "\n" + (int)curInte + "\n" + (int)curAgil;
        }

        curlv = MyLerp(curlv, tarlv, 1);
        curExp = MyLerp(curExp, tarExp, 2);
        curCurHP = MyLerp(curCurHP, tarCurHP, 2);
        curCurMP = MyLerp(curCurMP, tarCurMP, 2);
        curHP = MyLerp(curHP, tarHP, 4);
        curMP = MyLerp(curMP, tarMP, 4);
        curAgg = MyLerp(curAgg, tarAgg, 2);
        curArm = MyLerp(curArm, tarArm, 2);
        curStre = MyLerp(curStre, tarStre, 2);
        curAgil = MyLerp(curAgil, tarAgil, 2);
        curInte = MyLerp(curInte, tarInte, 2);
    }

    private void ToZero(){
        curlv = 0;
        curExp = 0;
        curCurHP = 0;
        curCurMP = 0;
        curHP = 0;
        curMP = 0;
        curAgg = 0;
        curArm = 0;
        curStre = 0;
        curAgil = 0;
        curInte = 0;
    }

    private float MyLerp(float value, float tarValue, float k) {
        value = Mathf.Lerp(value, tarValue, k * 0.02f);
        if ((tarValue - value) < k) {
            value = tarValue;
        }
        return value;
    }
}
