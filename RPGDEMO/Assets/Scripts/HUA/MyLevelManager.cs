using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyLevelManager : MonoBehaviour
{
    private GameObject player;
    static string nextLevel;
    AsyncOperation async;
    public float tempProgress;
    //public Slider slider;
    public Text text;
    // Start is called before the first frame update

    private Image background;                   //背景图
    private Sprite[] sprite = new Sprite[8];    //存储背景图数组
    private int LastTimeIndex = -1;             //上次背景图索引
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("TestPlayer");
        tempProgress = 0;
        if (SceneManager.GetActiveScene().name=="LoadingScene")
        {
            background = GameObject.Find("BackGround").GetComponent<Image>();       //获取Image组件
            for (int i = 0; i < 8; i++) {                                           //读取存储背景图
                sprite[i] = Resources.Load<Sprite>("BackGround/10" + i);
            }
            int index;
            do {
                index = Random.Range(0, 8);
            } while (index == LastTimeIndex);
            background.sprite = sprite[index];
            LastTimeIndex = index;

            async =SceneManager.LoadSceneAsync(nextLevel);
            async.allowSceneActivation = false;
            Debug.Log("1");
        }

       
    }
    public void LoadLoadingLevel(string nextLevelName)
    {
        nextLevel = nextLevelName;
        DontDestroyOnLoad(player);
        player.SetActive(false);

        SceneManager.LoadSceneAsync("LoadingScene");

        

        player.SetActive(true);
    }
    void transformChange()
    {
        player.transform.position = new Vector3(37, 1, 52);
    }
    // Update is called once per frame
    void Update()
    {
        if(text)
        {
            tempProgress = Mathf.Lerp(tempProgress, async.progress, Time.deltaTime*10);
            //text.text =((int)(tempProgress / 9*10 * 100)).ToString() + "%";
            text.text = "Loading...";
            //slider.value =tempProgress / 9*10;
            if(tempProgress>0.855)
            {
                tempProgress = 1;
                text.text = "Loading...";

                //text.text = ((int)(tempProgress / 10.01f * 10 * 100)).ToString() + "%";
                //slider.value =tempProgress / 10.01f * 10;
                async.allowSceneActivation = true;
                //SceneManager.MoveGameObjectToScene(player, _scene);
                //DontDestroyOnLoad(player);
                transformChange();
            }
        }
    }
}
