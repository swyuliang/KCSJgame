using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour 
{
	const int buttonWidth = 84;
	const int buttonHeight = 60;
	
	void OnGUI()
	{
		if(GUI.Button (new Rect(Screen.width /2 - (buttonWidth)/2,
		                        Screen.height * 2/3 - buttonHeight /2,
		                        buttonWidth,
		                        buttonHeight),
		               "你赢了！！"))
		{
			Application.LoadLevel("Menu");
		}
	}
}