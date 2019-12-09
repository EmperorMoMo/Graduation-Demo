using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 创建向导
/// 负责创建物品网格等
/// </summary>
public class CreateManager : MonoBehaviour {
    public GameObject SlotPrefab;
    public GameObject ItemPrefab;

    private FetchUtils FU = new FetchUtils();
    void Start() {
        CreateSlot();
        CreateItem(0, 101, 3);
        CreateItem(1, 101, 3);
        CreateItem(2, 101, 3);
        CreateItem(3, 101, 3);
        CreateItem(4, 101, 3);
    }

    private void CreateSlot(){
        for (int i = 0; i < 80; i++) {
            GameObject slot = GameObject.Instantiate(SlotPrefab, GameObject.Find("BackpgGrid").transform);
            slot.name = "Slot" + i;
            slot.GetComponent<Slot>().SetSlotIndex(i);
            DataManager.SlotGOList[i] = slot;
        }
    }

    public void CreateItem(int slotIndex, int uid, int stackMax) {
        int listIndex = FU.FetchEmpty();
        if (listIndex == -1) {
            return;
        }
        GameObject item = GameObject.Instantiate(ItemPrefab, GameObject.Find("Slot" + slotIndex).transform);
        item.AddComponent<Item>().itemBase = new ItemBase(uid, stackMax);
        item.GetComponent<Item>().SlotIndex = slotIndex;
        item.GetComponent<Item>().ShowCount();
        DataManager.SlotGOList[slotIndex].GetComponent<Slot>().ListIndex = slotIndex;
        DataManager.ItemGOList[slotIndex] = item;
    }
}
