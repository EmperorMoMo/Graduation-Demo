﻿using System.Collections;
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
    private static bool isNone = true;
    private static bool isCtrl = false;

    private static List<GameObject> PanelList = new List<GameObject>();        //存储UI面板List

    private static GameObject GetPanel(string panelName) {
        switch (panelName) {
            case "equip": return EquipmentPanel;
            case "backpage": return Backpage;
            case "attr": return AttributePanel;
            default: return null;
        }
    }
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
            isNone = false;
        }

        if (Input.GetKeyUp(KeyCode.Tab)) {
            CloseFuncationMenu();
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            if (!EquipmentPanel.activeSelf) {
                ShowPanel(EquipmentPanel);
            } else {
                ClosePanel(EquipmentPanel);
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            if (!AttributePanel.activeSelf) {
                ShowPanel(AttributePanel);
            } else {
                ClosePanel(AttributePanel);
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            if (!Backpage.activeSelf) {
                ShowPanel(Backpage);
            } else {
                ClosePanel(Backpage);
            }
        }


        if (Input.GetKeyUp(KeyCode.Tab)) {
            CloseFuncationMenu();
            isNone = true;
        }

        if (Input.GetKeyUp(KeyCode.F1)) {
            if (PanelList.Count != 0) {
                ClosePanel(PanelList[PanelList.Count - 1]);
            }
        }

        if (isNone) {
            if (PanelList.Count == 0) {
                    if (isCtrl) {
                    MainCamera.hide_Cursor();
                    isCtrl = false;
                    Debug.Log("归还控制");
                }
            }
        }
        if (PanelList.Count != 0) {
            if (!isCtrl) {
                MainCamera.show_Cursor();
                isCtrl = true;
                Debug.Log("取得控制");
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

    public void PreShowPanel(string panelName) {
        ShowPanel(GetPanel(panelName));
    }
    public static void ShowPanel(GameObject Panel) {
        Panel.SetActive(true);
        PanelList.Add(Panel);
        CloseFuncationMenu();
    }

    public static void ClosePanel(GameObject Panel) {
        Panel.SetActive(false);
        PanelList.Remove(EquipmentPanel);
    }
}
