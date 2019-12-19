using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI向导类
/// 管理所有的UI界面，提供UI面板GameObject的访问
/// 控制所有UI界面显示隐藏等
/// </summary>
public class UIManager : MonoBehaviour {
    public static GameObject PlayerHandle;      //角色
    public static GameObject Canvas;            //UI画布
    public static GameObject Head;              //头像血条
    public static GameObject MiniMap;           //小地图
    public static GameObject QuickBar;          //快捷栏
    public static GameObject EquipmentPanel;    //装备面板
    public static GameObject Backpage;          //背包
    public static GameObject Shoppage;          //商店
    public static GameObject AttributePanel;    //属性面板
    public static GameObject SkillPanel;        //技能面板
    public static GameObject SwitchRole;        //切换人物
    public static GameObject FuncationMenu;     //功能菜单
    public static GameObject DialogBox;         //对话框

    private static CameraController MainCamera;         //主相机控制脚本
    private static bool isNone = true;                  //非持久化界面是否为空
    private static bool isCtrl = false;                 //是否取得控制

    private static List<GameObject> PanelList = new List<GameObject>();        //存储UI面板的List

    void Awake() {
        PlayerHandle = GameObject.Find("PlayerHandle");
        Canvas = GameObject.Find("Canvas");
        Head = Canvas.transform.GetChild(0).gameObject;
        MiniMap = Canvas.transform.GetChild(1).gameObject;
        QuickBar = Canvas.transform.GetChild(2).gameObject; ;
        EquipmentPanel = Canvas.transform.GetChild(3).gameObject; ;
        Backpage = Canvas.transform.GetChild(4).gameObject;
        Shoppage = Canvas.transform.GetChild(5).gameObject;
        AttributePanel = Canvas.transform.GetChild(6).gameObject;
        SkillPanel = Canvas.transform.GetChild(7).gameObject;
        SwitchRole = Canvas.transform.GetChild(8).gameObject;
        FuncationMenu = Canvas.transform.GetChild(9).gameObject;
        DialogBox = Canvas.transform.GetChild(10).gameObject;

        MainCamera = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            ShowFuncationMenu();
        }

        if (Input.GetKeyUp(KeyCode.Tab)) {
            CloseFuncationMenu();
        }

        if (Input.GetKeyUp(KeyCode.I)) {
            if (!EquipmentPanel.activeSelf) {
                ShowPanel(EquipmentPanel);
            } else {
                ClosePanel(EquipmentPanel);
            }
        }

        if (Input.GetKeyUp(KeyCode.P)) {
            if (!AttributePanel.activeSelf) {
                ShowPanel(AttributePanel);
            } else {
                ClosePanel(AttributePanel);
            }
        }

        if (Input.GetKeyUp(KeyCode.B)) {
            if (!Backpage.activeSelf) {
                ShowPanel(Backpage);
            } else {
                ClosePanel(Backpage);
            }
        }

        if (Input.GetKeyUp(KeyCode.K)) {
            if (!SkillPanel.activeSelf) {
                ShowPanel(SkillPanel);
            } else {
                ClosePanel(SkillPanel);
            }
        }

        if (Input.GetKeyUp(KeyCode.O)) {
            if (!Shoppage.activeSelf) {
                ShowPanel(Shoppage);
            } else {
                ClosePanel(Shoppage);
            }
        }

        if (Input.GetKeyUp(KeyCode.F1)) {
            if (PanelList.Count != 0) {
                ClosePanel(PanelList[PanelList.Count - 1]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SetSkill(DataManager.SlotArr[80].QuickBarID, true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            SetSkill(DataManager.SlotArr[80].QuickBarID, false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SetSkill(DataManager.SlotArr[81].QuickBarID, true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2)) {
            SetSkill(DataManager.SlotArr[81].QuickBarID, false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SetSkill(DataManager.SlotArr[82].QuickBarID, true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3)) {
            SetSkill(DataManager.SlotArr[82].QuickBarID, false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SetSkill(DataManager.SlotArr[83].QuickBarID, true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha4)) {
            SetSkill(DataManager.SlotArr[83].QuickBarID, false);
        }

        if (Input.GetKeyUp(KeyCode.Alpha5)) {
            if (DataManager.SlotArr[84].QuickBarID != -1) {
                DataManager.SlotArr[84].transform.GetChild(1).GetComponent<ConsumCopy>().UseConsum();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha6)) {
            if (DataManager.SlotArr[85].QuickBarID != -1) {
                DataManager.SlotArr[85].transform.GetChild(1).GetComponent<ConsumCopy>().UseConsum();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha7)) {
            if (DataManager.SlotArr[86].QuickBarID != -1) {
                DataManager.SlotArr[86].transform.GetChild(1).GetComponent<ConsumCopy>().UseConsum();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha8)) {
            if (DataManager.SlotArr[87].QuickBarID != -1) {
                DataManager.SlotArr[87].transform.GetChild(1).GetComponent<ConsumCopy>().UseConsum();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha9)) {
            if (DataManager.SlotArr[84].QuickBarID != -1) { }
            DataManager.SlotArr[88].transform.GetChild(1).GetComponent<ConsumCopy>().UseConsum();
        }

        if (Input.GetKeyUp(KeyCode.Alpha0)) {
            if (DataManager.SlotArr[84].QuickBarID != -1) { }
            DataManager.SlotArr[89].transform.GetChild(1).GetComponent<ConsumCopy>().UseConsum();
        }

        if (isNone) {
            if (PanelList.Count == 0) {
                    if (isCtrl) {
                        MainCamera.hide_Cursor();
                        isCtrl = false;
                }
            }
        }

        if (PanelList.Count != 0) {
            if (!isCtrl) {
                MainCamera.show_Cursor();
                isCtrl = true;
            }
        }

        
    }

    //显示功能菜单
    public static void ShowFuncationMenu() {
        if (!FuncationMenu.activeSelf) {
            FuncationMenu.SetActive(true);
            isNone = false;
            if (!isCtrl) {
                MainCamera.show_Cursor();
                isCtrl = true;
                Debug.Log("取得控制");
            }
        }
    }

    //关闭功能菜单
    public static void CloseFuncationMenu() {
        if (FuncationMenu.activeSelf) {
            FuncationMenu.SetActive(false);
            isNone = true;
        }
    }

    //显示前操作
    public void PreShowPanel(string panelName) {
        ShowPanel(GetPanel(panelName));
    }

    //显示当前面板
    public static void ShowPanel(GameObject Panel) {
        if (Panel == AttributePanel) {
            AttrPanel.isReOpen = true;
        }
        Panel.SetActive(true);
        PanelList.Add(Panel);
        CloseFuncationMenu();
    }

    //关闭当前面板
    public void ClosePanel(GameObject Panel) {
        Panel.SetActive(false);
        PanelList.Remove(Panel);
    }

    //通过名字取得对应面板
    private static GameObject GetPanel(string panelName) {
        switch (panelName) {
            case "head": return Head;
            case "minimap": return MiniMap;
            case "quickbar": return QuickBar;
            case "equip": return EquipmentPanel;
            case "backpage": return Backpage;
            case "shoppage": return Shoppage;
            case "attr": return AttributePanel;
            case "dialog": return DialogBox;
            default: return null;
        }
    }
    private static void SetSkill(int skillID, bool value) {
        switch (skillID) {
            case 1200:
                PlayerHandle.GetComponent<PlayerInput>().UI_skill_1 = value;
                break;
            case 1201:
                PlayerHandle.GetComponent<PlayerInput>().UI_skill_2 = value;
                break;
            case 1202: 
                PlayerHandle.GetComponent<PlayerInput>().UI_skill_3 = value;
                break;
            case 1100:
                PlayerHandle.GetComponent<PlayerInput>().increaseskill_1 = value;
                break;
            case -1:
                break;
        }
    }
}
