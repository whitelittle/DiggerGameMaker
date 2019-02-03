using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public int CurLevel = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadNextLoad()
    {
        CurLevel++;
        SceneManager.LoadScene("Level_" + CurLevel);
    }
}
