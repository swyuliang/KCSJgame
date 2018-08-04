using UnityEngine;
using System.Collections;
using System.Collections.Generic;//引用
using System.Linq;//引用Linq方法

public class ScrollingScript : MonoBehaviour 
{
	public Vector2 speed = new Vector2(2,2);//定义二维速度
	public Vector2 direction = new Vector2(-1,0);//定义二维方向
	public bool isLinkedToCamera = false;


	public bool isLooping =false;
	//创建一个列表管理所有背景图
	private List<Transform> backgroundPart;

	// Use this for initialization
	void Start () 
	{
		if(isLooping)
		{
			backgroundPart =new List<Transform>();//初始化列表
			//运用for循环获取所有的列表
			for(int i =0 ; i < transform.childCount ; i++)
			{
				//获取第i个子午胎
				Transform child = transform.GetChild (i);
				//判断是否被渲染，如果没有则不添加
				if(child.renderer != null)
				{
					//添加此子物体
					backgroundPart.Add (child);
				}
			}
			//对已获得的背景根据X轴的大小进行排序
			backgroundPart  =backgroundPart.OrderBy(t => t.position.x).ToList();
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 movement = new Vector3 (speed.x * direction.x, speed.y * direction.y,0);
		movement *= Time.deltaTime;//根据时间帧的变化而进行移动
		transform.Translate (movement);//根据movement的值的变化进行移动
		if(isLinkedToCamera)
		{
			Camera.main.transform.Translate (movement);
		}

		if(isLooping)
		{
			//获得背景列表中的第一个对象，即第一个背景。
			Transform firstChild = backgroundPart.FirstOrDefault();
			if(firstChild != null)
			{
				//判断第一个背景的x值是否小于主摄像机的x值，如果小于则证明主摄像机已经走出了第一个背景
				if(firstChild.position.x < Camera.main.transform.position.x)
				{
					//再判断第一个背景是否在渲染，如果不是则进行搬运
					if(firstChild.renderer.IsVisibleFrom (Camera.main)== false)
					{
						//找到最后一个背景图
						Transform lastChild = backgroundPart.LastOrDefault();
						//获取最后一个背景的坐标信息
						Vector3 lastPosition =lastChild.transform.position;
						//获取最后一个背景的宽度
						Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);
						firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y,firstChild.position.z);
						backgroundPart.Remove(firstChild);//把第一个背景图移去
						backgroundPart.Add(firstChild);//再一次把除去的背景图添加到列表，此时会自动添加到列表后面
					}
				}
			}
		}
	
	}
}
