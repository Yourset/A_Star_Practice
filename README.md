总结一下
A*寻路开发总结
1、创建出来的图像和要用的算法相对应
就是两边都要做好关联的函数 比较重要的有
1、建立points数组 用来存放 用于计算的 节点
2、建立颜色和障碍设置的方法
3、物体这边要做好颜色显示等利于用户的方法 points这里仅仅是一个用于计算a*寻路的过程
point的定义如下
节点坐标，节点消耗，起点和终点的位置。
方法：
计算消耗
在添加points数组的时候 首先要全部添加 然后对所有的points数组中的节点设定起点和终点，用于后面的计算消耗。
寻路系统部分：
openlist closelist
openlist用于存放已经被打开但是未被设定为尝试路径的点
closelist用于存放已经是尝试路径的点
最终通过子节点和父节点的关系串联起来 从终点返回到起点


那个idea忘记删了 等下就删
