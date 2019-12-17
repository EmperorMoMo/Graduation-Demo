using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillCopy : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Skill skill;
    public void Start() {
        skill = this.transform.parent.GetChild(2).GetComponent<Skill>();
    }

    public void Update() {
        
    }

    public void OnBeginDrag(PointerEventData eventData) {
        skill.OnBeginDrag(eventData);
        this.transform.GetChild(0).GetComponent<Image>().raycastTarget = false; //关闭当前组件中Image的射线检测
    }

    public void OnDrag(PointerEventData eventData) {
        skill.OnDrag(eventData);
    }

    //结束拖拽
    public void OnEndDrag(PointerEventData eventData) {
        skill.OnEndDrag(eventData);
        this.transform.GetChild(0).GetComponent<Image>().raycastTarget = true; //关闭当前组件中Image的射线检测
    }
}
