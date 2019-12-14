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
    public static Slot[] SlotArr = new Slot[99];                    //存放所有的Slot脚本
    public static Item[] ItemArr = new Item[99];                    //存放当前分类的Item脚本
    public static List<int[]> ItemFile = new List<int[]>();           //物品存档
    public static Equipment[] EquipmentArr = new Equipment[80];     //存放所有的Equipment脚本

    public static Dictionary<int, EquipmentBase> EquipmentDic = new Dictionary<int, EquipmentBase>();      //装备词典

    public JsonData EquipmentJData;     //装备数据的Json数据文件对象


    public void Start() {
        EquipmentJData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/DataSource/EquipmentData.json"));    //将Json文件转化为对象
        ContructEquipemnt();
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
        Item item;
        for (int i = 0; i < 99; i++) {
            item = ItemArr[i];
            if (item != null) {
                ItemFile.Add(new int[3]{item.itemBase.UID, item.SlotIndex, item.curStack});
            }
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
