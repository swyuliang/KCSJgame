using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour 
{
	public int damage = 1;
	public bool isEnemyShot = false; 


	void Start () 
	{
		//控制子弹发射后如果没有碰撞到任何目标，便在5秒后消失。
		Destroy (gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
