using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//建立节点所拥有的属性
public class point
{
    
    //父亲节点
    public point parent=null;
    //全局管理器的加入
    public manager manager=null;

    //起点
    public point start_point=null;
    //终点
    public point end_point=null;
    //x坐标和y坐标
    
    public int x=0;
    public int y=0;

    public double consume=999;
    
    //是否是障碍物
    public bool is_barrier = false;

    /**
     * 计算当前方格的消耗
     */
    public void caculate_consume()
    {
        double distance_to_staert;
        // distance_to_staert = x-
        consume = Math.Pow(x * x + y * y, 0.5f);
        // consume = consume+
    }
    
    
}

public class astar_node:MonoBehaviour
{
    public point point = new point();
    private void Start ()
    {
        point = new point();
        
    }

    //当鼠标按下的时候 进行是否变成障碍物
    private void OnMouseDown()
    {
        Debug.Log("按下");
        //如果本来是 就变成 不是 反正就是一个取反的工作
        point.is_barrier = !point.is_barrier;
        if (point.is_barrier == true)
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.black;
        }

    }
}

