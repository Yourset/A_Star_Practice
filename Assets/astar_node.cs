using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//建立节点所拥有的属性
public class point
{
    
    //父亲节点
    public point parent;
    //全局管理器的加入
    public manager manager=null;

    //起点
    public point start_point;
    //终点
    public point end_point;
    //x坐标和y坐标
    
    public int x;
    public int y;
    
    

    public double consume=999;
    
    //是否是障碍物
    public bool is_barrier = false;

    /**
     * 计算当前方格的消耗
     */
    public double caculate_consume()
    {
        double distance_to_start;
        double use_x, use_y;
        use_x = (double)x;
        use_y = (double)y;
        // distance_to_staert = x-
        distance_to_start = Math.Sqrt(use_x*use_x + use_y*use_y);
        double manhatten_distance = Math.Abs(end_point.x - use_x) + Math.Abs(end_point.y - use_y);
        consume = manhatten_distance + distance_to_start;

        return consume;
    }
    
    
    
}

public class astar_node:MonoBehaviour
{
    public point point = new point();
    private void Start ()
    {
        //不是已经实例化了吗
        
        
        
     
        
        
    }

    //当鼠标按下的时候 进行是否变成障碍物
    private void OnMouseDown()
    {
        Debug.Log("按下 "+point.x+"_"+point.y);
        //如果本来是 就变成 不是 反正就是一个取反的工作
        point.is_barrier = !point.is_barrier;
        color_judge();

    }

    private void color_judge()
    {
        if (point.is_barrier == false)
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (point.start_point==point)
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (point.end_point==point)
        {
            this.transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
        
    }
}

