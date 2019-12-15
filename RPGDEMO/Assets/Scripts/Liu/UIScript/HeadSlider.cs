using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadSlider : MonoBehaviour {
    CharacterAttribute player;
    Slider HPSlider;
    Slider MPSlider;
    Text HPText;
    Text MPText;
    Text HPTextOutline;
    Text MPTextOutline;
    Text Level;
    float tarHPValue;
    float tarMPValue;
    public void Start() {
        player = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        HPSlider = this.transform.GetChild(0).GetComponent<Slider>();
        MPSlider = this.transform.GetChild(1).GetComponent<Slider>();
        Level = this.transform.GetChild(3).GetComponent<Text>();
        HPText = this.transform.GetChild(0).GetComponentInChildren<Text>();
        MPText = this.transform.GetChild(1).GetComponentInChildren<Text>();
        HPTextOutline = this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        MPTextOutline = this.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
        HPSlider.value = 0;
        MPSlider.value = 0;
        HPText.text = HPTextOutline.text = player.Cur_HP + " / " + player.finalAttribute.HP;
        MPText.text = MPTextOutline.text = player.Cur_MP + " / " + player.finalAttribute.MP;
    }
    public void Update() {
        tarHPValue = player.Cur_HP / player.finalAttribute.HP;
        tarMPValue = player.Cur_MP / player.finalAttribute.MP;
        HPSlider.value = Mathf.Lerp(HPSlider.value, tarHPValue, 0.08f);
        MPSlider.value = Mathf.Lerp(MPSlider.value, tarMPValue, 0.08f);
        HPText.text = HPTextOutline.text = player.Cur_HP.ToString("f0") + " / " + player.finalAttribute.HP.ToString("f0");
        MPText.text = MPTextOutline.text = player.Cur_MP.ToString("f0") + " / " + player.finalAttribute.MP.ToString("f0");
        Level.text = "Lv." + player.Level;
    }
}
