using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System;

public static class GameManager{

    public static string AssetCachesDir
    {
        get
        {
            string path = "";
            #if UNITY_EDITOR
              path = Application.dataPath + "/StreamingAssets/GameData.txt";
            #elif UNITY_IOS
              path = Application.temporaryCachePath +  "/GameData.txt";
            #elif UNITY_ANDROID
              path = Application.persistentDataPath +  "/GameData.txt";
            #else
              path = Application.streamingAssetsPath + "/GameData.txt";
            #endif
            Debug.Log(path);
            return path;
        }
    }
    static string GameDataPath = AssetCachesDir;

    //关卡索引从下标1开始算起
    public static int curLevelIndex = 1;
    //可以选择的最大关卡索引
    public static int maxLevelIndex = 1;
    //关卡总数
    public static int levelCount;
    //通过所有关卡
    public static bool isAllPass;


    //将游戏转成txt数据并保存

    public static List<string> listStr = new List<string>();
    //public static string[] strs;

    public static void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);

        //更新当前进入关卡索引
        curLevelIndex = levelIndex;
        //更新最大关卡数
        if (levelIndex > maxLevelIndex)
        {
            maxLevelIndex = levelIndex;
        }
        SaveGameData();
    }


    public static void LoadNextLevel()
    {
        if (curLevelIndex < levelCount)
        {
            curLevelIndex++;
            LoadLevel(curLevelIndex);
        }
        else
        {
            Debug.Log("通过所有关卡");
            isAllPass = true;   
        }
    }



    /// <summary>
    /// 保存游戏数据
    /// </summary>
    public static void SaveGameData()
    {
        listStr.Clear();
        listStr.Add("maxLevelIndex,"+maxLevelIndex.ToString());

        string[] strs = listStr.ToArray();
        WriteFile(GameDataPath,strs);
    }
    /// <summary>
    /// 保存游戏数据
    /// </summary>
    public static void SaveGameData(int lever)
    {
        listStr.Clear();
        listStr.Add("maxLevelIndex," + lever.ToString());

        string[] strs = listStr.ToArray();
        WriteFile(GameDataPath, strs);
    }

    /// <summary>
    /// 加载游戏数据
    /// </summary>
    public static void LoadGameData()
    {
        if (!File.Exists(GameDataPath))
        {
            CreateGameData(GameDataPath);
           
        }
        //读取文本 
        string[] strs = ReadFile();
        List<string> tempStrList = new List<string>();
        for (int i = 0; i < strs.Length; i++)
        {
            string[] tempStr = strs[i].Split(',');
            tempStrList.Add(tempStr[1]);
        }
        maxLevelIndex = Convert.ToInt32(tempStrList[0]);
    }

    private static string[] ReadFile()
    {
      
        string[] strs = File.ReadAllLines(GameDataPath);//读取文件的所有行，并将数据读取到定义好的字符数组strs中，一行存一个单元
        return strs;
    }

    public static void WriteFile(string path,string[] strs)
    {
        File.WriteAllLines(path, strs);
    }
    public static void CreateGameData(string path)
    {
        SaveGameData(1);
    }
}
