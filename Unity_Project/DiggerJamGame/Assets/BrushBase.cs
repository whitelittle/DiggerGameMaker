﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class BrushBase : MonoBehaviour
{
    public GameObject obj;
    //LineRenderer  
    private LineRenderer lineRenderer;
    //定义一个Vector3,用来存储鼠标点击的位置  
    private Vector3 position;
    //用来索引端点  
    private int index = 0;
    //端点数  
    private int LengthOfLineRenderer = 0;

    //用来索引端点  
    private Vector3 startPosition;
    //端点数  
    private Vector3 endPosition;
    //球体物理
    Rigidbody rig;

    private bool gameStatus = true;
    void Start()
    {
        //添加LineRenderer组件  
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        //设置材质  
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //设置颜色  
        lineRenderer.SetColors(Color.red, Color.blue);
        //设置宽度  
        lineRenderer.SetWidth(0.05f, 0.05f);




    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //记录起点
            startPosition = Input.mousePosition;
           
        }
        //获取LineRenderer组件  
        lineRenderer = GetComponent<LineRenderer>();
        //鼠标左击  
        if (Input.GetMouseButton(0)&& gameStatus)
        {
            //将鼠标点击的屏幕坐标转换为世界坐标，然后存储到position中  
            position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            //端点数+1  
            LengthOfLineRenderer++;
            //设置线段的端点数  
            lineRenderer.SetVertexCount(LengthOfLineRenderer);
            //两点确定一条直线，所以我们依次绘制点就可以形成线段了  
            lineRenderer.SetPosition(index, position);
            index++;

        }
        if (Input.GetMouseButtonUp(0)&& gameStatus)
        {
            //清空上一条线的数据
            lineRenderer = new LineRenderer();
            position = new Vector3();
            index = 0;
            LengthOfLineRenderer = 0;
            //记录终点
            endPosition = Input.mousePosition;
            //添加刚体
            rig = obj.AddComponent<Rigidbody>();
            //终点减去起点获得向量
            Vector3 offset = endPosition - startPosition;
            //施加力
            rig.AddForce(offset*2);
            //修改游戏状态
            gameStatus = false;

        }



    }

    void OnGUI()
    {
        GUILayout.Label("当前鼠标X轴位置：" + Input.mousePosition.x);
        GUILayout.Label("当前鼠标Y轴位置：" + Input.mousePosition.y);

        GUILayout.Label("起点位置：(" + startPosition.x + "," + startPosition.y + ")");
        GUILayout.Label("终点位置：(" + endPosition.x + "," + endPosition.y + ")");
        GUILayout.Label("直角边交点：(" + endPosition.x + "," + startPosition.y + ")");


        Vector3 offset = startPosition - endPosition;
        float distance = offset.magnitude;
        GUILayout.Label("距离："+ distance);
        GUILayout.Label("状态：" + gameStatus);
        if (!gameStatus )
        {
            if (GUI.Button(new Rect(50, 250, 200, 30), "重置"))
            {
                //重置场景
                SceneManager.LoadScene("Test_Brush");

            }
        }
 
    }


}