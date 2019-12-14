using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 装备类
/// 挂载在装备上
/// </summary>
public class Equipment : Item, IPointerEnterHandler, IPointerExitHandler {
    public EquipmentBase equipemtnBase;
    private bool isEnter = false;
    private bool isShow = false;
    private float stayTime = 0.5f;
    private float stayTimer = 0;

    public void Start() {
        equipemtnBase = (EquipmentBase)itemBase;
    }
    public void Update() {
        if (isEnter) {
            if (!isShow) {
                stayTimer += Time.deltaTime;
                if (stayTimer > stayTime) {
                    InfoPanel.ShowEquipmentInfo(this.equipemtnBase);
                    isShow = true;
                    stayTimer = 0;
                }
            }

            if (Input.GetMouseButtonUp(1)) {
                IASManager.Equip(this);
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
