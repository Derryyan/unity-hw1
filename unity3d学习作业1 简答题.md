# unity3d学习作业1 简答题

标签（空格分隔）： unity3d 简答作业

---

 - 解释 游戏对象（GameObjects） 和 资源（Assets）的区别与联系。
    - 游戏对象是组件的容器，是游戏内包括光源、摄像机之内的实体的总称。资源是预设好的音乐、材质、代码等等。资源需要添加到游戏对象上，或者实例化为游戏对象后，才能发挥作用。
 - 下载几个游戏案例，分别总结资源、对象组织的结构（指资源的目录组织结构与游戏对象树的层次结构）
    - 资源的目录一般根据种类分类，如背景、音乐、材质等，方便管理。
    - 游戏对象树与资源目录的分类方式相似，也是根据种类来划分，相同种类或用途的对象被放在同一个对象树下。
 - 编写一个代码，使用 debug 语句来验证 MonoBehaviour 基本行为或事件触发的条件
     - 基本行为包括 Awake() Start() Update() FixedUpdate() LateUpdate()
    - 常用事件包括 OnGUI() OnDisable() OnEnable()
        - 代码如下：
```
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Start");
	}

	// Update is called once per frame
	void Update () {
		Debug.Log("Update");
	}
	void Awake() {
		Debug.Log("Awake");
	}
	void FixedUpdate() {
		Debug.Log("FixedUpdate");
	}
	void LateUpdate() {
		Debug.Log("LateUpdate");
	}
	void OnGUI() {
		Debug.Log("OnGUI");
	}
	void OnDisable() {
		Debug.Log("OnDisable");
	}
	void OnEnable() {
		Debug.Log("OnEnable");
	}
}
```
- 部分结果为![console][1]
    

 - 根据console得到的结果以及官方手册，可以得到以下结论：
 - OnGUI在渲染和处理GUI事件时调用，这次测试由于没有相关行为所以比较难看出具体调用的时间。
 - FixedUpdate在固定时间片调用，而Update在每一帧都调用，LateUpdate在Update调用之后调用。
 - OnDisable在脚本被禁用时调用。
 

- 查找脚本手册，了解 GameObject，Transform，Component 对象
  - 分别翻译官方对三个对象的描述（Description）
      -  GameObject:是Unity场景里面所有实体的基类；是Unity中代表角色、道具、场景的基本对象，它们本身不完整，但是它们通过作为组件的容器来实现它们的功能。
      -  Transform：指的是游戏对象的位置、旋转和缩放。每个游戏对象都有transform，用于储存和操控对象的位置、旋转和缩放。每个transform都可以有一个父对象，允许分层次地进行transform。在Hierarchy面板可以查看层次关系。transform支持计数器，因此也可以进行对子对象的遍历。
      -  Component：一切附加到游戏物体的基类。
  - 描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件![2][2]
     - 本题目要求是把可视化图形编程界面与 Unity API 对应起来，当你在 Inspector 面板上每一个内容，应该知道对应 API。
     - 例如：table 的对象是 GameObject，第一个选择框是 activeSelf 属性。
 - 用UML图描述以上三者的关系
     - table对象的属性为 Tag: untagged; Layer:Default;
     - table 的 Transform属性为Position: (0, 0, 0)；Rotation: (0, 0, 0)；Scale : (1, 1, 1)
     - table的部件为Transform、Box Collider、Mesh Renderer
 - UML图如下 ：![3][3]

- 整理相关学习资料，编写简单代码验证以下技术的实现：
  - 查找对象
     - `static function Find (string name) : GameObject`用于按名字查找单个对象，如`GameObject.Find("table")`;如果名字中含有'/'则视为路径名。
     - `static function FindWithTag (string tag) : GameObject` 用于按标签查找单个对象
     - `static function FindGameObjectsWithTag (string tag) : GameObject[]`用于按标签查找所有对象，返回一个表
     - `static function FindObjectOfType (Type type) : Object`返回第一个被激活的Type类型的对象
     - `static function FindObjectsOfType (Type type) : Object[]`返回所有Type类型的对象，以数组存储
  - 添加子对象
     - 通过transform下的parent实现，如`a.transform.parent=b.transform`
  - 遍历对象树
    - 利用上文提到的 FindObjectsOfType，将类型定为GameObject即可得到所有对象，然后遍历得到的数组即可。
  - 清除所有子对象
    - `for (int i = 0; i < transform.childCount; i++) {  
            Destroy (transform.GetChild (i).gameObject);  
        }  ` 根据脚本手册，实际物体的销毁总是延迟到当前更新循环后，但总是渲染之前完成。所以此处childcount不会立即减少。 
- 资源预设（Prefabs）与 对象克隆 (clone)
 - 预设（Prefabs）有什么好处？
    -  预设可以使游戏对象和资源重复利用，并且对预设进行修改后，所有用该预设资源实例化的游戏对象都会发生改变。
 - 预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？
    - 两者都可以产生相同的对象，但是克隆出来的对象是独立的，如果克隆后要修改则需逐个修改；而将预设实例化后的对象需要修改的话只要修改预设即可。
 - 制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象
    - `GameObject preTable = Resources.Load("Table") as GameObject;
GameObject ins = Instantiate(preTable);` 
- 尝试解释组合模式（Composite Pattern / 一种设计模式）。使用 BroadcastMessage() 方法向子对象发送消息
    -  组合模式解耦了客户程序与复杂元素内部结构，从而使客户程序可以像处理简单元素一样来处理复杂元素。例子有系统目录结构，网站导航结构等。
    -  父对象调用BroadcastMessage(string method)时所有子对象都将调用method方法。调用BroadcastMessage时可以在方法名后附加参数。
    -  实例：
```
    -  public class Example : MonoBehaviour {
   void Start() {
			GameObject gameObject = GameObject.Find("Table");
			gameObject.BroadcastMessage("PrintWords", "test");
	}
}
```
PrintWords函数为
`void PrintWords(string words) {
        print(words);
    }`
    
结果为 test

 [^footnote]:注脚


  [1]: https://github.com/Derryyan/unity3d-learning/blob/master/pic/console.png
  [2]: https://pmlpml.github.io/unity3d-learning/images/ch02/ch02-homework.png
  [3]: https://github.com/Derryyan/unity3d-learning/blob/master/pic/gameObject.png