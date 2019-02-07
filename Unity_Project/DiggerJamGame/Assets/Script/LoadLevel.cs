using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    int levelCount;

    //放置关卡按钮的容器
    [SerializeField] Transform btnContent;
    //按钮预制体
    [SerializeField] GameObject btnPrefab;

    private void Awake()
    {
        //关卡数=总场景数-Main场景
        levelCount = SceneManager.sceneCountInBuildSettings - 1;
        GameManager.levelCount = levelCount;
    }

    private void Start()
    {
        GameManager.LoadGameData();
        SpawnButton();
    }

    public void SpawnButton()
    {
        //索引0是关卡选择场景
        for (int i = 1; i <= levelCount; i++)
        {
            //根据关卡数实例化关卡按钮
            GameObject btnGo = Instantiate(btnPrefab, btnContent);

            //开辟新内存 防止onClick监听至同一个参数
            int index = i;

            //获取按钮监听点击事件
            Button btn = btnGo.GetComponent<Button>();
            btn.onClick.AddListener(() => GameManager.LoadLevel(index));

            //设置按钮名称
            Text text = btnGo.GetComponentInChildren<Text>();
            text.text = "关卡" + index;

            if (index > GameManager.maxLevelIndex)
            {
                btn.interactable = false;
            }
        }
    }
}
