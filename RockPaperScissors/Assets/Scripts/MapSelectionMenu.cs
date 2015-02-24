﻿/// <summary>
/// Map Selection Menu.
/// Attaches to main camera.
/// </summary>
using UnityEngine;
using System.Collections;

public class MapSelectionMenu : MonoBehaviour {
	
	// Button Textures
	public Texture TradSelectionButton;
	public Texture BtleSelectionButton;
	public Texture LandSelectionButton;
	public Texture LavaSelectionButton;
	public Texture RandSelectionButton;
	public Texture MapSelectionButton;
	
	// Button Sizes
	private Rect TradRect;
	private Rect BtleRect;
	private Rect LandRect;
	private Rect LavaRect;
	private Rect RandRect;
	private Rect MapSelectionRect;
	
	// Token Textures
	public Texture P1TokenTexture;
	public Texture P2TokenTexture;
	public Texture P1TokenSelectedTexture;
	public Texture P2TokenSelectedTexture;
	
	// Token Rectangles (one for each token in each position)
	private Rect P1TokenRect_Trad;
	private Rect P1TokenRect_Btle;
	private Rect P1TokenRect_Land;
	private Rect P1TokenRect_Lava;
	private Rect P1TokenRect_Rand;
	private Rect P2TokenRect_Trad;
	private Rect P2TokenRect_Btle;
	private Rect P2TokenRect_Land;
	private Rect P2TokenRect_Lava;
	private Rect P2TokenRect_Rand;
	
	// Keep track of which button each player is selecting
	enum MapState {Trad=1, Btle, Land, Lava, Rand};	
	private MapState P1State;
	private MapState P2State;
	
	// Keep track of if each player has selected a character yet
	private bool P1Selected;
	private bool P2Selected;
	
	
	
	/*******\
	* START *
	\*******/
	void Start()
	{
		// Calculate the button sizes and spacing depending on the screen size
		int buttonWidth = Screen.width / 4;
		int buttonHeight = (int) (buttonWidth * 0.7125); // The height is multiplied by 0.7125 to match the aspect ratio of the button to 16:9
		int vertSpacing = (Screen.height - 2*buttonHeight)/3;
		int horzSpacing = (Screen.width - 3*buttonWidth)/4;
		
		// Initialize the buttons' rectangles
		TradRect = new Rect(buttonWidth + 2*horzSpacing, vertSpacing, buttonWidth, buttonHeight);
		BtleRect = new Rect(2*buttonWidth + 3*horzSpacing, vertSpacing, buttonWidth, buttonHeight);
		LandRect = new Rect(buttonWidth + 2*horzSpacing, buttonHeight + 2*vertSpacing, buttonWidth, buttonHeight);
		LavaRect = new Rect(2*buttonWidth + 3*horzSpacing, buttonHeight + 2*vertSpacing, buttonWidth, buttonHeight);
		RandRect = new Rect(horzSpacing, buttonHeight + 2*vertSpacing, buttonWidth, buttonHeight);
		MapSelectionRect = new Rect(horzSpacing, vertSpacing, buttonWidth, buttonHeight);
		
		// Calculate the token sizes based on the button sizes
		double tokenWidth = (double)P1TokenTexture.width * ((double)buttonWidth / (double)TradSelectionButton.width);
		double tokenHeight = (double)P1TokenTexture.height * ((double)buttonHeight / (double)TradSelectionButton.height);
		int P1offset = 1;
		int P2offset = (int)(buttonWidth - tokenWidth);
		
		// Inititalize the tokens' rectangles
		P1TokenRect_Trad = new Rect((buttonWidth + 2*horzSpacing)+P1offset, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRect_Btle = new Rect((2*buttonWidth + 3*horzSpacing)+P1offset, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRect_Land = new Rect((buttonWidth + 2*horzSpacing)+P1offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRect_Lava = new Rect((2*buttonWidth + 3*horzSpacing)+P1offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRect_Rand = new Rect(horzSpacing+P1offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRect_Trad = new Rect((buttonWidth + 2*horzSpacing)+P2offset, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRect_Btle = new Rect((2*buttonWidth + 3*horzSpacing)+P2offset, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRect_Land = new Rect((buttonWidth + 2*horzSpacing)+P2offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRect_Lava = new Rect((2*buttonWidth + 3*horzSpacing)+P2offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRect_Rand = new Rect(horzSpacing+P2offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		
		// Initialize the player selection
		P1State = MapState.Trad;
		P2State = MapState.Trad;
		
		// Initialize whether the players have selected to false
		P1Selected = false;
		P2Selected = false;
	}
	
	
	
	/*******\
	* ONGUI *
	\*******/
	void OnGUI()
	{
		// Draw the buttons, adjusted for screen width and height
		GUI.DrawTexture(TradRect, TradSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(BtleRect, BtleSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(LandRect, LandSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(LavaRect, LavaSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(RandRect, RandSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(MapSelectionRect, MapSelectionButton, ScaleMode.ScaleToFit, true);
		
		// Draw the tokens in the correct position, adjusted for screen size. Change the token texture depending on if it's selected.
		if (P1State == MapState.Trad)
		{
			if (P1Selected)
				GUI.DrawTexture(P1TokenRect_Trad, P1TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P1TokenRect_Trad, P1TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P1State == MapState.Btle)
		{
			if (P1Selected)
				GUI.DrawTexture(P1TokenRect_Btle, P1TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P1TokenRect_Btle, P1TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P1State == MapState.Land)
		{
			if (P1Selected)
				GUI.DrawTexture(P1TokenRect_Land, P1TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P1TokenRect_Land, P1TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P1State == MapState.Lava)
		{
			if (P1Selected)
				GUI.DrawTexture(P1TokenRect_Lava, P1TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P1TokenRect_Lava, P1TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else
		{
			if (P1Selected)
				GUI.DrawTexture(P1TokenRect_Rand, P1TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P1TokenRect_Rand, P1TokenTexture, ScaleMode.ScaleToFit, true);
		}
		
		if (P2State == MapState.Trad)
		{
			if (P2Selected)
				GUI.DrawTexture(P2TokenRect_Trad, P2TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P2TokenRect_Trad, P2TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P2State == MapState.Btle)
		{
			if (P2Selected)
				GUI.DrawTexture(P2TokenRect_Btle, P2TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P2TokenRect_Btle, P2TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P2State == MapState.Land)
		{
			if (P2Selected)
				GUI.DrawTexture(P2TokenRect_Land, P2TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P2TokenRect_Land, P2TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P2State == MapState.Lava)
		{
			if (P2Selected)
				GUI.DrawTexture(P2TokenRect_Lava, P2TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P2TokenRect_Lava, P2TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else
		{
			if (P2Selected)
				GUI.DrawTexture(P2TokenRect_Rand, P2TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P2TokenRect_Rand, P2TokenTexture, ScaleMode.ScaleToFit, true);
		}
	}
	
	
	
	/********\
	* UPDATE *
	\********/
	void Update()
	{
	
	}
}
