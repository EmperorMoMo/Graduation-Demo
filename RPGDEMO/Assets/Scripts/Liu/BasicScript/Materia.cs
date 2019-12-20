using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 装备类
/// 挂载在装备上
/// </summary>
public class Materia : Item, IPointerEnterHandler, IPointerExitHandler {
    private bool isEnter = false;
    private bool isShow = false;
    private float stayTime = 0.5f;
    private float stayTimer = 0;
    private Vector3 mousePosition;

    public void Start() {
    }
    public void Update() {
        if (isEnter) {
            if (!isShow) {
                stayTimer += Time.deltaTime;
                if (stayTimer > stayTime) {
                    InfoPanel.ShowMatertia(itemBase);
                    mousePosition = Input.mousePosition;
                    isShow = true;
                    stayTimer = 0;
                }
            }
        }

        if (isShow) {
            if (mousePosition != Input.mousePosition) {
                InfoPanel.HidePanel();
                isShow = false;
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
