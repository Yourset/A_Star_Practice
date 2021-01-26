using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

//建立节点所拥有的属性
public class point
{
    public point(astar_node n)
    {
        node = n;
    }
    
    public point()
    {
        
    }
    public astar_node node;
    //父亲节点
    public point parent;
    //全局管理器的加入
    public manager manager=null;

    /// <summary>
    /// 经过多个父亲节点走到这里的消耗
    /// </summary>
    public double g=99999;

    /// <summary>
    /// 预计到终点的消耗
    /// </summary>
    public double h;

    //预估
    public double F;

    
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
    public double caculate_consume(point fake_parent)
    {
        double distance_to_start;
        double use_x, use_y;
        use_x = (double)x;
        use_y = (double)y;
        
        //下面的都是根据起点和终点来进行的
        //但是实际上正确的应该是父亲节点和终点来计算
        // distance_to_staert = x-
        
        // distance_to_start = Math.Sqrt(Math.Abs(start_point.x - use_x)*Math.Abs(start_point.x - use_x) + Math.Abs(start_point.y - use_y)*Math.Abs(start_point.y - use_y));
        // double manhatten_distance = Math.Abs(end_point.x - use_x) + Math.Abs(end_point.y - use_y);
        // consume = manhatten_distance + distance_to_start;
        
        // distance_to_staert = x-
        // Debug.Log(parent);
        
        //求直线距离
        /*
        distance_to_start = Math.Sqrt(Math.Abs(parent.x - use_x)*Math.Abs(parent.x - use_x) + Math.Abs(parent.y - use_y)*Math.Abs//
        (parent.y - use_y));
        distance_to_start = math.floor(distance_to_start * 10) / 10+parent.g;
        g = distance_to_start;
        // distance_to_start = Math.Abs(start_point.x - use_x) + Math.Abs(start_point.y - use_y);
        //曼哈顿距离
        double manhatten_distance = Math.Abs(end_point.x - use_x) + Math.Abs(end_point.y - use_y);
        consume = manhatten_distance + g;
        Debug.Log("消耗" + consume);

        node.debug_text.text = consume.ToString();
*/
        double temp_to_parent = Math.Sqrt(Math.Abs(parent.x - use_x)*Math.Abs(parent.x - use_x) + Math.Abs(parent.y - use_y)*Math.Abs(parent.y - use_y));
        // 如果该节点在open列表中，则检查其通过当前节点计算得到的F值是否更小，如果更小则更新其F值，并将其父节点设置为当前节点。
        
        //根据当前的父亲节点计算g g=g开启节点的g+距离开启节点的g
        double fake_g = fake_parent.g + get_from_one_point(fake_parent);
        h = get_manhatten(end_point);
        if (g > fake_g)
        {
            g = fake_g;
            parent = fake_parent;
            consume = g + h;
        }else if (fake_parent == start_point)
        {
            g = get_from_one_point(fake_parent);
            consume = g + h;
        }
        consume = math.floor(consume * 10) / 10;
        //如果g<当前g
        //g替换 g更新 f更新
        node.debug_text.text = consume.ToString();
        
        //设置箭头
        node.shezhijiantou(parent);
        
        
return consume;
    }

    private double get_from_one_point(point a)
    {
        double use_x, use_y;
        use_x = (double)x;
        use_y = (double)y;
        double temp_to_parent = Math.Sqrt(Math.Abs(a.x - use_x)*Math.Abs(a.x - use_x) + Math.Abs(a.y - use_y)*Math.Abs(a.y - use_y));
        return temp_to_parent;
    }
    
    private double get_manhatten(point a)
    {
        double use_x, use_y;
        use_x = (double)x;
        use_y = (double)y;
        double temp_to_parent = Math.Abs(a.x - use_x) + Math.Abs(a.y - use_y);
        return temp_to_parent;
    }
    
    
    
}

public class astar_node:MonoBehaviour
{
    public Text debug_text;
    public point point = new point();
    public GameObject jiantou;
    private void Start ()
    {
        point.node = this.transform.GetComponent<astar_node>();

        //不是已经实例化了吗






    }

    public void shezhijiantou(point baba)
    {
        double set_x = (baba.x - point.x)*0.5;
        double set_y = (baba.y - point.y)*0.5;
        jiantou.transform.position = new Vector2(this.transform.position.x + (float) set_x, this.transform.position.y + (float) set_y);
    }

    //当鼠标按下的时候 进行是否变成障碍物
    private void OnMouseDown()
    {
        Debug.Log("按下 "+point.x+"_"+point.y);
        //如果本来是 就变成 不是 反正就是一个取反的工作
        point.is_barrier = !point.is_barrier;
        color_judge();

    }

    public void color_judge()
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

    // private void OnGUI()
    // {
    //     string aaa = point.consume.ToString();
    //     GUIContent a= new GUIContent();
    //     a.text = aaa;
    //     GUI.Label(new Rect(this.transform.position,new Vector2(2,2)), a);
    //     Debug.Log("运行");
    // }
}

