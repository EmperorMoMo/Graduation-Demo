using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipFrame : MonoBehaviour {
    public GameObject tipPer;
    public static GameObject TipPerfab;
    public static Transform Trans;

    public void Start(){
        TipPerfab = tipPer;
        Trans = this.transform;
    }

    public static void ShowTip(string Name, int Count, int quality) {
        GameObject Tip = GameObject.Instantiate(TipPerfab, Trans);
        Tip.transform.GetComponentInChildren<Text>().text = "获得 <color=" + GetColor(quality) + ">" + Name + "</color> * <color=#FFFFFf>" + Count + "</color>";
        Destroy(Tip, 1.2f);
    }

    public static void NoMoney() {
        GameObject Tip = GameObject.Instantiate(TipPerfab, Trans);
        Tip.transform.GetComponentInChildren<Text>().text = "<color=#B500FF>金币不足</color>";
        Destroy(Tip, 1.2f);
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
