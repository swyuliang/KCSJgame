using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {
	public int score = 0;					// 定义分数处值
	private int previousScore = 0;			// 保存前一帧的分数
	
	void Awake ()
	{
	}
	
	void Update ()
	{
		// 设置分数在GUIText上的显示
		guiText.text = "Score: " + score;
		
		// 把当前分数复制给前一帧的分数。即用于更新分数。
		previousScore = score;
	}
}
