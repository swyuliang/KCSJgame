using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour 
{
	const int buttonWidth = 120;
	const int buttonHeight = 60;
	
	void OnGUI()
	{
		if(GUI.Button (new Rect(Screen.width /2 - (buttonWidth/2),
		                        (1 * Screen.height / 3) - (buttonHeight /2),
		                        buttonWidth,
		                        buttonHeight),
		               "再来一次"))
		{
			Application.LoadLevel("Game1");
		}
		if(GUI.Button (new Rect(Screen.width /2 - (buttonWidth/2),
		                        (2 * Screen.height / 3) - (buttonHeight /2),
		                        buttonWidth,
		                        buttonHeight),
		               "回到主菜单"))
		{
			Application.LoadLevel("Menu");
		}
	}
}
