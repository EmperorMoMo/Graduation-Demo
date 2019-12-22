using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 消耗品类
/// 挂载在消耗品上
/// </summary>
public class ConsumCopy : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public int ConsumUID;
    public int Count;
    public Consum consum;
    public int QuickIndex = -1;

    private Image fillImage;
    private Text countText;
    private string ConType;

    public void Start() {
        consum = FetchUtils.FetchLastConsum(ConsumUID, out Count);
        fillImage = this.transform.GetChild(1).GetComponent<Image>();
        countText = this.transform.GetChild(0).GetComponent<Text>();
        ConType = consum.consumBase.ConType;
    }

    public void Update() {
        if (Input.GetMouseButtonUp(1)) {
            
        }
        countText.text = Count.ToString();
    }
    public void FixedUpdate() {
        if (string.Equals(consum.consumBase.ConType, "HP")) {
            fillImage.fillAmount = IASManager.HPConTime / consum.ConsuTimer;
        }
        if (string.Equals(consum.consumBase.ConType, "MP")) {
            fillImage.fillAmount = IASManager.MPConTime / consum.ConsuTimer;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        //鼠标左键按住
        if (Input.GetMouseButton(0)) {
            this.transform.SetParent(UIManager.Canvas.transform);       //将物品移至Canvas下
            this.transform.position = eventData.position;               //设置为鼠标位置
            this.transform.GetComponent<Image>().raycastTarget = false; //关闭当前组件中Image的射线检测
        }
    }

    //在拖拽中
    public void OnDrag(PointerEventData eventData) {
        //鼠标左键按住
        if (Input.GetMouseButton(0)) {
            this.transform.position = eventData.position;
        }
    }

    //结束拖拽
    public void OnEndDrag(PointerEventData eventData) {
        //鼠标左键弹起
        if (Input.GetMouseButtonUp(0)) {
            this.transform.GetComponent<Image>().raycastTarget = true;                  //开启当前组件中Image的射线检测
        }
    }



    public void ToEmpty(int Index) {
        DataManager.SlotArr[QuickIndex].QuickBarID = -1;
        QuickIndex = Index;
        DataManager.SlotArr[Index].QuickBarID = ConsumUID;
        this.transform.SetParent(DataManager.SlotArr[Index].transform);
        this.transform.position = this.transform.parent.position;
    }

    public void ExChange(ConsumCopy curCopy, int slotIndex) {

        curCopy.ToEmpty(QuickIndex);
        ToEmpty(slotIndex);
    }

    public void UseConsum() {
        if ((string.Equals(consum.consumBase.ConType, "HP") && IASManager.HPConTime > 0)) {
            return;
        }
        if ((string.Equals(consum.consumBase.ConType, "MP") && IASManager.MPConTime > 0)) {
            return;
        }
        bool isUpdata = false;
        if (consum.curStack == 1) {
            isUpdata = true;
        }
        Count--;
        IASManager.Consu(consum);
        if (isUpdata) {
            consum = FetchUtils.FetchLastConsum(ConsumUID, out Count);
        }
    }
}
