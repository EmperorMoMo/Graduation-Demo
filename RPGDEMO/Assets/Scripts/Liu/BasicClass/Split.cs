using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Split : MonoBehaviour {
    public static Slot Slot;
    public static Item Item;
    public static bool ReOpen = false;
    public int Count = 1;

    public string tiptext;
    private Text TipText;
    void Start() {
        TipText = this.transform.GetComponentInChildren<Text>();
    }
    void Update() {
        tiptext = "拆分物品：" + Item.itemBase.Name + " 数量 * " + Count.ToString();
        TipText.text = tiptext;
        if (ReOpen) {
            SetCount(1);
            ReOpen = false;
        }
    }
    public void DeSplit() {
        Debug.Log("拆分");
        IASManager.SplitItem(Item, Slot, Count);
        GameObject.Find("Split").SetActive(false);
    }

    public void Cancel() {
        GameObject.Find("Split").SetActive(false);
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
        Count = Mathf.Clamp(count, 1, Item.curStack - 1);
        this.transform.GetComponent<InputField>().text = Count.ToString();
    }

    public static void SetValue(Item item, Slot slot) {
        Item = item;
        Slot = slot;
        ReOpen = true;
    }
}
