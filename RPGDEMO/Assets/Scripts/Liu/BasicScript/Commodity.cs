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
    private Vector3 mousePosition;

    private CharacterAttribute ca;

    public void Start() {
        ca = UIManager.PlayerHandle.GetComponent<CharacterAttribute>();
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
                    if (itemBase.UID / 1000 == 3) {
                        InfoPanel.ShowMatertia(itemBase);
                    }
                    if (itemBase.UID / 1000 == 4) {
                        InfoPanel.ShowScroll((ScrollBase)itemBase);
                    }
                    mousePosition = Input.mousePosition;
                    isShow = true;
                    stayTimer = 0;
                }
            }

            if (Input.GetMouseButtonUp(1)) {
                if (Input.GetKey(KeyCode.LeftControl)){
                    Debug.Log("拟购买");
                    Shop.SetValue(this);
                    UIManager.Shoppage.transform.GetChild(3).gameObject.SetActive(true);
                    UIManager.Shoppage.transform.GetChild(3).position = Input.mousePosition;
                    return;
                }
                if (itemBase.Price <= ca.Gold)
                {
                    ca.Gold -= itemBase.Price;
                    IASManager.Shop(itemBase, 1);
                } else {
                    TipFrame.NoMoney();
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
    }
}
