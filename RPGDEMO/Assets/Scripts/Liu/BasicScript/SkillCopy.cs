using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillCopy : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public int SkillID;
    public float CDTime;
    public float CDTimer;
    public Skill skill;
    public Image Cover;

    private ActorController ac;

    public void Start() {
        ac = UIManager.PlayerHandle.GetComponent<ActorController>();
    }

    public void Update() {
        CDTime = GetCDTime(SkillID);
        if (CDTime == 0) {
            Cover.fillAmount = 0;
        } else {
            Cover.fillAmount = 1 - CDTime / CDTimer;
        }
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

    private float GetCDTime(int skillID) {
        switch (skillID) {
            case 1200:
                return ac.CD_skill_one;
            case 1201:
                return ac.CD_skill_two;
            case 1202:
                return ac.CD_skill_three;
            case 1100:
                return ac.CD_skill_four;
            default:
                return 0;
        }
    }
}
