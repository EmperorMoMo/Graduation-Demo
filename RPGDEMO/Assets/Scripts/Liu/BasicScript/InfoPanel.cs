using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {
    private static Transform Panel;
    private static Transform Head;
    private static Transform Describe;
    private static Transform LvLimit;
    private static Transform Attribute;
    private static Transform Effect;
    private static Transform Price;
    private static Transform Carry;
    public void Start() {
        Panel = this.transform;
        Head = this.transform.GetChild(0);
        Describe = this.transform.GetChild(2);
        LvLimit = this.transform.GetChild(3);
        Attribute = this.transform.GetChild(4);
        Effect = this.transform.GetChild(5);
        Price = this.transform.GetChild(6);
        Carry = this.transform.GetChild(7);
    }

    public static void ShowEquipmentInfo(EquipmentBase equipment) {
        BuildItemBase(equipment);
        Head.GetChild(3).GetComponent<Text>().text = GetPosition(equipment.Position);
        LvLimit.GetComponent<Text>().text = "等级：Lv" + equipment.LvLimit + "以上";
        Attribute.GetComponent<Text>().text = GetAttribute(equipment.Attr);
        ShowPanel();
    }

    public static void ShowConsumInfo(ConsumBase consum) {
        BuildItemBase(consum);
        Head.GetChild(3).GetComponent<Text>().text = "药剂";
        if (consum.Duration == 0) {
            LvLimit.GetComponent<Text>().text = "即时回复";
            Attribute.GetComponent<Text>().text = "使用后立即回复" + consum.ReValue + "点" + consum.ConType;
        } else {
            LvLimit.GetComponent<Text>().text = "持续回复";
            Attribute.GetComponent<Text>().text = "使用后每秒持续回复" + consum.ReValue + "点" + consum.ConType + "持续" + consum.Duration + "秒";
        }
        ShowPanel();
    }

    public static void ShowMatertia(ItemBase materia) {
        BuildItemBase(materia);
        Head.GetChild(3).GetComponent<Text>().text = "材料";
        LvLimit.GetComponent<Text>().text = "";
        Attribute.GetComponent<Text>().text = "";
        ShowPanel();
    }

    private static void ShowPanel() {
        Panel.SetParent(UIManager.Canvas.transform);
        Panel.position = Input.mousePosition;
        Panel.GetComponent<CanvasGroup>().alpha = 1;
        if (1f - Panel.GetComponent<CanvasGroup>().alpha > 0.001f) {
            Panel.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(Panel.GetComponent<CanvasGroup>().alpha, 1f, 0.2f);
        }
    }

    public static void HidePanel() {
        Panel.GetComponent<CanvasGroup>().alpha = 0;
    }
    private static void BuildItemBase<T>(T itembase) where T : ItemBase {
        string color = GetColor(itembase.Quality);
        Head.GetChild(0).GetComponent<Image>().sprite = itembase.Sprite;
        Head.GetChild(1).GetComponent<Text>().text = " <color=" + color + ">" + itembase.Name + "</color>";
        Head.GetChild(2).GetComponent<Text>().text = " <color=" + color + ">" + GetQuality(itembase.Quality) + "</color>";
        Describe.GetComponent<Text>().text = "<color=" + color + ">" + itembase.Describe + "</color>";
        Price.GetComponent<Text>().text = "<color=" + color + ">售价:" + itembase.Price + "</color>";
        Carry.GetComponent<Text>().text = "负重：0.81kg";
    }

    private static string GetColor(int quality) { 
        switch (quality) {
            case 0: return "#FFFFFF";
            case 1: return "#2DBF25";
            case 2: return "#0056FF";
            case 3: return "#B500FF";
            case 4: return "#FF2100";
            default: return "#FFFFFF";
        }
    }
    private static string GetPosition(int position) { 
        switch (position) {
            case 0: return " 武器";
            case 1: return " 头盔";
            case 2: return " 胸甲";
            case 3: return " 护肩";
            case 4: return " 手套";
            case 5: return " 腰带";
            case 6: return " 鞋子";
            case 7: return " 项链";
            case 8: return " 戒指";
            default: return "#FFFFFF";
        }
    }

    private static string GetQuality(int quality) {
        switch (quality) {
            case 0: return "新手";
            case 1: return "高级";
            case 2: return "稀有";
            case 3: return "神器";
            case 4: return "史诗";
            default: return "一般";
        }
    }

    private static string GetAttribute(BaseAttribute attr) {
        string str = "";
        if (attr.HP != 0) {
            str += "\n生命值 +" + attr.HP;
        }
        if (attr.MP != 0) {
            str += "\n魔法值 +" + attr.MP;
        }
        if (attr.ReHP != 0) {
            str += "\n生命回复 +" + attr.ReHP;
        }
        if (attr.ReMP != 0) {
            str += "\n魔法回复 +" + attr.ReMP;
        }
        if (attr.Aggressivity != 0) {
            str += "\n攻击力 +" + attr.Aggressivity;
        }
        if (attr.Armor != 0) {
            str += "\n护甲值 +" + attr.Armor;
        }
        if (attr.Strength != 0) {
            str += "\n力量 +" + attr.Strength;
        }
        if (attr.Intellect != 0) {
            str += "\n智力 +" + attr.Intellect;
        }
        if(attr.Agile != 0){
            str += "\n敏捷 +" + attr.Agile; 
        }
        return str + "\n";
    }
}
