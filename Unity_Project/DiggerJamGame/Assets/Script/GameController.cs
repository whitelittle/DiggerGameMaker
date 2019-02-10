using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
   
    GUIStyle fontStyle; //字体样式

    public GameObject playerObj;
    public GameObject brushObj;
    public Texture2D texture;
    Brush brush;
    Force force;

    bool touchBack;

    private void Awake()
    {
        brush = Brush.GetInstance(brushObj);
        force = Force.GetInstance(playerObj);

        //设置字体样式
        fontStyle = new GUIStyle
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 25,
        };
        fontStyle.normal.textColor = Color.white;
        fontStyle.normal.background = texture ?? new Texture2D(400, 100);
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
        //设置刚体
        force.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    
        for (int i = 1; i < brush.Points.Count-1; i++)
        {
            yield return null;
            //限制速度
            //Vector3 tempForce = force.GetForce(brush.Points, i);
            //if (tempForce.magnitude > 500)
            //{
            //    tempForce = tempForce.normalized * 500;
            //}
            //force.Rigidbody.AddForce(tempForce);//施加力
            force.Rigidbody.AddForce(force.GetForce(brush.Points, i));//施加力
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

        if (GUI.Button(new Rect(25, 25, 80, 40), "菜单"))
        {
            touchBack = true;
        }

        if (touchBack)
        {
            OnAgainButton();
            OnChoseLevelButton();
        }

    }
    void windowfunction(int windowid)
    {
        
        if (playerObj.GetComponent<GameStateController>().isWin)
        {
            if (GameManager.curLevelIndex < GameManager.levelCount)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 100), "下一关", fontStyle))
                {
                    brush.BrushStatus = false;
                    //进入下一个场景
                    GameManager.LoadNextLevel();
                }
            }
            else
            {
                brush.BrushStatus = false;
                //显示通关文本
                GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 100), "通过所有关卡", fontStyle);
            }
            
        }

        OnAgainButton();
        OnChoseLevelButton();
        //定义窗体可以活动的范围
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    void OnAgainButton()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 100), "再试一次", fontStyle))
        {
            brush.BrushStatus = false;
            //重置回当前场景
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    void OnChoseLevelButton()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 100, 400, 100), "选关界面", fontStyle))
        {
            brush.BrushStatus = false;
            //重置回选关场景
            SceneManager.LoadScene(0);
        }
    }
}