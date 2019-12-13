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

    private static GameObject MainCamera;       //主相机

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
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (!FuncationMenu.activeSelf) {
                FuncationMenu.SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab)) {
            if (FuncationMenu.activeSelf) {
                FuncationMenu.SetActive(false);
            }
        }
    }
}
