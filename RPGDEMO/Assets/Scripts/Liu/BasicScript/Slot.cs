using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 网格类
/// 挂载在物体上
/// </summary>
public class Slot : MonoBehaviour, IDropHandler {
    public int Index = -1;              //该网格的指向的网格及物品索引

    public int QuickBarID = -1;

    //鼠标在该网格上落下
    public void OnDrop(PointerEventData eventData) {
        Equipment equipment = eventData.pointerDrag.GetComponent<Equipment>();
        if (Index >= 90 && Index < 99) {
            if (equipment != null) {
                if (equipment.equipemtnBase.Position == Index - 90) {
                    Debug.Log("装备");
                    IASManager.Equip(equipment);
                }
            }
            return;
        }

        Skill skill = eventData.pointerDrag.GetComponent<Skill>();
        SkillCopy skillcopy = eventData.pointerDrag.GetComponent<SkillCopy>();
        if (Index >= 80 && Index < 90) {
            //该格不存在物体
            if (QuickBarID == -1) {
                if (skill != null) {
                    skill.Homing();
                    skill.ToEmpty(Index);
                }
                if (skillcopy != null) {
                    skillcopy.skill.Homing();
                    skillcopy.skill.ToEmpty(Index);
                }
            //该格存在物体
            } else {
                //从技能栏来
                if (skill != null) {
                    skill.ExChange(DataManager.SkillDic[QuickBarID], Index);
                }
                if (skillcopy != null) {
                    skillcopy.skill.ExChange(DataManager.SkillDic[QuickBarID], Index);
                }
            }
            return;
        }

        Item dragItem = eventData.pointerDrag.GetComponent<Item>();     //拖拽体的Item脚本

        //如果并未移动到其他网格
        if (dragItem.SlotIndex == Index) {
            Debug.Log("未动");
            return;
        }

        //鼠标未拖拽任何物体
        if (dragItem == null) {
            Debug.Log("无物");
            return;
        }

        //该网格指向的Arr上不存在物品
        if (DataManager.ItemArr[Index] == null) {
            //如果按住ctrl键,拆分
            if (Input.GetKey(KeyCode.LeftControl) && dragItem.curStack > 1) {
                Debug.Log("拟拆分");
                Split.SetValue(dragItem, this);
                UIManager.Backpage.transform.GetChild(3).gameObject.SetActive(true);
            } else { 
            //放置在空位
                Debug.Log("置空");
                IASManager.ToEmpty(dragItem, this);
            }
        } else { 
        //该网格上存在物品
            //如果是相同物品，则进行堆叠操作
            if (dragItem.itemBase.UID == DataManager.ItemArr[Index].itemBase.UID) {
                Debug.Log("堆叠");
                IASManager.Stack(dragItem, this);
            } else {
                //否则进行换位操作
                Debug.Log("换位");
                IASManager.Exchange(dragItem, this);
            }
        }
    }
}
