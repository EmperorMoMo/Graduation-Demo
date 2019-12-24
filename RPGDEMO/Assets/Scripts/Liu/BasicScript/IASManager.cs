using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品网格向导
/// 负责创建物品网格，移动物品，更改物品与网格的对应关系等
/// </summary>
public class IASManager : MonoBehaviour {
    public GameObject slotPrefab;
    public GameObject itemPrefab;
    private static GameObject SlotPrefab;       //生成网格的预制体
    private static GameObject ItemPrefab;       //生成物品的预制体

    private static List<int[]> thisFile = new List<int[]>();
    private static int[] quickFile = new int[10];

    public static float HPConTime = 0;
    public static float MPConTime = 0;

    public void Awake(){
        SlotPrefab = slotPrefab;
        ItemPrefab = itemPrefab;
    }
    IEnumerator wait() {
        yield return new WaitForSeconds(0.1f);
        ReadQuick();
        UIManager.Backpage.SetActive(false);
        UIManager.Shoppage.SetActive(false);

    }
    public void Start() {
        UIManager.Backpage.SetActive(true);
        CreateSlot();
        CreateQuickBarSlot();
        CreateEquipmentSlot();
        CreateShopSlot();

        for (int i = 0; i < 10; i++) {
            quickFile[i] = -1;
        }
        ReadData();
        StartCoroutine(wait());

    }
    public void Update() {

    }

    public void FixedUpdate() {
        if (HPConTime > 0) {
            HPConTime -= Time.fixedDeltaTime;
        }
        if (MPConTime > 0) {
            MPConTime -= Time.fixedDeltaTime;
        }
    }

    //创建背包网格
    private static void CreateSlot(){
        for (int i = 0; i < 80; i++) {
            GameObject slot = GameObject.Instantiate(SlotPrefab, UIManager.Backpage.transform.GetChild(1).GetChild(0));
            slot.name = "Slot" + i;                                 //GameObject名
            slot.AddComponent<Slot>().Index = i;                    //网格指向的ItemArr
            DataManager.SlotArr[i] = slot.GetComponent<Slot>();     //将Slot脚本存入数组

        }
    }

    //创建快捷栏网格
    private static void CreateQuickBarSlot() {
        Transform Grid = UIManager.QuickBar.transform.GetChild(1);
        for (int i = 80; i < 90; i++) {
            GameObject slot = Grid.GetChild(i - 80).gameObject;       //对应装备栏
            slot.AddComponent<Slot>().Index = i;                        //网格的索引
            DataManager.SlotArr[i] = slot.GetComponent<Slot>();         //将Slot脚本存入数组
        }
    }

    //创建装备栏网格
    private static void CreateEquipmentSlot() {
        Transform Equips = UIManager.EquipmentPanel.transform.GetChild(2).GetChild(0);
        for (int i = 90; i < 99; i++) {
            GameObject slot = Equips.GetChild(i - 90).gameObject;       //对应装备栏
            slot.AddComponent<Slot>().Index = i;                        //网格的索引
            DataManager.SlotArr[i] = slot.GetComponent<Slot>();         //将Slot脚本存入数组
        }
    }
    public static void CreateShopSlot() {
        Transform Shop = UIManager.Shoppage.transform.GetChild(1);
        for (int i = 0; i < 64; i++) {
            DataManager.shopTra[i] = Shop.GetChild(i);
        }
    }
    public static void ReadData() {
        foreach (int[] i in DataManager.ItemFile) {
            thisFile.Add(new int[3] { i[0], i[1], i[2] });
        }
        foreach (int[] i in thisFile) {
            CreateItem(i[0], i[1], i[2]);
        }
        thisFile.Clear();
    }

    public static void ReadQuick() {
        for (int i = 0; i < 10; i++) {
            quickFile[i] = DataManager.QuickFile[i];
        }
        for (int i = 0; i < 10; i++) {
            if (quickFile[i] != -1) {
                CreateQuick(quickFile[i], i);
            }
        }
        for (int i = 0; i < 10; i++) {
            quickFile[i] = -1;
        }
    }

    //创建物品
    public static void CreateItem(int uid) {
        
        CreateItem(uid, -1, 1);
    }

    public static void CreateItem(int uid, int Index, int CurStack) {

        int index = FetchUtils.FetchEmpty();
        if (index == -1) {
            Debug.Log("物品栏已满，无法生成");
        }
        GameObject Item;
        if (Index != -1) {
            Item = GameObject.Instantiate(ItemPrefab, DataManager.SlotArr[Index].transform);     //指定位置生成指定物品
        } else {
            Item = GameObject.Instantiate(ItemPrefab, DataManager.SlotArr[index].transform);
        }
        Item item;
        switch (uid / 1000) {
            case 1:
                item = Item.AddComponent<Equipment>();                     //挂载Equipment脚本
                item.itemBase = FetchUtils.FetchEquipmentsBase(uid);       //为其中的物品基础类赋值
                break;
            case 2:
                item = Item.AddComponent<Consum>();
                item.itemBase = FetchUtils.FetchConsumsBase(uid);
                item.transform.GetChild(1).GetComponent<Image>().sprite = item.itemBase.Sprite;
                break;
            case 3:
                item = Item.AddComponent<Materia>();
                item.itemBase = FetchUtils.FetchMaterial(uid);
                break;
            case 4:
                item = Item.AddComponent<Scroll>();
                item.itemBase = FetchUtils.FetchScrollBase(uid);
                break;
            default:
                item = null;
                break;
                
        }
        if (Index != -1) {
            item.SlotIndex = Index;                                //网格索引指向该网格
        } else {
            item.SlotIndex = index;
        }
        item.curStack = CurStack;

        Color newColor;
        ColorUtility.TryParseHtmlString(GetColor(item.itemBase.Quality), out newColor);
        Item.transform.GetChild(2).GetComponent<Image>().color = newColor;
        Item.GetComponent<Image>().sprite = item.itemBase.Sprite;  //显示贴图
        item.ShowCount();                                          //显示数量
        TipFrame.ShowTip(item.itemBase.Name, CurStack);
        if (Index == -1) {
            Item perItem;
            for (int i = 0; i < 80; i++) {
                perItem = DataManager.ItemArr[i];
                if (perItem != null) {
                    if (perItem.itemBase.UID == item.itemBase.UID && perItem.curStack < perItem.itemBase.StackMax) {
                        Stack(item, DataManager.SlotArr[i]);
                        return;
                    }
                }
            }
        }
        if (Index == -1) {
            DataManager.ItemArr[index] = item;                     //将Equipment脚本存入数组
        } else {
            DataManager.ItemArr[Index] = item;  
        }
        DataManager.SaveItem();


        
    }

    public static void CreateConsumCopy(int uid, int Index) {
        for (int i = 84; i < 90; i++) {
            if (DataManager.SlotArr[i].QuickBarID == uid) {
                Destroy(DataManager.SlotArr[i].transform.GetChild(1).gameObject);
                DataManager.SlotArr[i].QuickBarID = -1;
                break;
            }
        }
        GameObject consumCopy = GameObject.Instantiate(ItemPrefab, DataManager.SlotArr[Index].transform);     //指定位置生成指定物品
        consumCopy.GetComponent<Image>().sprite = consumCopy.transform.GetChild(1).GetComponent<Image>().sprite
            = FetchUtils.FetchConsumsBase(uid).Sprite;
        consumCopy.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
        consumCopy.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
        consumCopy.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
        ConsumCopy copy = consumCopy.AddComponent<ConsumCopy>();
        copy.ConsumUID = uid;
        copy.QuickIndex = Index;

        DataManager.SlotArr[Index].QuickBarID = uid;
        DataManager.SaveQuick();
    }

    public static void CreateQuick(int uid, int slotIndex) {
        int index = -1;
        if (slotIndex >= 0 && slotIndex < 4) {
            switch (uid) { 
                case 1200:
                    index = 0;
                    break;
                case 1201:
                    index = 1;
                    break;
                case 1202:
                    index = 2;
                    break;
                case 1100:
                    index = 3;
                    break;
            }
            Skill skill = UIManager.SkillPanel.transform.GetChild(1).GetChild(index).GetChild(2).GetComponent<Skill>();
            skill.ToEmpty(slotIndex + 80);
        }
        if (slotIndex >= 4 && slotIndex < 10) {
            CreateConsumCopy(uid, slotIndex + 80);
        }
    }

    public static void CreateCommodity(string name) {
        for (int i = 0; i < 64; i++) {
            if (DataManager.shopTra[i].childCount == 2) {
                Destroy(DataManager.shopTra[i].GetChild(1).gameObject);
            }
        }

        Commodity commodity = null;
        switch (name) { 
            case "武器":
                for (int i = 0; i < 64; i++) {
                    if (DataManager.WeaponFile[i] == 0) {
                        continue;
                    }
                    GameObject Item = GameObject.Instantiate(ItemPrefab, DataManager.shopTra[i]);     //指定位置生成指定物品
                    commodity = Item.AddComponent<Commodity>();
                    commodity.itemBase = FetchUtils.FetchEquipmentsBase(DataManager.WeaponFile[i]);       //为其中的物品基础类赋值

                    Item.GetComponent<Image>().sprite = commodity.itemBase.Sprite;  //显示贴图
                    Item.transform.GetChild(0).GetComponent<Text>().text = "";      //显示数量

                    Color newColor;
                    ColorUtility.TryParseHtmlString(GetColor(commodity.itemBase.Quality), out newColor);
                    Item.transform.GetChild(2).GetComponent<Image>().color = newColor;
                }
                break;
            case "防具":
                for (int i = 0; i < 64; i++) {
                    if (DataManager.ArmorFile[i] == 0) {
                        continue;
                    }
                    GameObject Item = GameObject.Instantiate(ItemPrefab, DataManager.shopTra[i]);     //指定位置生成指定物品
                    commodity = Item.AddComponent<Commodity>();
                    commodity.itemBase = FetchUtils.FetchEquipmentsBase(DataManager.ArmorFile[i]);       //为其中的物品基础类赋值

                    Item.GetComponent<Image>().sprite = commodity.itemBase.Sprite;  //显示贴图
                    Item.transform.GetChild(0).GetComponent<Text>().text = "";      //显示数量

                    Color newColor;
                    ColorUtility.TryParseHtmlString(GetColor(commodity.itemBase.Quality), out newColor);
                    Item.transform.GetChild(2).GetComponent<Image>().color = newColor;
                }
                break;
            case "消耗品":
                for (int i = 0; i < 64; i++) {
                    if (DataManager.ConsuFile[i] == 0) {
                        continue;
                    }
                    GameObject Item = GameObject.Instantiate(ItemPrefab, DataManager.shopTra[i]);     //指定位置生成指定物品
                    commodity = Item.AddComponent<Commodity>();
                    commodity.itemBase = FetchUtils.FetchConsumsBase(DataManager.ConsuFile[i]);       //为其中的物品基础类赋值

                    Item.GetComponent<Image>().sprite = commodity.itemBase.Sprite;  //显示贴图
                    Item.transform.GetChild(0).GetComponent<Text>().text = "";      //显示数量

                    Color newColor;
                    ColorUtility.TryParseHtmlString(GetColor(commodity.itemBase.Quality), out newColor);
                    Item.transform.GetChild(2).GetComponent<Image>().color = newColor;
                }
                break;
            case "材料":
                for (int i = 0; i < 64; i++) {
                    if (DataManager.MatsFile[i] == 0) {
                        continue;
                    }
                    GameObject Item = GameObject.Instantiate(ItemPrefab, DataManager.shopTra[i]);     //指定位置生成指定物品
                    commodity = Item.AddComponent<Commodity>();
                    commodity.itemBase = FetchUtils.FetchMaterial(DataManager.MatsFile[i]);       //为其中的物品基础类赋值

                    Item.GetComponent<Image>().sprite = commodity.itemBase.Sprite;  //显示贴图
                    Item.transform.GetChild(0).GetComponent<Text>().text = "";      //显示数量

                    Color newColor;
                    ColorUtility.TryParseHtmlString(GetColor(commodity.itemBase.Quality), out newColor);
                    Item.transform.GetChild(2).GetComponent<Image>().color = newColor;
                }
                break;
            case "卷轴":
                for (int i = 0; i < 64; i++) {
                    if (DataManager.ScrollFile[i] == 0) {
                        continue;
                    }
                    GameObject Item = GameObject.Instantiate(ItemPrefab, DataManager.shopTra[i]);     //指定位置生成指定物品
                    commodity = Item.AddComponent<Commodity>();
                    commodity.itemBase = FetchUtils.FetchScrollBase(DataManager.ScrollFile[i]);       //为其中的物品基础类赋值

                    Item.GetComponent<Image>().sprite = commodity.itemBase.Sprite;  //显示贴图
                    Item.transform.GetChild(0).GetComponent<Text>().text = "";      //显示数量

                    Color newColor;
                    ColorUtility.TryParseHtmlString(GetColor(commodity.itemBase.Quality), out newColor);
                    Item.transform.GetChild(2).GetComponent<Image>().color = newColor;
                }
                break;
        }
    }

    //将物品放在空网格上
    public static void ToEmpty(Item item, Slot slot) {
        int slotIndex = item.SlotIndex;
        
        DataManager.ItemArr[slot.Index] = item;                 //网格指向的Arr指向的物品更新
        DataManager.ItemArr[item.SlotIndex] = null;             //原存储物品的Arr的指向置为空
        item.SlotIndex = slot.Index;                            //拖拽体指向的Slot更新

        if (slotIndex >= 90 && slotIndex < 99) {
            SetEquipmentAtr();
            DataManager.SlotArr[slotIndex].transform.GetChild(0).gameObject.SetActive(true);
        }
        DataManager.SaveItem();
    }

    //将物品交换位置
    public static void Exchange(Item item, Slot slot) {
        Item curItem = DataManager.ItemArr[slot.Index];         //网格指向的物体
        Slot preSlot = DataManager.SlotArr[item.SlotIndex];
        DataManager.ItemArr[slot.Index] = item;                 //网格指向的Arr指向的物体更新
        curItem.SlotIndex = item.SlotIndex;                     //当前物体指向的网格更新为物体指向的网格
        item.SlotIndex = slot.Index;                            //物体指向的网格更新
        DataManager.ItemArr[preSlot.Index] = curItem;           //前网格指向的Arr指向的物体更新

        curItem.ReplaceParent();                                //当前物品的父物体更新
        DataManager.SaveItem();
    }

    //物品堆叠
    public static void Stack(Item item, Slot slot) {
        Item curItem = DataManager.ItemArr[slot.Index];         //网格指向的物体
        //若堆叠总量小于堆叠上限
        if (item.curStack + curItem.curStack <= item.itemBase.StackMax) {
            curItem.curStack += item.curStack;                  //更新当前物品的数量
            DataManager.ItemArr[item.SlotIndex] = null;         //指向物品的Arr置为空
            Destroy(item.gameObject);
        } else {
            //若堆叠数量大于堆叠上限
            item.curStack = (item.curStack + curItem.curStack) - item.itemBase.StackMax;
            curItem.curStack = item.itemBase.StackMax;
            item.ShowCount();
        }
        curItem.ShowCount();
        DataManager.SaveItem();
    }

    //拆分物品
    public static void SplitItem(Item item, Slot slot, int count) {
        CreateItem(item.itemBase.UID, slot.Index, count);              //指定位置创建新物品
        Item newItem = DataManager.ItemArr[slot.Index];         //新物品
        item.curStack -= count;
        item.ShowCount();
        DataManager.SaveItem();
    }

    public static void Equip(Equipment equipment){
        if (!UIManager.EquipmentPanel.activeSelf) {
            UIManager.ShowPanel(UIManager.EquipmentPanel);
        }
        if (!UIManager.Backpage.activeSelf) {
            UIManager.ShowPanel(UIManager.Backpage);
        }
        Slot EquipSlot = DataManager.SlotArr[equipment.equipemtnBase.Position + 90];
        //如果该装备在对应的装备栏上
        if (equipment.SlotIndex == EquipSlot.Index) {
            //放置在空位
            int index = FetchUtils.FetchEmpty();
            ToEmpty(equipment, DataManager.SlotArr[index]);
            EquipSlot.transform.GetChild(0).gameObject.SetActive(true);
        } else {
        //该装备不在对应的装备栏上
            //装备栏上存在其他物品
            if (DataManager.ItemArr[EquipSlot.Index] != null) { 
                //交换位置
                IASManager.Exchange(equipment, EquipSlot);
            } else { 
            //装备栏上不存在装备
                ToEmpty(equipment, EquipSlot);
                EquipSlot.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        SetEquipmentAtr();
        equipment.ReplaceParent();
        DataManager.SaveItem();
    }

    public static void Consu(Consum consum) {
        Debug.Log(consum.consumBase.ConType + "\\" + HPConTime + "\\" + MPConTime);
        if ((string.Equals(consum.consumBase.ConType, "HP") && HPConTime <= 0) || (string.Equals(consum.consumBase.ConType, "MP") && MPConTime <= 0)){
            
            UIManager.PlayerHandle.GetComponent<CharacterAttribute>()
                .UseDrug(consum.consumBase.ConType, consum.consumBase.ReValue, consum.consumBase.Duration);
            if(string.Equals(consum.consumBase.ConType, "HP")){
                HPConTime =consum.ConsuTimer;
            }
            if(string.Equals(consum.consumBase.ConType, "MP")){
                MPConTime =consum.ConsuTimer;
            }
            if (--consum.curStack == 0) {
                DataManager.ItemArr[consum.SlotIndex] = null;         //指向物品的Arr置为空
                Destroy(consum.gameObject);
            } else {
                consum.ShowCount();
            }
        }
        DataManager.SaveItem();
    }

    public static void Shop(ItemBase item, int count) {
        if (!UIManager.Backpage.activeSelf) {
            UIManager.ShowPanel(UIManager.Backpage);
        }
        for (int i = 0; i < count; i++) {
            CreateItem(item.UID);
        }
    }

    public static void SetEquipmentAtr() {
        BaseAttribute attr = new BaseAttribute();
        BaseAttribute equipmentAttr;
        for (int i = 90; i < 99; i++) {
            if (DataManager.ItemArr[i] != null) {
                equipmentAttr = ((Equipment)DataManager.ItemArr[i]).equipemtnBase.Attr;
                attr.HP += equipmentAttr.HP;
                attr.MP += equipmentAttr.MP;
                attr.ReHP += equipmentAttr.ReHP;
                attr.ReMP += equipmentAttr.ReMP;
                attr.Aggressivity += equipmentAttr.Aggressivity;
                attr.Armor += equipmentAttr.Armor;
                attr.Strength += equipmentAttr.Strength;
                attr.Intellect += equipmentAttr.Intellect;
                attr.Agile += equipmentAttr.Agile;
            }
        }
        UIManager.PlayerHandle.GetComponent<CharacterAttribute>().ChangeEquipAttribute(attr);
    }

    public static void Make(ScrollBase scroll) {
        foreach (int[] i in scroll.Mats) {
            DestoryItem(i[0], i[1]);
        }
        CreateItem(scroll.TarUID);
    }

    public static void DestoryItem(int uid, int count) {
        do {
            Materia lastMat = FetchUtils.FetchLastMaterial(uid);
            if (count < lastMat.curStack) {
                lastMat.curStack -= count;
                count = 0;
            }
            if (count == lastMat.curStack) {
                DataManager.ItemArr[lastMat.SlotIndex] = null;
                Destroy(lastMat.gameObject);
                count = 0;
            }
            if (count > lastMat.curStack) {
                DataManager.ItemArr[lastMat.SlotIndex] = null;
                Destroy(lastMat.gameObject);
                count -= lastMat.curStack;
            }
        } while (count != 0);
    }

    public void Arrangement() {
        for (int i = 0; i < 80; i++) {
            Item item = DataManager.ItemArr[i];
            if (item != null) {
                Item perItem;
                for (int j = 0; j < i; j++) {
                    perItem = DataManager.ItemArr[j];
                    if (perItem != null) {
                        if (perItem.itemBase.UID == item.itemBase.UID && perItem.curStack < perItem.itemBase.StackMax) {
                            Stack(item, DataManager.SlotArr[perItem.SlotIndex]);
                        }
                    }
                }
            }
        }

        int count = 0;
        for (int i = 0; i < 80; i++) {
            Item item = DataManager.ItemArr[i];
            if (item != null) {
                count++;
            }
        }
        for (int i = count; i < 80; i++) {
            Item item = DataManager.ItemArr[i];
            if (item != null) {
                int index = FetchUtils.FetchEmpty();
                ToEmpty(item, DataManager.SlotArr[index]);
                item.transform.SetParent(DataManager.SlotArr[index].transform);
                item.ReplaceParent();
            }
        }
        Item[] ItemCopy = new Item[count];
        for (int i = 0; i < count; i++) {
            ItemCopy[i] = DataManager.ItemArr[i];
        }
        Item temp;
        for (int i = 0; i < count - 1; i++) {
            for (int j = 0; j < count - 1 - i; j++) {
                if (ItemCopy[j].itemBase.UID > ItemCopy[j + 1].itemBase.UID) {
                    temp = ItemCopy[j];
                    ItemCopy[j] = ItemCopy[j + 1];
                    ItemCopy[j + 1] = temp;
                }
            }
        }
        for (int i = 0; i < count; i++) {
            Debug.Log(ItemCopy[i].itemBase.UID);
            ItemCopy[i].SlotIndex = i;
            ItemCopy[i].transform.SetParent(DataManager.SlotArr[i].transform);
            ItemCopy[i].transform.position = ItemCopy[i].transform.parent.position;
            DataManager.ItemArr[i] = ItemCopy[i];
        }
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
}
