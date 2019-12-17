﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 消耗品类
/// 挂载在消耗品上
/// </summary>
public class Consum : Item, IPointerEnterHandler, IPointerExitHandler {
    public ConsumBase consumBase;
    private bool isEnter = false;
    private bool isShow = false;
    private float stayTime = 0.5f;
    private float stayTimer = 0;

    public float ConsuTimer = 22;
    public float ConsuTime = 0;

    private Image fillImage;
    public void Start() {
        consumBase = (ConsumBase)itemBase;
        fillImage = this.transform.GetChild(1).GetComponent<Image>();
    }

    public void Update() {
        if (isEnter) {
            if (!isShow) {
                stayTimer += Time.deltaTime;
                if (stayTimer > stayTime) {
                    InfoPanel.ShowConsumInfo(this.consumBase);
                    isShow = true;
                    stayTimer = 0;
                }
            }

            if (Input.GetMouseButtonUp(1)) {
                IASManager.Consu(this);
            }
        }
    }

    public void FixedUpdate() {
        if (ConsuTime > 0) {
            ConsuTime -= Time.fixedDeltaTime;
            fillImage.fillAmount = ConsuTime / ConsuTimer;
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
