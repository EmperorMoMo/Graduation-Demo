using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHP : MonoBehaviour {
    public GameObject BarPrefab;

    private GameObject ShowHPPanel;
    private Image Bar;
    private Text text;

    private EnemyAttribute Enemy;
    private float HP;
    private float MaxHP;

    void Start() {
        Enemy = this.transform.GetComponent<EnemyAttribute>();

        ShowHPPanel = GameObject.Instantiate(BarPrefab, this.transform);
        Bar = ShowHPPanel.transform.GetChild(1).GetComponent<Image>();
        text = ShowHPPanel.transform.GetChild(2).GetComponent<Text>();
        Vector3 position = new Vector3(transform.position.x, (int)(transform.position.y + 3), transform.position.z);
        ShowHPPanel.transform.position = position;

        MaxHP = Enemy.MAX_HP;
    }
    void Update() {
        this.transform.GetComponentsInChildren<Canvas>()[0].transform.rotation = Camera.main.transform.rotation;
        HP = Enemy.HP;
        Bar.fillAmount = HP / Enemy.MAX_HP;
        text.text = (int)HP + " / " + (int)MaxHP;
    }
}
