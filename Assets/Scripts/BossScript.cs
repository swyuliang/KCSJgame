using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour 
{

	private bool hasSpawn;
	private Animator animator;
	private MoveScript moveScript;
	private WeaponScript weapon;

	public float minAttackCooldown = 0.5f;
	public float maxAttackCooldown = 2f;
	public SpriteRenderer bodyRenderer;

	private float aiCooldown;
	private bool isAttacking;
	private Vector2 positionTarget;

	void Awake()
	{
		//加载动画画控制器，移动脚本和武器脚本
		animator = GetComponent<Animator> ();
		moveScript = GetComponent<MoveScript> ();
		weapon =  GetComponentInChildren<WeaponScript> ();
	}


	void Start () 
	{
		//初始化Boss的状态，使其为未激活状态
		hasSpawn = false;
		collider2D.enabled = false;
		moveScript.enabled = false;
		weapon.enabled = false;
		isAttacking = false;
		aiCooldown = maxAttackCooldown;

	
	}

	void Attack()
	{
		//判断目标是否具备武器，并且武器是否在开启状态
		if(weapon != null && weapon.CanAttack)
		{
			weapon.Attack (true);
			SoundEfectsHelper.Instance.MakeEnemyShotSound();
		}
	}
	

	void Update () 
	{
		if(hasSpawn)
	  {
		aiCooldown -= Time.deltaTime;
		if(aiCooldown < 0)
		{
			isAttacking =! isAttacking ;
			aiCooldown = Random.Range (minAttackCooldown ,maxAttackCooldown);
			positionTarget = Vector2.zero;
			animator.SetBool("Attack",isAttacking);
		}
		if(isAttacking)
		{
			moveScript.direction = Vector2.zero;
			Attack ();
		}
		else
		{
		     Move();
		}
	  }
		else 
		{
			if(bodyRenderer.IsVisibleFrom(Camera.main))
			{
				Spawn();
			}

		}
	}

	void Move()
	{
		//判断初始坐标是否为（0,0）
		if(positionTarget == Vector2.zero)
		{
			//如果成立，则赋予Boss坐标新的值，新值为0~1之间的随机数
			Vector2 randonPoint = new Vector2(Random.Range(0f,1f),Random.Range(0f,1f));
			//把当前目标的坐标转化为世界坐标
			positionTarget = Camera.main.ViewportToWorldPoint(randonPoint);
		}
		if(collider2D.OverlapPoint (positionTarget))
		{
			positionTarget = Vector2.zero;
		}
		//设置当前方向：目标的坐标方向-当前坐标
		Vector2 direction = positionTarget - (Vector2)transform.position;
		moveScript.direction = direction.normalized;
	}

	void Spawn()
	{
		hasSpawn = true ;
		collider2D.enabled = true;
		moveScript.enabled = true;
     	weapon.enabled = true;

	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{

		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		//判断Boss是否被击中
		if(shot != null)
		{
			//击中前先判断Boss是否被玩家的炮弹击中
			if(shot.isEnemyShot == false)
			{
				//击中后调整Boss攻击的冷却时间，并播放被击中的动画
				aiCooldown = Random.Range (minAttackCooldown ,maxAttackCooldown);
				isAttacking = false;
				animator.SetTrigger("Hit");
			}
		}
	}

	
	void OnDestroy()
	{
		//当Boss被击败后，运行"胜利脚本"
		GameObject.Find ("GUI").AddComponent<WinScript>();
	
	}
}
