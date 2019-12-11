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
    private bool isEnter = false;

    public void OnPointerEnter(PointerEventData eventData) {
        isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        isEnter = false;   
    }
}
