using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
	//怪物运动的速度
	public Vector2 speed = new Vector2(10,10);
	//怪物运动的方向
	public Vector2 direction = new Vector2 (-1, 0);
	private Vector3 movement;

	// Update is called once per frame
	void Update () 
	{
		//怪物速度的计算公式（方向*速度大小）
		movement = new Vector3 (direction.x * speed.x , direction.y * speed.y, 0);

	}
	void FixedUpdate()
	{	
		//把速度赋值给2D刚体速度，形成运用物理动力驱动怪物
		rigidbody2D.velocity = movement;
	}
}
