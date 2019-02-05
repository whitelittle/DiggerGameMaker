using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour {


    //力系数
    private const int FORCE = 25;
    //旋转速度
    private const int SPEED = 100;

    private static Force _force = null;
    private Force()
    {
    }

    public static Force GetInstance(GameObject gameObject)
    {
        if (_force == null)
        {
            _force = new Force();
            _force.Rigidbody = gameObject.AddComponent<Rigidbody>(); ;
            _force.Rigidbody.useGravity = false;
        }
        return _force;
    }

   
    //球体物理
    public Rigidbody Rigidbody { get; set; }
    /// <summary>
    /// 获得力的向量
    /// </summary>
    /// <param name="points">点的集合</param>
    /// <param name="index">当前所在点的索引</param>
    /// <returns>力的向量</returns>
    public Vector3 GetForce(List<Vector3> points,int index) {
        Vector3 offset_0 = Vector3.zero;
        Vector3 offset_1 = Vector3.zero;
        Vector3 offset_2 = Vector3.zero;
        Vector3 p1 = points[index];
        Vector3 p2 = points[index - 1];
        Vector3 p3 = Vector3.zero;
        Vector3 p4 = Vector3.zero;
        if (index > 1)
        {
            p3 = points[index - 2];
            offset_1 = p2 - p3;
            offset_2 = p1 - p2;
            offset_0 = -offset_1 + offset_2;
        }
        else
        {
            offset_0 = p1 - p2;
        }
        return offset_0 * Force.FORCE;
    }
    /// <summary>
    /// 卸载力
    /// </summary>
    /// <param name="offset1"></param>
    /// <param name="offset2"></param>
    public void WakeUpForce(Vector3 offset1, Vector3 offset2)
    {
        //卸载力
        if (Vector3.Dot(offset1.normalized, offset2.normalized) < 0.7f)
        {
            this.Rigidbody.Sleep();
            this.Rigidbody.WakeUp();
        }
    }

    public void RotateGameObject(bool brushStatus=true) {
        if(brushStatus)
            this.Rigidbody.gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * Force.SPEED);
    }
}
