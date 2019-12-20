using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 装备类
/// 挂载在装备上
/// </summary>
public class Scroll : Item, IPointerEnterHandler, IPointerExitHandler {
    public ScrollBase ScrollBase;
    private bool isEnter = false;
    private bool isShow = false;
    private float stayTime = 0.5f;
    private float stayTimer = 0;

    public List<string[]> MatsCount = new List<string[]>();
    private bool isMake = true;

    public void Start() {
        ScrollBase = (ScrollBase)itemBase;
    }
    public void Update() {
        if (isEnter) {
            judge();
            if (!isShow) {
                stayTimer += Time.deltaTime;
                if (stayTimer > stayTime) {
                    Debug.Log("Show");
                    InfoPanel.ShowScroll(ScrollBase, MatsCount);
                    isShow = true;
                    stayTimer = 0;
                }
            }

            if (Input.GetMouseButtonUp(1)) {
                if (isMake) {
                    DataManager.ItemArr[SlotIndex] = null;
                    Destroy(this.gameObject);
                    IASManager.Make(ScrollBase);
                } else {
                    Debug.Log("不可合成");
                }
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

    public void judge() {
        MatsCount.Clear();
        isMake = true;
        foreach (int[] i in ScrollBase.Mats) {
            string name = FetchUtils.FetchMaterial(i[0]).Name;
            int curCount = FetchUtils.FetchMatCount(i[0]);
            int Count = i[1];

            MatsCount.Add(new string[3] { name, curCount.ToString(), Count.ToString() });
            isMake = isMake && (curCount >= Count);
        }
    }
}
