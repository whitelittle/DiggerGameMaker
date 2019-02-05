using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public static class GameManager{

    public static int maxLevelIndex = 1;
    static string GameDataPath =Application.dataPath +"Data/GameData.txt";
    public static void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);

        if (levelIndex > maxLevelIndex)
        {
            maxLevelIndex = levelIndex;
        }
    }

    public static void SaveGameData()
    {

    }

    public static void ReadFile(string FileName)
    {
        string[] strs = File.ReadAllLines(FileName);//读取文件的所有行，并将数据读取到定义好的字符数组strs中，一行存一个单元
        for (int i = 0; i < strs.Length; i++)
        {
        }
    }

    public static void WriteFile()
    {
        
    }
}
