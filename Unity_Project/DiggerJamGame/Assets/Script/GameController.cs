using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{


    public bool GameStatus { get; set; }
    public GameObject playerObj;
    public GameObject brushObj;
    Brush brush;
    Force force;
    
    private void Awake()
    {
        GameStatus = true;
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
        if (Input.GetMouseButton(0) && GameStatus)
        {
            brush.PrintLine();
        }
        if (Input.GetMouseButtonUp(0) && GameStatus)
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
        //修改游戏状态
        GameStatus = false;
    }

    void OnGUI()
    {
        GUILayout.Label("当前鼠标X轴位置：" + Input.mousePosition.x);
        GUILayout.Label("当前鼠标Y轴位置：" + Input.mousePosition.y);


        GUILayout.Label("状态：" + GameStatus);
        if (!GameStatus)
        {
            if (GUI.Button(new Rect(50, 250, 200, 30), "重置"))
            {
                brush.BrushStatus = false;

                //重置回当前场景
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }

    }


}