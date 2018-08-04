using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour 
{
	public Transform shotPrefab;
	public float shootingRate = 0.25f;
	private float shootCooldown;

	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;		
		}
	}

	// Use this for initialization
	void Start () 
	{
		//初始化武器的冷却时间，当怪物出现时候隔0.5秒才开始攻击。
		shootCooldown = 0.5f;
	}

	void Update()
	{
		//每次武器冷却值重设置
		shootCooldown -= Time.deltaTime;
	}
	public void  Attack(bool isEnemy)
	{
		if(CanAttack )
		{
			//把攻击频率赋时间作为冷却时间，让后初始化子弹预制体
			shootCooldown = shootingRate ;
			//初始化预制体的位置。
			Transform shotTransform = Instantiate (shotPrefab ) as Transform;
			//把当前物体所在的位置赋值给子弹
			shotTransform.position =transform.position;
			//获取武器脚本
			ShotScript shot = shotTransform.GetComponent<ShotScript>();
			if(shot != null)
			{
				//如果进行了射击后，默认为敌人子弹
				shot.isEnemyShot = isEnemy;
			}
			MoveScript move = shotTransform.GetComponent<MoveScript>();
			if(move != null )
			{
				move.direction = this.transform.right;
			}
		}
	}
	

}
