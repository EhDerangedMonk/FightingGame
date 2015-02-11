/// <summary>
/// Character Selection Menu.
/// Attaches to main camera.
/// </summary>
using UnityEngine;
using System.Collections;

public class CharacterSelectionMenu : MonoBehaviour {
	
	public float ButtonLocationX;
	public float ZakirButtonLocationY;
	public float VioletButtonLocationY;
	public float NoirButtonLocationY;
	
	public GUIStyle CharacterButtonTexture;
	
	public Texture NoirIdle;
	public Texture ZakirIdle;
	public Texture VioletIdle;
	
	void OnGUI()
	{
		// Display the buttons
		if (GUI.Button (new Rect (Screen.width * ButtonLocationX, Screen.height * ZakirButtonLocationY, 350, 250), "", CharacterButtonTexture))
		{
			print ("Selected ZAKIR");
			
			// Load the map scene
			Application.LoadLevel("MapSelectionMenu");
		}
		
		if (GUI.Button (new Rect (Screen.width * ButtonLocationX, Screen.height * VioletButtonLocationY, 350, 250), "", CharacterButtonTexture))
		{
			print ("Selected VIOLET");
			
			// Load the map scene
			Application.LoadLevel("MapSelectionMenu");
		}
		
		if (GUI.Button (new Rect (Screen.width * ButtonLocationX, Screen.height * NoirButtonLocationY, 350, 250), "", CharacterButtonTexture))
		{
			print ("Selected NOIR");
			
			// Load the map scene
			Application.LoadLevel("MapSelectionMenu");
		}
		
		// Display the characters
		GUI.DrawTexture(new Rect (Screen.width * ButtonLocationX+175, Screen.height * ZakirButtonLocationY+125, ZakirIdle.width, ZakirIdle.height), ZakirIdle);
		GUI.DrawTexture(new Rect (Screen.width * ButtonLocationX+175, Screen.height * VioletButtonLocationY+125, VioletIdle.width, VioletIdle.height), VioletIdle);
		GUI.DrawTexture(new Rect (Screen.width * ButtonLocationX+175, Screen.height * NoirButtonLocationY+125, NoirIdle.width, NoirIdle.height), NoirIdle);
	}
}
