using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadSlider : MonoBehaviour {
    CharacterAttribute player;
    ActorController ac;
    Slider HPSlider;
    Slider MPSlider;
    Slider EnergySlider;
    Text HPText;
    Text MPText;
    Text HPTextOutline;
    Text MPTextOutline;
    Text Level;
    float tarHPValue;
    float tarMPValue;
    float tarEnValue;
    public void Start() {
        player = GameObject.Find("PlayerHandle").GetComponent<CharacterAttribute>();
        ac = GameObject.Find("PlayerHandle").GetComponent<ActorController >();
        HPSlider = this.transform.GetChild(0).GetComponent<Slider>();
        MPSlider = this.transform.GetChild(1).GetComponent<Slider>();
        EnergySlider = this.transform.GetChild(2).GetComponent<Slider>();
        Level = this.transform.GetChild(4).GetComponent<Text>();
        HPText = this.transform.GetChild(0).GetComponentInChildren<Text>();
        MPText = this.transform.GetChild(1).GetComponentInChildren<Text>();
        HPTextOutline = this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        MPTextOutline = this.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
        HPSlider.value = 0;
        MPSlider.value = 0;
        EnergySlider.value = 0;
        HPText.text = HPTextOutline.text = player.Cur_HP + " / " + player.finalAttribute.HP;
        MPText.text = MPTextOutline.text = player.Cur_MP + " / " + player.finalAttribute.MP;
    }
    public void Update() {
        tarHPValue = player.Cur_HP / player.finalAttribute.HP;
        tarMPValue = player.Cur_MP / player.finalAttribute.MP;
        tarEnValue = ac.nengliang / 100;
        HPSlider.value = Mathf.Lerp(HPSlider.value, tarHPValue, 0.08f);
        MPSlider.value = Mathf.Lerp(MPSlider.value, tarMPValue, 0.08f);
        EnergySlider.value = Mathf.Lerp(EnergySlider.value, tarEnValue, 0.08f);
        HPText.text = HPTextOutline.text = player.Cur_HP.ToString("f0") + " / " + player.finalAttribute.HP.ToString("f0");
        MPText.text = MPTextOutline.text = player.Cur_MP.ToString("f0") + " / " + player.finalAttribute.MP.ToString("f0");
        Level.text = "Lv." + player.Level;
    }
}
