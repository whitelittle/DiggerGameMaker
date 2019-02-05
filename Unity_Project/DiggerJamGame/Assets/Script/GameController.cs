using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{



    public GameObject playerObj;
    public GameObject brushObj;
    Brush brush;
    Force force;
    
    private void Awake()
    {
        brush = Brush.GetInstance(brushObj);
        force = Force.GetInstance(playerObj);
    }
    void FixedUpdate()
    {
        force.RotateGameObject(brush.BrushStatus); 
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            brush.Points.Add(Input.mousePosition);  //记录经过点
        }
        //鼠标左击  
        if (Input.GetMouseButton(0) && !playerObj.GetComponent<GameStateController>().isFinished)
        {
            brush.PrintLine();
        }
        if (Input.GetMouseButtonUp(0) && !playerObj.GetComponent<GameStateController>().isFinished)
        {
            //清空上一条线的数据
            brush.ClearBrush();
            //施加力
            StartCoroutine(ObjectAddForce());
           
        }
    }
    IEnumerator ObjectAddForce()
    {
     
        //关闭重力影响
        force.Rigidbody.useGravity = true;
        for (int i = 1; i < brush.Points.Count-1; i++)
        {
            yield return null;
            force.Rigidbody.AddForce(force.GetForce(brush.Points,i));//施加力
        }
        //清空点
        brush.Points.Clear();
       
    }

    void OnGUI()
    {
        //GUILayout.Space(100);
        //GUILayout.Label("当前鼠标X轴位置：" + Input.mousePosition.x);
        //GUILayout.Label("当前鼠标Y轴位置：" + Input.mousePosition.y);
        //GUILayout.Label("状态：" + GameStatus);
        if (playerObj.GetComponent<GameStateController>().isFinished)
        {
            Rect windowrect = new Rect(0, 0, Screen.width, Screen.height);
            GUI.Window(0, windowrect, windowfunction, "");

        }

    }
    void windowfunction(int windowid)
    {
      
        if (playerObj.GetComponent<GameStateController>().isWin)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 100), "下一关"))
            {
                brush.BrushStatus = false;
                //进入下一个场景
               
            }
        }
        if (GUI.Button(new Rect(Screen.width/2-200, Screen.height/2-50, 400, 100), "再试一次"))
        {
            brush.BrushStatus = false;
            //重置回当前场景
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 +100, 400, 100), "选关界面"))
        {
            brush.BrushStatus = false;
            //重置回当前场景
           
        }
        //定义窗体可以活动的范围
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

}