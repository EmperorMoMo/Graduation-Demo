using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sale : MonoBehaviour, IDropHandler  {
    private CharacterAttribute CA;
    public void Start() {
        CA = UIManager.PlayerHandle.GetComponent<CharacterAttribute>();
    }

    // Update is called once per frame
    public void Update() {
        
    }

     public void OnDrop(PointerEventData eventData) {
         Item dragItem = eventData.pointerDrag.GetComponent<Item>();     //拖拽体的Item脚本
         if (dragItem != null) {
             CA.Gold += (dragItem.itemBase.Price / 2) * dragItem.curStack; 
             DataManager.ItemArr[dragItem.SlotIndex] = null;
             Destroy(dragItem.gameObject);
         }
     }
}
