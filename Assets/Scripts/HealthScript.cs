using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour 
{
	public UISlider lifebar;
	private int maxHp;
	private ScoreScript  score;

	float destination = 1;


	public int hp = 1;
	//设置是否为敌人的攻击
	public bool isEnemy =true;


	void Start()
	{
		//初始化人物的生命值
		maxHp = hp;
		//更新血条的生命
		UpdateLifebar();
		score = GameObject.Find("Score").GetComponent<ScoreScript>();
	}

	void LateUpdate()
	{
		//判断物体是否带血条或血条的生命值是否为空
		if(lifebar != null && lifebar.value != destination)
		{
			//设置血条的晃动值，血条在真实值与晃动值0.2之间发生变化
			lifebar.value = Mathf.Lerp (lifebar.value,destination,0.2f);

			if(Mathf.Abs (lifebar.value - destination) < 0.01f)
			{
				lifebar.value =destination;
			}
		}
	}



	public void Damage(int damageCount)
	{
		//生命值计算公式：受伤后的生命值=目前生命-伤害值
		hp -= damageCount;
		if (hp <= 0) 
		{
			//如果生命值小于零，播放爆炸声音和烟雾粒子效果，并且销毁目标
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			SoundEfectsHelper.Instance.MakeExplosionSound();

			Destroy(gameObject);
			//当怪物消失时候加100分
			score.score += 100;

		}
		//更新血条生命显示
		UpdateLifebar();
	}


	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		//加载射击脚本，获取子弹碰撞器
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		if(shot != null)
		{
			if(shot.isEnemyShot != isEnemy)
			{
				//如果有受到子弹攻击，且子弹是由敌人发射出来的则调用生命递减函数，并销毁子弹
				Damage(shot.damage );
				Destroy(shot.gameObject);
			}
		}
	}

	void UpdateLifebar()
	{
		if(lifebar != null)
		//lifebar.value = (float)hp / maxHp;
			destination = (float)hp / maxHp;
	}

	public void UpdateLifebarText()
	{
		//血条生命显示
		lifebar.GetComponentInChildren<UILabel> ().text = hp + "/" + maxHp;
	}
	 
}
