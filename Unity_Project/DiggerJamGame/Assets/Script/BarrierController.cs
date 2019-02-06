using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 本脚本附加在所有需要操作的障碍物（非合理设计）
/// </summary>
public class BarrierController : MonoBehaviour {
    /// <summary>
    /// 障碍类别
    /// </summary>
    public BarrierType type;
    /// <summary>
    /// 是否循环操作
    /// </summary>
    public bool isLoop;
    /// <summary>
    /// 速度
    /// </summary>
    public float speed;
    /// <summary>
    /// 是循环操作时的最高计数
    /// </summary>
    public float maxCount;


    private float count=0;
    private float parameter = 0.5f;
    ///定义枚举
    public enum BarrierType
    {
        /// <summary>
        /// 上升
        /// </summary>
        Rise=1,
        /// <summary>
        /// 横向移动
        /// </summary>
        Translation=2,
        /// <summary>
        /// 选装
        /// </summary>
        Rotate =3,
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isLoop)
        {
            //是循环操作时开始计数
            count++;
            //达到最高计数时进行反方向运动，并清空计数
            if (count == maxCount)
            {
                parameter = -parameter;
                count = 0;
            }     
        }
        //对不同类别的操作
        switch ((int)type)
        {
            case (int)BarrierType.Rise:
                this.transform.Translate(new Vector3(0, parameter) * speed * Time.deltaTime);
                break;
            case (int)BarrierType.Rotate:
                this.transform.Rotate(Vector3.forward * speed * Time.deltaTime, Space.Self);
                break;
            case (int)BarrierType.Translation:
                this.transform.Translate(new Vector3(parameter, 0) * speed * Time.deltaTime);
                break;
            default: break;

        }
      
    }
  
}
