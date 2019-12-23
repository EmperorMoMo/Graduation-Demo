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
    public static int[] WeaponFile = new int[80];               //商品存档
    public static int[] ArmorFile = new int[80];
    public static int[] ConsuFile = new int[80];
    public static int[] MatsFile = new int[80];
    public static int[] ScrollFile = new int[80];
    public static int[] QuickFile = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

    public static Slot[] SlotArr = new Slot[99];                    //存放所有的Slot脚本
    public static Item[] ItemArr = new Item[99];                    //存放所有的Item脚本
    public static Transform[] shopTra = new Transform[64];

    public static Dictionary<int, Skill> SkillDic = new Dictionary<int, Skill>();

    public static Equipment[] EquipmentArr = new Equipment[80];     //存放所有的Equipment脚本
    public static Consum[] ConsumArr = new Consum[80];              //存放所有的Consum脚本

    public static Dictionary<int, EquipmentBase> EquipmentDic = new Dictionary<int, EquipmentBase>();       //装备词典
    public static Dictionary<int, ConsumBase> ConsumDic = new Dictionary<int, ConsumBase>();                //消耗品词典
    public static Dictionary<int, ItemBase> MaterialDic = new Dictionary<int, ItemBase>();
    public static Dictionary<int, ScrollBase> ScrollDic = new Dictionary<int, ScrollBase>();

    public JsonData EquipmentJData;     //装备数据的Json数据文件对象
    public JsonData ConsumJData;        //消耗品数据的Json数据文件对象
    public JsonData MaterialJData;       //材料数据的Json数据文件对象
    public JsonData ScrollJData;       //材料数据的Json数据文件对象

    public void Start() {
        StreamReader streamreader;
        streamreader = new StreamReader(Application.dataPath + "/StreamingAssets/EquipmentData.json");
        EquipmentJData = JsonMapper.ToObject(streamreader);    //将Json文件转化为对象
        streamreader = new StreamReader(Application.dataPath + "/StreamingAssets/ConsumData.json");
        ConsumJData = JsonMapper.ToObject(streamreader);          //将Json文件转化为对象
        streamreader = new StreamReader(Application.dataPath + "/StreamingAssets/MaterialData.json");
        MaterialJData = JsonMapper.ToObject(streamreader);          //将Json文件转化为对象
        streamreader = new StreamReader(Application.dataPath + "/StreamingAssets/ScrollData.json");
        ScrollJData = JsonMapper.ToObject(streamreader);

        SkillDic.Add(1200, null);
        SkillDic.Add(1201, null);
        SkillDic.Add(1202, null);
        SkillDic.Add(1100, null);

        ContructEquipemnt();
        ContructConsum();
        ContructMaterial();
        ContructScroll();

        WeaponFile[0] = 1000;
        WeaponFile[1] = 1100;

        ArmorFile[0] = 1001;
        ArmorFile[1] = 1002;
        ArmorFile[2] = 1003;
        ArmorFile[3] = 1004;
        ArmorFile[4] = 1005;
        ArmorFile[5] = 1006;
        ArmorFile[6] = 1007;
        ArmorFile[7] = 1008;


        ArmorFile[8] = 1101;
        ArmorFile[9] = 1102;
        ArmorFile[10] = 1103;
        ArmorFile[11] = 1104;
        ArmorFile[12] = 1105;
        ArmorFile[13] = 1106;
        ArmorFile[14] = 1107;
        ArmorFile[15] = 1108;

        ConsuFile[0] = 2000;
        ConsuFile[1] = 2001;
        ConsuFile[2] = 2002;
        ConsuFile[3] = 2003;

        MatsFile[0] = 3000;
        MatsFile[1] = 3001;
        MatsFile[2] = 3100;
        MatsFile[3] = 3101;
        MatsFile[4] = 3103;
        MatsFile[5] = 3104;
        MatsFile[6] = 3105;
        MatsFile[7] = 3200;
        MatsFile[8] = 3201;
        MatsFile[8] = 3202;
        MatsFile[9] = 3203;
        MatsFile[10] = 3204;
        MatsFile[11] = 3300;
        MatsFile[12] = 3301;

        ScrollFile[0] = 4200;
        ScrollFile[1] = 4201;
        ScrollFile[2] = 4202;
        ScrollFile[3] = 4203;
        ScrollFile[4] = 4204;
        ScrollFile[5] = 4205;
        ScrollFile[6] = 4206;
        ScrollFile[7] = 4207;
        ScrollFile[8] = 4208;

        ScrollFile[9] = 4300;
        ScrollFile[10] = 4301;
        ScrollFile[11] = 4302;
        ScrollFile[12] = 4303;
        ScrollFile[13] = 4304;
        ScrollFile[14] = 4305;
        ScrollFile[15] = 4306;
        ScrollFile[16] = 4307;
        ScrollFile[17] = 4308;
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

    private void ContructMaterial() {
        int uid;
        string name;
        int quality;
        int price;
        string describe;
        string sprite;
        int stackMax;

        for (int i = 0; i < MaterialJData.Count; i++) {
            uid = (int)MaterialJData[i]["UID"];
            name = MaterialJData[i]["Name"].ToString();
            quality = (int)MaterialJData[i]["Quality"];
            price = (int)MaterialJData[i]["Price"];
            describe = MaterialJData[i]["Describe"].ToString();
            sprite = MaterialJData[i]["Sprite"].ToString();
            stackMax = (int)MaterialJData[i]["StackMax"];
            ItemBase material = new ItemBase(uid, name, quality, price, stackMax, describe);
            material.Sprite = Resources.Load<Sprite>("Material/" + sprite);
            MaterialDic.Add(uid, material);
        }
    }

    private void ContructScroll() {
        int uid;
        string name;
        int quality;
        int price;
        int tarUID;
        List<int[]> mats;
        string describe;
        string sprite;
        int stackMax;

        for (int i = 0; i < ScrollJData.Count; i++) {
            uid = (int)ScrollJData[i]["UID"];
            name = ScrollJData[i]["Name"].ToString();
            quality = (int)ScrollJData[i]["Quality"];
            price = (int)ScrollJData[i]["Price"];
            tarUID = (int)ScrollJData[i]["TarUID"];
            mats = ContructMats(i);
            describe = ScrollJData[i]["Describe"].ToString();
            sprite = ScrollJData[i]["Sprite"].ToString();
            stackMax = (int)ScrollJData[i]["StackMax"];

            ScrollBase scroll = new ScrollBase(uid, name, quality, price, stackMax, describe, sprite, tarUID, mats);
            ScrollDic.Add(uid, scroll);
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

    private List<int[]> ContructMats(int index) {
        JsonData Mats = ScrollJData[index]["Mats"];
        List<int[]> mats = new List<int[]>();
        for (int i = 0; i < Mats.Count; i++) { 
            mats.Add(new int[2]{(int)Mats[i][0], (int)Mats[i][1]});
            //Debug.Log((int)Mats[i][0] + "//" + (int)Mats[i][1]);
        }
        return mats;
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
