using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class LueBrush : MonoBehaviour
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

    //历经点记录
    [SerializeField] List<Vector3> points = new List<Vector3>();
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
        if (Input.GetMouseButton(0))
        {
            //记录经过点
            points.Add(Input.mousePosition);
        }
        //鼠标左击  
        if (Input.GetMouseButton(0) && gameStatus)
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
        if (Input.GetMouseButtonUp(0) && gameStatus)
        {
            //清空上一条线的数据
            position = new Vector3();
            index = 0;
            LengthOfLineRenderer = 0;

            StartCoroutine(ObjectAddForce());
        }
    }

    IEnumerator ObjectAddForce()
    {
        //添加刚体组件
        rig = gameObject.AddComponent<Rigidbody>();
        //关闭重力影响
        rig.useGravity = false;
        Vector3 offset_0 = Vector3.zero;
        Vector3 offset_1 = Vector3.zero;
        for (int i = 1; i < points.Count-1; i++)
        {
            yield return null;
            //终点减去起点获得向量
            offset_0 = points[i] - points[i - 1];
            offset_1 = points[i + 1] - points[i];
            
            //卸载力
            if (Vector3.Dot(offset_0.normalized, offset_1.normalized) < 0.7f)
            {
                rig.Sleep();
                rig.WakeUp();
            }

            //施加力
            rig.AddForce(offset_0 * 2);

        }
        //修改游戏状态
        gameStatus = false;
        //清空点
        points.Clear();
    }

    void OnGUI()
    {
        GUILayout.Label("当前鼠标X轴位置：" + Input.mousePosition.x);
        GUILayout.Label("当前鼠标Y轴位置：" + Input.mousePosition.y);


        GUILayout.Label("状态：" + gameStatus);
        if (!gameStatus)
        {
            if (GUI.Button(new Rect(50, 250, 200, 30), "重置"))
            {
                //重置回当前场景
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }

    }


}