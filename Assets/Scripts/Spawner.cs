using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public float spawnTime = 5f;		// 每隔5秒生成一次怪物
	public float spawnDelay = 3f;		// 其中有3秒延迟时间
	public GameObject[] enemies;		// 保存需要生成的怪物的预制体

	void Start ()
	{
		// 初始化定时器，（定时器名字，延迟时间，相隔时间）
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}
	
	
	void Spawn ()
	{
		// 运用随机函数产生怪物
		int enemyIndex = Random.Range(0, enemies.Length);
		Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
	}
}
