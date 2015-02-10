/// <summary>
/// Map Selection Menu.
/// Attaches to main camera.
/// </summary>
using UnityEngine;
using System.Collections;

public class MapSelectionMenu : MonoBehaviour {
	
	public float ButtonLocationX1;
	public float ButtonLocationX2;
	public float TraditionalButtonLocationY;
	public float BattlefieldButtonLocationY;
	public float LandslideButtonLocationY;
	public float LavaButtonLocationY;
	
	public GUIStyle TraditionalButtonTexture;
	public GUIStyle BattlefieldButtonTexture;
	public GUIStyle LandslideButtonTexture;
	public GUIStyle LavaButtonTexture;
	
	void OnGUI()
	{
		// Display the buttons
		if (GUI.Button (new Rect (Screen.width * ButtonLocationX1, Screen.height * TraditionalButtonLocationY, 350, 250), "", TraditionalButtonTexture))
		{
			print ("Clicked TRADITIONAL FIGHTER MAP");
			
			// Load the map scene
			Application.LoadLevel("Traditional Fighter Map");
		}
		
		if (GUI.Button (new Rect (Screen.width * ButtonLocationX2, Screen.height * BattlefieldButtonLocationY, 350, 250), "", BattlefieldButtonTexture))
		{
			print ("Clicked SMASH BROS. BATTLEFIELD MAP");
			
			// Load the map scene
			Application.LoadLevel("Smash Bros Battlefield Map");
		}
		
		if (GUI.Button (new Rect (Screen.width * ButtonLocationX1, Screen.height * LandslideButtonLocationY, 350, 250), "", LandslideButtonTexture))
		{
			print ("Clicked LANDSLIDE MAP");
			
			// Load the map scene
			Application.LoadLevel("Landslide Map");
		}
		
		if (GUI.Button (new Rect (Screen.width * ButtonLocationX2, Screen.height * LavaButtonLocationY, 350, 250), "", LavaButtonTexture))
		{
			print ("Clicked LAVA MAP");
			
			// Load the map scene
			Application.LoadLevel("Lava Map");
		}
	}
}
