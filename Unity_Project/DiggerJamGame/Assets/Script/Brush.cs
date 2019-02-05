using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour {

    private static Brush _brush = null;
    private Brush()
    {
    }

    public static Brush GetInstance(GameObject gameObject)
    {
        if (_brush == null)
        {
            _brush = new Brush();
            _brush.GameObject = gameObject;
            _brush.Index = 0;
            _brush.LengthOfLineRenderer = 0;
            _brush.Points = new List<Vector3>();
            _brush.BrushStatus = false;
            _brush.LineRenderer = gameObject.AddComponent<LineRenderer>();
            _brush.LineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            _brush.LineRenderer.SetColors(Color.red, Color.blue);
            _brush.LineRenderer.SetWidth(0.05f, 0.05f);
        }
        return _brush;
    }

    /// <summary>
    /// 画笔对象
    /// </summary>
    public GameObject GameObject { get; set; }
    /// <summary>
    /// 渲染对象
    /// </summary>
    public LineRenderer LineRenderer { get; set; }
    /// <summary>
    /// 画笔状态
    /// </summary>
    public bool BrushStatus { get; set; }
    /// <summary>
    /// 画笔初始颜色
    /// </summary>
    public Color ColorStart { get; set; }
    /// <summary>
    /// 画笔结束颜色
    /// </summary>
    public Color ColorEnd { get; set; }
    /// <summary>
    /// 画笔途径点集合
    /// </summary>
    public List<Vector3> Points { get; set; }
    /// <summary>
    /// 当前画笔的点
    /// </summary>
    public Vector3 Position { get; set; }
    /// <summary>
    /// 端点索引
    /// </summary>
    public int Index { get; set; }
    /// <summary>
    /// 端点数 
    /// </summary>
    public int LengthOfLineRenderer { get; set; }


    public void PrintLine() {
        //将鼠标点击的屏幕坐标转换为世界坐标，然后存储到position中  
        this.Position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
        //端点数+1  
        this.LengthOfLineRenderer++;
        //设置线段的端点数  
        this.LineRenderer.SetVertexCount(this.LengthOfLineRenderer);
        //两点确定一条直线，所以我们依次绘制点就可以形成线段了  
        this.LineRenderer.SetPosition(this.Index, this.Position);
        this.Index++;
    }
    /// <summary>
    /// 清空上一条线的数据并初始化画笔状态
    /// </summary>
    public void ClearBrush() {
        //清空上一条线的数据
        this.Position = new Vector3();
        this.Index = 0;
        this.LengthOfLineRenderer = 0;
        this.BrushStatus = true;
    }
}
