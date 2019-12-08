using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour {
    public GameObject SlotPrefab;
    public GameObject ItemPrefab;
    void Start() {
        CreateSlot();
        CreateItem(0);
        CreateItem(1);
    }

    private void CreateSlot(){
        for (int i = 0; i < 80; i++) {
            GameObject slot = GameObject.Instantiate(SlotPrefab, GameObject.Find("BackpgGrid").transform);
            slot.name = "Slot" + i;
            slot.GetComponent<Slot>().SetSlotIndex(i);
            DataManager.SlotGOList[i] = slot;
        }
    }

    private void CreateItem(int slotIndex) {
        int listIndex = FetchNull();
        if (listIndex == -1) {
            return;
        }
        GameObject item = GameObject.Instantiate(ItemPrefab, GameObject.Find("Slot" + slotIndex).transform);
        item.AddComponent<Item>().itemBase = new ItemBase();
        item.GetComponent<Item>().SlotIndex = slotIndex;
        DataManager.SlotGOList[slotIndex].GetComponent<Slot>().ListIndex = slotIndex;
        DataManager.ItemGOList[listIndex] = item;
    }

    private int FetchNull() {
        for (int i = 0; i < 80; i++) {
            if (DataManager.ItemGOList[i] == null) {
                return i;
            }
        }
        return -1;
    }
}
