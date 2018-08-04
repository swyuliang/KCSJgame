using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	private bool hasSpawn;
	private MoveScript moveScript;
	private WeaponScript weapon;


	void Awake ()
	{
		//获取目标上的武器脚本和移动脚本
		weapon =  GetComponentInChildren<WeaponScript> ();
		moveScript = GetComponent<MoveScript>();
	}

	void  Start()
	{
		//初始化物体被渲染前为静止状态
		hasSpawn = false;
		collider2D.enabled = false;
		moveScript.enabled = false;
		weapon.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//如果之前没有被渲染，且主相机渲染到目标
		if(hasSpawn == false)
		{
			if(renderer.IsVisibleFrom (Camera.main))
			{
				//调用开状态函数，把目标状态全开
				Spawn();
			}
		}
		else
		{
			//如果目标身上带有武器
	       if(weapon != null && weapon.CanAttack)
		   {
				//把攻击值设置为可以攻击，并且播放攻击声音
			weapon.Attack (true);
			SoundEfectsHelper.Instance.MakeEnemyShotSound();
		   }
			//如果子弹离开主相机的渲染范围，则销毁子弹
			if(renderer.IsVisibleFrom (Camera.main)== false)
			{
				Destroy (gameObject);
			}

		}

	}

	void Spawn()
	{
		//当被目标被渲染时，状态全开。
		hasSpawn = true;
		collider2D.enabled = true;
		moveScript.enabled = true;
		weapon.enabled = true;
	}

}

  