using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Threading;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

//全局节点管理器
public class manager
{
    //建立单例模式
    static manager()
    {
        
    }
    
    //起点
    public point start_point = new point();
    //终点
    public point end_point= new point();
    //开启列表
    public List<point> open_list = new List<point>();
    //关闭列表 用于到了终点以后的回溯
    public List<point> close_list = new List<point>();

    public point[,] points;

    public List<GameObject> obj_blocks;
}
//
// public class point_list
// {
//     public ArrayList list;
//     
// }                                    
public class astar_manager:MonoBehaviour
{
    private int times = 0; 
    
    private int jinxing = 1;
    //起点和终点
    public int start_x=0;
    public int start_y=0;

    public int end_x = 5;
    public int end_y = 5;
    
    //基础方格内容
    public GameObject block;
    //横向方格数量
    public int x_num=5;
    //纵向方格数量
    public int y_num=4;
    //间距
    public float space_between=0.3f; 
    
    public manager mg = new manager();

    
    //游戏启动
    void output(string a)
    {
        print(a);
    }
    /// <summary>
    ///数据检测
    /// </summary>
    /// <param name="a">数据1</param>
    /// <param name="b">数据2</param>
    /// <param name="c">数据3</param>
    void output(double a=666,double b=666,double c=666,double d = 666)
    {
        // print(a+" "+b+" "+c+" "+d);
    }
    void Start()
    {
        //初始化
        chushihua();
        
    }

    /// <summary>
    ///
    /// 初始化的方法全部都存放在这里
    /// </summary>
    void chushihua()
    {
        mg.obj_blocks = new List<GameObject>();   
        mg.points = new point[x_num,y_num];
        //生成方块
        build_block();
        //更改起点和终点的颜色并且记录
        all_star_end();
        //记录到points
        build_points();

        mg.start_point.parent = mg.start_point;
    }
    
    //生产出所有方块 保存到mg
    void build_block()
    {
        
        //复制节点
        //横向
        for (int i = 0; i < x_num; i++)
        {
            
            //纵向
            for (int j = 0; j < y_num; j++)
            {
                GameObject block = Instantiate(this.block, this.transform);
                
                float x = this.transform.position.x+this.space_between*i;
                float y = this.transform.position.y+this.space_between*j;
                float z = this.transform.position.z;
                
                block.transform.position= new Vector3(x,y,z);

                // if (this.GetComponent<astar_node>().point.x==start_x&&this.GetComponent<astar_node>().point.y==start_y)
                // {
                //     this.GetComponent<astar_node>().point.parent = this.GetComponent<astar_node>().point;
                // }
                
                record_block(block,i,j);
                
                
            }
        }
        
    }
    //记录方块到obj_block
    void record_block(GameObject the_block,int x,int y)
    {
        //对物体 记录坐标
        mg.obj_blocks.Add(the_block); 
        the_block.GetComponent<astar_node>().point.x = x;
        the_block.GetComponent<astar_node>().point.y = y;

    }

    //记录起点和终点 并且更改颜色
    void SetStartPoint(int x=1,int y=1)
    {
        foreach (var VARIABLE in mg.obj_blocks)
        {
            //寻找到以后就
            if (VARIABLE.GetComponent<astar_node>().point.x == x&&VARIABLE.GetComponent<astar_node>().point.y == y)
            {
                //设为蓝色 并且设置起点
                VARIABLE.GetComponent<SpriteRenderer>().color = Color.blue;
                //记录起点
                mg.start_point = VARIABLE.GetComponent<astar_node>().point;
                // Debug.Log(mg.start_point);
                // VARIABLE.GetComponent<astar_node>().point.parent = VARIABLE.GetComponent<astar_node>().point;
                // Debug.Log("VAR"+VARIABLE.GetComponent<astar_node>().point.parent);

                //如果是起点 那么起点的父亲等于自己

            }
        }
    }
    //设置终点
    void SetEndPoint(int x=5 ,int y=4)
    {
        foreach (var VARIABLE in mg.obj_blocks)
        {
            //寻找到以后就
            if (VARIABLE.GetComponent<astar_node>().point.x == x&&VARIABLE.GetComponent<astar_node>().point.y == y)
            {
                //设为蓝色 并且设置起点
                VARIABLE.GetComponent<SpriteRenderer>().color = Color.red;
                //记录终点
                mg.end_point = VARIABLE.GetComponent<astar_node>().point;
            }
        }
    }

    void Set_each_block_start_end()
    {
        foreach (var VARIABLE in mg.obj_blocks)
        {
            VARIABLE.GetComponent<astar_node>().point.end_point = mg.end_point;
            VARIABLE.GetComponent<astar_node>().point.start_point = mg.start_point;
            if (VARIABLE.GetComponent<astar_node>().point.start_point == VARIABLE.GetComponent<astar_node>().point)
            {
                
                VARIABLE.GetComponent<astar_node>().point.parent = mg.start_point;
                Debug.Log("走过了"+VARIABLE.GetComponent<astar_node>().point.parent);
            }
        }
    }

    /// <summary>
    ///
    /// 对所有关于起点和终点的内容进行工作
    /// </summary>
    void all_star_end()
    {
        SetStartPoint(start_x,start_y);
        SetEndPoint(end_x,end_y);
        Set_each_block_start_end();
    }
    //根据当前方块 生成points
    void build_points()
    {
        foreach (var variable in mg.obj_blocks)
        {
             point block_a = variable.GetComponent<astar_node>().point;
             mg.points[block_a.x, block_a.y] = block_a;
             output( mg.points[block_a.x, block_a.y].x, mg.points[block_a.x, block_a.y].y);
        }        
    }
    //根据points计算A* 通过按钮触发

    public void find_button()
    {
        clear_all();
        //更改起点和终点的颜色并且记录
        all_star_end();
        //记录到points
        build_points();
        begin_to_find();
        
       
    }


    private point now_center;
    private int test_try_times = 0;
    void begin_to_find()
    {
        //清空openlist 然后再开始寻找 这个只是用于测试
        // mg.open_list.Clear();
        
        //寻找周边
        //添加到openlist
        //找最小的到closelist
        //在closelist中找最小的
        //寻找周边

        now_center = mg.start_point;

        jinxing = 2;
        
        find_open_near(now_center);
        
        // while(mg.close_list.Count!=mg.points.Length&& test_try_times<=200&&mg.open_list.Count!=0)
        // {
        //     test_try_times += 1;
        //     find_open_near(now_center);
        //     //这一步包含了放到closelist
        //     now_center=get_good_in_open();
        //     //如果不是只剩下一个
        //     if(mg.open_list.Count!=1)
        //     mg.open_list.Remove(now_center);
        //      
        //     if (now_center == mg.end_point)
        //     {
        //         break;
        //         
        //     }
        //     Debug.Log(mg.open_list.Count);
        //     if (mg.open_list.Count == 0)
        //     {
        //         Debug.Log("的确是因为openlist开完了所有没了");
        //     }
        // }
        //
        // if (now_center == mg.end_point)
        // {
        //     Debug.Log("找到啦！！！");
        //     set_color(now_center,Color.green);
        //     set_road_color_from_end(now_center);
        //  
        // }
        // else
        // {
        //     Debug.Log("没找到");
        // }
        
    }

    private void FixedUpdate()
    {
        times++;
        if(mg.close_list.Count!=mg.points.Length&& test_try_times<=1000&&mg.open_list.Count!=0&& jinxing==2&&times%10==0)
        {
            test_try_times += 1;
            find_open_near(now_center);
            //这一步包含了放到closelist
            now_center=get_good_in_open();
            //如果不是只剩下一个
            if(mg.open_list.Count!=1)
                mg.open_list.Remove(now_center);
             
            if (now_center == mg.end_point)
                // {
                //     Debug.Log("找到啦！！！");
                //     set_color(now_center,Color.green);
                //     set_road_color_from_end(now_center);
                //  
                // }
                // else
                // {
                //     Debug.Log("没找到");
                // }
            // Debug.Log(mg.open_list.Count);
            if (mg.open_list.Count == 0)
            {
                Debug.Log("的确是因为openlist开完了所有没了");
            }
            
            if (now_center == mg.end_point)
            {
                Debug.Log("找到啦！！！");
                set_color(now_center,Color.green);
                set_road_color_from_end(now_center);
                jinxing = 1;

            }
            else if (mg.open_list.Count==0)
            {
                Debug.Log("没找到");
                jinxing = 1;
            }
            
        }

        
        //跳出循环
       
            // if (now_center == mg.end_point||mg.close_list.Count==mg.open_list.Count)
            // {
            //     Debug.Log("找到啦！！！");
            //     set_color(now_center,Color.green);
            //     set_road_color_from_end(now_center);
            //     jinxing = 1;
            //
            // }
            // else
            // {
            //     Debug.Log("没找到");
            //     jinxing = 1;
            // }

            


        }
    

    /// <summary>
///
/// 在openlist找到最小的
/// </summary>
/// 
    point get_good_in_open()
{
    //用于比较的point 
    point compare_point = new point();
    compare_point.consume = 9999;
    ;
    foreach (var opoint in mg.open_list)
    {
        if(opoint.consume < compare_point.consume)
        {
            compare_point = opoint;
        }
        
    }
    

    //设置颜色
    set_color(compare_point,Color.yellow);
    //加入到closelist
    if (mg.close_list.Contains(compare_point) == false&& compare_point!=new point())
    {
        mg.close_list.Add(compare_point);
        
    }
    
    return compare_point;
}

/// <summary>
/// 清理所有信息
/// </summary>
void clear_all()
{
    //清理开启和关闭列表
    mg.open_list.Clear();
    mg.close_list.Clear();
    foreach (var VARIABLE in mg.obj_blocks)
    {
        VARIABLE.transform.GetComponent<astar_node>().color_judge();
        VARIABLE.transform.GetComponent<astar_node>().point.g = 99999;
        
    }
}
void set_road_color_from_end(point end)
{
    point p = end;
    while (p!=mg.start_point)
    {
        set_color(p.parent,Color.magenta);
        
        p = p.parent;
    }
    
}
point get_good_in_close()
{
    //用于比较的point 
    point compare_point = new point();
    ;
    foreach (var opoint in mg.close_list)
    {
        if(opoint.consume < compare_point.consume)
        {
            compare_point = opoint;
        }
        
    }

    
    
    return compare_point;
}

/// <summary>
/// 设置某个点为黄色
/// </summary>
/// <param name="p">这个点</param>
void set_color(point p ,Color the_color)
{
    foreach (var obj in mg.obj_blocks)
    {
        if (obj.GetComponent<astar_node>().point == p)
        {
            obj.GetComponent<SpriteRenderer>().color = the_color;
        }
    }
}
/// <summary>
///设置某个点的颜色为黄色
/// </summary>
/// <param name="x">x坐标</param>
/// <param name="y">y坐标</param>
void set_color(int x,int y )
{
    
}
    //探索当前方块周围的方块 并且加入到openlist
    void find_open_near(point center_point)
    {
        int x = center_point.x;
        int y = center_point.y; 
        //上下左右
        open_one(x,y+1,center_point);
        open_one(x,y-1,center_point);
        open_one(x+1,y,center_point);
        open_one(x-1,y,center_point);
        //左上左下右上右下
        open_one(x+1,y+1,center_point);
        open_one(x+1,y-1,center_point);
        open_one(x-1,y-1,center_point);
        open_one(x-1,y+1,center_point);
    }
    //检查一个方块是否符合加入openlist的条件  符合则加入
    void open_one(int x,int y,point father)
    {
        //判定方块位置合法
        if (x>=0&&y>=0&&y<y_num&&x<x_num)
        {
            
            
            output(x,y,333);
            
            point p = mg.points[x, y];
            //障碍物 是否存在 是否在closelist中存在
            if (p.is_barrier==false&&p!=null&&mg.open_list.Contains(p)==false&&mg.close_list.Contains(p)==false)
            {
                //计算一下
                p.parent = father;
                p.caculate_consume(father);
                mg.open_list.Add(p);
                

            }else if(p.is_barrier==false&&p!=null&&mg.open_list.Contains(p)==true&&mg.close_list.Contains(p)==false)//是否在openlist中存在 
            {
                //根据当前的父亲节点计算g g=g开启节点的g+距离开启节点的g
                //如果g<当前g
                //g替换 g更新 f更新
                p.caculate_consume(father);

            }
            
        }
    }
    //找到最小的 加入到closelist 并且把它作为父亲继续openlist
    
    // 先openlist 然后closelist 找到以后openlist closelist 直到closelist中包括了终点
    
    //根据颜色生成路径
    
    
    

    


   

}

