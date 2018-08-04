using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	//声明二维速度变量并且赋予初始值
	public Vector2 speed = new Vector2(50,50);

	private Vector3 movement;

	private ScoreScript  score;

	private Transform myTransform;

	void Start()
	{
		myTransform = transform;
		//获取分数脚本
		score = GameObject.Find("Score").GetComponent<ScoreScript>();
	}

	// Update is called once per frame
	void Update () 
	{
		//用于获取X轴的输入
		float inputX = Input.GetAxis("Horizontal");
		//用于获取Y轴的输入
		float inputY = Input.GetAxis("Vertical");
		//角色速度的计算公式（输入方向*速度大小）
		movement = new Vector3 (inputX * speed.x, inputY * speed.y, 0);

		//点击鼠标发射子弹
		bool shoot = Input.GetButtonDown("Fire1");

		if(shoot)
		{
			//获取武器脚本，并且检测角色是否带有武器
			WeaponScript weapon =GetComponent<WeaponScript>();
			if(weapon != null)
			{
				//每次攻击完成后，把武器关闭并且播放攻击声音
				weapon.Attack (false);
				SoundEfectsHelper.Instance.MakePlayerShotSound();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		StoneScript stone = collision.gameObject.GetComponent<StoneScript>();
		//对敌人造成伤害
		if(enemy != null)
		{
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if(enemyHealth != null)
				enemyHealth.Damage(enemyHealth.hp);
			damagePlayer = true;
		}
		//对石头造成伤害
		if(stone != null)
		{
			HealthScript stoneHealth = stone.GetComponent<HealthScript>();
			if(stoneHealth != null)
				stoneHealth.Damage(stoneHealth.hp);
			damagePlayer = true;
		}
		//同时角色自己也受到伤害
		if(damagePlayer)
		{
			HealthScript playerHealth = GetComponent<HealthScript>();
			playerHealth.Damage (1);
		}
	}

	void LateUpdate()
	{
		ClamPlayer();
	}


	void FixedUpdate()
	{	
		//把速度赋值给2D刚体速度，形成运用物理动力驱动角色
		rigidbody2D.velocity = movement;
	}


	void ClamPlayer()
	{
		//控制角色的移动范围，只能在主摄像机的范围内
		float dist = transform.position.z - Camera.main.transform.position.z;
		//获取主摄像机四个方向的坐标标
		float leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
		float rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;
		float topBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).y;
		float bottomBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, dist)).y;
		//把主角的范围限制在四个坐标的范围
		transform.position = new Vector3 (Mathf.Clamp (myTransform.position.x, leftBorder, rightBorder),
		                                 Mathf.Clamp (myTransform.position.y, topBorder, bottomBorder),
		                                 myTransform.position.z);
	}

	void OnDestroy()
	{
		//当角色死亡时候，获取GAMEOVER脚本，并且把血条的值设置为0
		GameObject.Find("GUI").AddComponent<GameOverScript>();
		GetComponent<HealthScript> ().lifebar.value = 0;
		score.score -= 100;
	}
}
