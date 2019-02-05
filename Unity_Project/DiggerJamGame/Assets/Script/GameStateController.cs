using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour {

    //主摄像机引用
    Camera mainCamera;
    //物体相对相机位置（0-1,0-1，z）;
    Vector2 viewPoint;
    //是否完成判断
    public bool isFinished = false;
    public bool isWin = false;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        viewPoint = mainCamera.WorldToViewportPoint(gameObject.transform.position);

        if (isFinished) return;
        if (viewPoint.x < 0 || viewPoint.x > 1 || viewPoint.y < 0 || viewPoint.y > 1)
        {
            Debug.Log("跑出屏幕外,重置回该关卡");
            isFinished = true;
        }
    }

    void OnGUI()
    {
        //GUILayout.Space(200);
        //GUILayout.Label("当前物体位置：" + viewPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFinished) return;
        if (other.CompareTag("target"))
        {
            Debug.Log("到达目标，进入下一关");
            isFinished = true;
            isWin = true;
        }
    }
}
