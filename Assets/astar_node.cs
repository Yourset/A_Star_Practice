using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Block_Type
{
    barrier,
    plane
    
    
    
}
public class AstarNode
{
    //坐标
    public float X;

    public float Y;//

    //父亲节点
    public AstarNode Father;

    //寻路消耗
    public float F;
    public float G;
    public float H;

    /// <summary>
    /// 起点
    /// </summary>
    public AstarNode Start;

    /// <summary>
    /// 终点
    /// </summary>
    public AstarNode End;

    /// <summary>
    /// 计算路径消耗\
    /// 不包括重新测量终点的位置
    /// </summary>
    public void get_F()
    {
        F = G + H;
    }
    
}