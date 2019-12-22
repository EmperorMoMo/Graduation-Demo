using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    public static Commodity Item;
    public static bool ReOpen = false;
    public int Count = 1;

    public string tiptext;
    private Text TipText;

    private CharacterAttribute ca;
    void Start() {
        ca = UIManager.PlayerHandle.GetComponent<CharacterAttribute>();
        TipText = this.transform.GetComponentInChildren<Text>();
    }
    void Update() {
        tiptext = "购买物品：" + Item.itemBase.Name + " 数量 * " + Count.ToString();
        TipText.text = tiptext;
        if (ReOpen) {
            SetCount(1);
            ReOpen = false;
        }
    }
    public void DeShop() {
        Debug.Log("购买");
        if (Item.itemBase.Price <= ca.Gold * Count)
        {
            ca.Gold -= Item.itemBase.Price * Count;
            IASManager.Shop(Item.itemBase, Count);
        } else {
            TipFrame.NoMoney();
        }
        GameObject.Find("Shop").SetActive(false);
    }

    public void Cancel() {
        GameObject.Find("Shop").SetActive(false);
    }

    public void AddCount() {
        SetCount(++Count);
    }

    public void DecCount() {
        SetCount(--Count);
    }

    public void ChangeCount() {
        if (this.transform.GetComponent<InputField>().text != "") {
            SetCount(Convert.ToInt32(this.transform.GetComponent<InputField>().text));
        }
    }

    private void SetCount(int count) {
        Count = Mathf.Clamp(count, 1, 99);
        this.transform.GetComponent<InputField>().text = Count.ToString();
    }

    public static void SetValue(Commodity item) {
        Item = item;
        ReOpen = true;
    }
}
