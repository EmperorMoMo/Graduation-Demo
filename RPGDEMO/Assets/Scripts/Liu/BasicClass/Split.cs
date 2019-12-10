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
        tiptext = "物品UID：" + Item.itemBase.UID + "数量 * " + Count.ToString();
        TipText.text = tiptext;
        if (ReOpen) {
            SetCount(1);
            ReOpen = false;
        }
    }
    public void DeSplit() {
        Slot.SplitItem(Item, Count);
        GameObject.Find("Split").SetActive(false);
    }

    public void Cancel() {
        GameObject.Find("Split").SetActive(false);
    }

    public void AddCount() {
        Debug.Log("Add");
        SetCount(++Count);
    }

    public void DecCount() {
        Debug.Log("Dec");
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
