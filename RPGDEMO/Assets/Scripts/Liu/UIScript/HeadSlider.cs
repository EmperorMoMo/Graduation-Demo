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
    float tarHPValue;
    float tarMPValue;
    public void Start() {
        player = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        HPSlider = this.transform.GetChild(0).GetComponent<Slider>();
        MPSlider = this.transform.GetChild(1).GetComponent<Slider>();
        HPText = this.transform.GetChild(0).GetComponentInChildren<Text>();
        MPText = this.transform.GetChild(1).GetComponentInChildren<Text>();
        HPTextOutline = this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        MPTextOutline = this.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();

        HPText.text = HPTextOutline.text = player.HP + " / " + player.MAX_HP;
        MPText.text = MPTextOutline.text = player.MP + " / " + player.MAX_MP;
    }
    public void Update() {
        tarHPValue = player.HP / player.MAX_HP;
        tarMPValue = player.MP / player.MAX_MP;
        HPSlider.value = Mathf.Lerp(HPSlider.value, tarHPValue, 0.08f);
        MPSlider.value = Mathf.Lerp(MPSlider.value, tarMPValue, 0.08f);
    }
}
