using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using LitJson;

/// <summary>
/// 数据类
/// 读取Json数据文件，存储物品，网格信息等，方便索引查找，做持久化操作等
/// </summary>
public class DataManager : MonoBehaviour
{
    public static List<int[]> ItemFile = new List<int[]>();         //物品存档
    public static int[] ShopFile = new int[80];                     //商品存档
    public static int[] QuickFile = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

    public static Slot[] SlotArr = new Slot[99];                    //存放所有的Slot脚本
    public static Item[] ItemArr = new Item[99];                    //存放所有的Item脚本
    public static Transform[] shopTra = new Transform[64];

    public static Dictionary<int, Skill> SkillDic = new Dictionary<int, Skill>();

    public static Equipment[] EquipmentArr = new Equipment[80];     //存放所有的Equipment脚本
    public static Consum[] ConsumArr = new Consum[80];              //存放所有的Consum脚本

    public static Dictionary<int, EquipmentBase> EquipmentDic = new Dictionary<int, EquipmentBase>();       //装备词典
    public static Dictionary<int, ConsumBase> ConsumDic = new Dictionary<int, ConsumBase>();                //消耗品词典

    public JsonData EquipmentJData;     //装备数据的Json数据文件对象
    public JsonData ConsumJData;        //消耗品数据的Json数据文件对象

    public void Start() {
        EquipmentJData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/DataSource/EquipmentData.json"));    //将Json文件转化为对象
        ConsumJData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/DataSource/ConsumData.json"));          //将Json文件转化为对象

        SkillDic.Add(1200, null);
        SkillDic.Add(1201, null);
        SkillDic.Add(1202, null);
        SkillDic.Add(1100, null);

        ContructEquipemnt();
        ContructConsum();

        ShopFile[0] = 1000;
        ShopFile[1] = 1001;
        ShopFile[2] = 1002;
        ShopFile[3] = 1003;
        ShopFile[4] = 1004;
        ShopFile[5] = 1005;
        ShopFile[6] = 1006;
        ShopFile[7] = 1007;
        ShopFile[8] = 1008;

        ShopFile[9] = 1100;
        ShopFile[10] = 1101;
        ShopFile[11] = 1102;
        ShopFile[12] = 1103;
        ShopFile[13] = 1104;
        ShopFile[14] = 1105;
        ShopFile[15] = 1106;
        ShopFile[16] = 1107;
        ShopFile[17] = 1108;

        ShopFile[18] = 1200;
        ShopFile[19] = 1201;
        ShopFile[20] = 1202;
        ShopFile[21] = 1203;
        ShopFile[22] = 1204;
        ShopFile[23] = 1205;
        ShopFile[24] = 1206;
        ShopFile[25] = 1207;
        ShopFile[26] = 1208;

        ShopFile[27] = 1300;
        ShopFile[28] = 1301;
        ShopFile[29] = 1302;
        ShopFile[30] = 1303;
        ShopFile[31] = 1304;
        ShopFile[32] = 1305;
        ShopFile[33] = 1306;
        ShopFile[34] = 1307;
        ShopFile[35] = 1308;


        ShopFile[36] = 2000;
        ShopFile[37] = 2001;
        ShopFile[38] = 2002;
        ShopFile[39] = 2003;
    }

    private void ContructEquipemnt() { 
        int uid;
        string name;
        int quality;
        int price;
        int position;
        BaseAttribute attr;
        int lvlimit;
        string describe;
        string sprite;
        int stackMax; 

        for (int i = 0; i < EquipmentJData.Count; i++) {
            uid = (int)EquipmentJData[i]["UID"];
            name = EquipmentJData[i]["Name"].ToString();
            quality = (int)EquipmentJData[i]["Quality"];
            price = (int)EquipmentJData[i]["Price"];
            position = (int)EquipmentJData[i]["Position"];
            attr = ContructAttribute(i);
            lvlimit = (int)EquipmentJData[i]["LvLimit"];
            describe = EquipmentJData[i]["Describe"].ToString();
            sprite = EquipmentJData[i]["Sprite"].ToString();
            stackMax = (int)EquipmentJData[i]["StackMax"];

            EquipmentBase equipment = new EquipmentBase(uid, name, quality, price, stackMax, describe, sprite, position, attr, lvlimit);
            EquipmentDic.Add(uid, equipment);
        }
    }

    private void ContructConsum() {
        int uid;
        string name;
        int quality;
        int price;
        string conType;
        int reValue;
        int duration;
        string describe;
        string sprite;
        int stackMax;

        for (int i = 0; i < ConsumJData.Count; i++) {
            uid = (int)ConsumJData[i]["UID"];
            name = ConsumJData[i]["Name"].ToString();
            quality = (int)ConsumJData[i]["Quality"];
            price = (int)ConsumJData[i]["Price"];
            conType = ConsumJData[i]["ConType"].ToString();
            reValue = (int)ConsumJData[i]["ReValue"];
            duration = (int)ConsumJData[i]["Duration"];
            describe = ConsumJData[i]["Describe"].ToString();
            sprite = ConsumJData[i]["Sprite"].ToString();
            stackMax = (int)ConsumJData[i]["StackMax"];
            ConsumBase consum = new ConsumBase(uid, name, quality, price, stackMax, describe, sprite, conType, reValue, duration);
            ConsumDic.Add(uid, consum);
        }
    }

    private BaseAttribute ContructAttribute(int index) {
        JsonData Attr = EquipmentJData[index]["Attribute"];

        int hp = (int)Attr[0];
        int mp = (int)Attr[1];
        int rehp = (int)Attr[2];
        int remp = (int)Attr[3];
        int aggr = (int)Attr[4];
        int armo = (int)Attr[5];
        int stre = (int)Attr[6];
        int inte = (int)Attr[7];
        int agil = (int)Attr[8];

        BaseAttribute attr = new BaseAttribute(hp, mp, rehp, remp, aggr, armo, stre, inte, agil);

        return attr;
    }

    public static void SaveItem() {
        //Debug.Log("Saving...");
        Item item;
        ItemFile.Clear();
        for (int i = 0; i < 99; i++) {
            item = ItemArr[i];
            if (item != null) {
                //Debug.Log("Save one");
                ItemFile.Add(new int[3]{item.itemBase.UID, item.SlotIndex, item.curStack});
            }
        }
    }

    public static void SaveQuick() {
        for (int i = 0; i < 10; i++) {
            QuickFile[i] = -1;
        }
        for (int i = 0; i < 10; i++) {
            QuickFile[i] = SlotArr[80 + i].QuickBarID;
        }
    }

    public static string ShowFile() {
        string str = "物品";
        foreach (int[] i in ItemFile) {
            foreach(int j in i){
                str += j + "/";
            }
            str += "  //  ";
        }
        return str;
    }
}
