using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Split : MonoBehaviour {
    public static Slot slot;
    public static Item item;
    public int Count = 1;

    public string tiptext;
    private Text TipText;
    void Start() {
        TipText = this.transform.GetComponentInChildren<Text>();
    }
    void Update() {
        tiptext = "物品UID：" + item.itemBase.UID + "数量 * " + Count.ToString();
        TipText.text = tiptext;
    }
    public void DeSplit() {
        slot.SplitItem(item, Count);
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
        Debug.Log("ChangeCount");
        if (this.transform.GetComponent<InputField>().text != "") {
            SetCount(Convert.ToInt32(this.transform.GetComponent<InputField>().text));
        }
    }

    private void SetCount(int count) {
        Count = Mathf.Clamp(count, 1, item.curStack - 1);
        this.transform.GetComponent<InputField>().text = Count.ToString();
    }
}
