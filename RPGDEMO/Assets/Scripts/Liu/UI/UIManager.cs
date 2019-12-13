using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI向导类
/// 管理所有的UI界面，提供UI面板GameObject的访问
/// 控制所有UI界面显示隐藏等
/// </summary>
public class UIManager : MonoBehaviour {
    public static GameObject Canvas;            //UI画布
    public static GameObject Head;              //头像血条
    public static GameObject MiniMap;           //小地图
    public static GameObject QuickBar;          //快捷栏
    public static GameObject EquipmentPanel;    //装备面板
    public static GameObject Backpage;          //背包
    public static GameObject Shoppage;          //商店
    public static GameObject AttributePanel;    //属性面板
    public static GameObject SwitchRole;        //切换人物
    public static GameObject FuncationMenu;     //功能菜单
    public static GameObject DialogBox;         //对话框

    private static CameraController MainCamera;       //主相机控制脚本
    private static bool isNone = false;
    private static bool isCtrl = false;

    private static Stack<GameObject> UIStack = new Stack<GameObject>();        //存储UI面板的栈

    void Awake() {
        Canvas = GameObject.Find("Canvas");
        Head = Canvas.transform.GetChild(0).gameObject;
        MiniMap = Canvas.transform.GetChild(1).gameObject;
        QuickBar = Canvas.transform.GetChild(2).gameObject; ;
        EquipmentPanel = Canvas.transform.GetChild(3).gameObject; ;
        Backpage = Canvas.transform.GetChild(4).gameObject;
        Shoppage = Canvas.transform.GetChild(5).gameObject;
        AttributePanel = Canvas.transform.GetChild(6).gameObject;
        SwitchRole = Canvas.transform.GetChild(7).gameObject;
        FuncationMenu = Canvas.transform.GetChild(8).gameObject;
        DialogBox = Canvas.transform.GetChild(9).gameObject;

        MainCamera = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            ShowFuncationMenu();
        }

        if (Input.GetKeyUp(KeyCode.Tab)) {
            CloseFuncationMenu();
        }

        if (Input.GetKeyUp(KeyCode.F1)) {
            UIStack.Pop().SetActive(false);
        }

        if (isNone) {
            if (UIStack.Count == 0) {
                    if (isCtrl) {
                    Debug.Log("归还控制");
                    MainCamera.hide_Cursor();
                    isCtrl = false;
                }
            }
        }
    }

    public static void ShowFuncationMenu() {
        if (!FuncationMenu.activeSelf) {
            FuncationMenu.SetActive(true);
            isNone = false;
            MainCamera.show_Cursor();
            if (!isCtrl) {
                isCtrl = true;
                Debug.Log("取得控制");
            }
        }
    }

    public static void CloseFuncationMenu() {
        if (FuncationMenu.activeSelf) {
            FuncationMenu.SetActive(false);
            isNone = true;
        }
    }

    public static void ShowPanel() { 
        
    }

    //public void ShowBackpage(string str) {
    //    GameObject Panel = Backpage;
    //    Panel.SetActive(true);
    //    UIStack.Push(Backpage);
    //    CloseFuncationMenu();
    //}

    //public static void ShowEquip() {
    //    EquipmentPanel.SetActive(true);
    //    UIStack.Push(EquipmentPanel);
    //    CloseFuncationMenu();
    //}
}
