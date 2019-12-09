using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Split : MonoBehaviour {
    public static Slot slot;
    public static Item item;
    public int Count = 0;

    private Text tipText;
    void Start() {
        tipText = this.transform.GetComponentInChildren<Text>();
        Debug.Log(tipText);

        tipText.text = "物品" + Count;
    }

    void Update() {

    }

    public void DeSplit() {
        SetCount(Convert.ToInt32(this.transform.GetComponent<Text>().text));
        slot.SplitItem(item, Count);
    }

    public void AddCount() {
        SetCount(Count++);
    }

    public void DecCount() {
        SetCount(Count--);
    }

    private void SetCount(int count) {
        Count = Mathf.Clamp(count, 1, item.curStack - 1);
    }
}
