using UnityEngine;
using System.Collections;

public class SpecialEffectsHelper : MonoBehaviour 
{
	public static SpecialEffectsHelper Instance;
	public ParticleSystem smokeEffect;


	// Use this for initialization
	void Awake () 
	{
		if(Instance != null)
		{
			Debug.LogError("Multiple instance of SpecialEffectsHelper");

		}
		Instance = this;
	}

	private ParticleSystem instantiate(ParticleSystem parefab,Vector3 position)
	{
		//粒子实例化函数，用于实例化Parefab类型的物体
		ParticleSystem newParticleSystem = Instantiate (parefab, position, Quaternion.identity) as ParticleSystem;
		Destroy (newParticleSystem.gameObject, newParticleSystem.startLifetime);
		return newParticleSystem;
	}
	public void Explosion(Vector3 position)
	{
		//在目标所在的坐标上实例化烟雾粒子效果
		instantiate (smokeEffect, position);
	}

}
