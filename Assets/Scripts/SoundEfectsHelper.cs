using UnityEngine;
using System.Collections;

public class SoundEfectsHelper : MonoBehaviour 
{
	public static SoundEfectsHelper Instance;
	public AudioClip explosionSound;
	public AudioClip playerShotSound;
	public AudioClip enemyShotSound;

	// Use this for initialization
	void Awake () 
	{
       if(Instance != null)
		{
			Debug.LogError("Multiple instance of SoundEfectsHelper");
		}
	
		Instance = this;
	}


	private void MakeSound(AudioClip originalClip)
	{
		AudioSource.PlayClipAtPoint (originalClip, Vector3.zero);
	}

	public void MakeExplosionSound()
	{
		MakeSound (explosionSound);
	}

	public void MakePlayerShotSound()
	{
		MakeSound (playerShotSound);
	}
	public void MakeEnemyShotSound()
	{
		MakeSound (enemyShotSound);
	}

}
