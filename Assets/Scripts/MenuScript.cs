using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour 
{
	const int buttonWidth = 80;
	const int buttonHeight = 60;
	
	void OnGUI()
	{
		if(GUI.Button (new Rect(Screen.width /2 - (buttonWidth/2),
		                        (1 * Screen.height / 2) - (buttonHeight /2),
		                        buttonWidth,
		                        buttonHeight),
		               "闯关模式"))
		{
			//打开“Game1”场景
			Application.LoadLevel("Game1");
		}
		if(GUI.Button (new Rect(Screen.width /2 - (buttonWidth/2),
		                        (2 * Screen.height / 3) - (buttonHeight /2),
		                        buttonWidth,
		                        buttonHeight),
		               "无尽模式"))
		{
			//打开“Score”场景
			Application.LoadLevel("Score");
		}
 }
}
