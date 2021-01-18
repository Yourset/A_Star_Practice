using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//建立节点所拥有的属性
public class point
{
    //父亲节点
    public point parent;
    //全局管理器的加入
    public manager manager;

    //起点
    public point start_point;
    //终点
    public point end_point;
    
    //是否是障碍物
    public bool is_barrier = false;
}

public class astar_node:MonoBehaviour
{
    point point = new point();
    private void Start()
    {
        
    }

    //当鼠标按下的时候 进行是否变成障碍物
    private void OnMouseDown()
    {
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

