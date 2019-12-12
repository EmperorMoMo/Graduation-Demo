using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 创建向导
/// 负责创建物品网格等
/// </summary>
public class CreateManager : MonoBehaviour {
    public GameObject SlotPrefab;
    public GameObject ItemPrefab;

    private FetchUtils FU = new FetchUtils();

    private bool isInit = false;
    void Start() {
        
    }

    void Update() {
        if (!isInit) {
            if (GameObject.Find("BackpgGrid") != null) {
                Init();
            }
        }
    }

    void Init() {
        CreateSlot();
        CreateItem(1001);
        CreateItem(1002);
        CreateItem(1003);
        CreateItem(1004);
        CreateItem(1005);
        isInit = true;
    }

    private void CreateSlot(){
        for (int i = 0; i < 80; i++) {
            GameObject slot = GameObject.Instantiate(SlotPrefab, GameObject.Find("BackpgGrid").transform);
            slot.name = "Slot" + i;
            slot.GetComponent<Slot>().SetSlotIndex(i);
            DataManager.SlotGOList[i] = slot;
        }
    }

    public void CreateItem(int uid) {
        int index = FU.FetchEmpty();
        if (index == -1) {
            return;
        }
        GameObject item = GameObject.Instantiate(ItemPrefab, GameObject.Find("Slot" + index).transform);
        Equipment equipment = item.AddComponent<Equipment>();
        equipment.itemBase = FU.FetchEquipmentsBase(uid);
        equipment.SlotIndex = index;
        equipment.ShowCount();
        item.GetComponent<Image>().sprite = FU.FetchEquipmentsBase(uid).sprite;
        DataManager.SlotGOList[index].GetComponent<Slot>().ListIndex = index;
        DataManager.ItemGOList[index] = item;
    }
}
