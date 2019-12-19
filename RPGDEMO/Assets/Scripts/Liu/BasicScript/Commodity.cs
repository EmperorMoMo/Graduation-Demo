using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Commodity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public ItemBase itemBase;               //物品基础类对象
    private bool isEnter = false;
    private bool isShow = false;
    private float stayTime = 0.5f;
    private float stayTimer = 0;
    public void Start() {
        
    }

    public void Update() {
        if (isEnter) {
            if (!isShow) {
                stayTimer += Time.deltaTime;
                if (stayTimer > stayTime) {
                    if (itemBase.UID / 1000 == 1) {
                        InfoPanel.ShowEquipmentInfo((EquipmentBase)itemBase);
                    }
                    if (itemBase.UID / 1000 == 2) {
                        InfoPanel.ShowConsumInfo((ConsumBase)itemBase);
                    }
                    isShow = true;
                    stayTimer = 0;
                }
            }

            if (Input.GetMouseButtonUp(1)) {
                IASManager.Shop(itemBase);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        isEnter = false;
        InfoPanel.HidePanel();
        isShow = false;
    }
}
