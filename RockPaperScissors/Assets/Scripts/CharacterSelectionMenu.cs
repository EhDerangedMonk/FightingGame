﻿/*******************************************************************************\
*	Author:		Braden Gibson											 		*
*	Purpose:	Attaches to main camera. Each player can select their character *
*				simultaneously. 												*
\*******************************************************************************/
using UnityEngine;
using System.Collections;

// TODO: Make selecting the characters spawn them correctly in the map
// TODO: Add support for up to 4 players

public class CharacterSelectionMenu : MonoBehaviour {
	
	// Button Textures
	public Texture NoirSelectionButton;
	public Texture ZakirSelectionButton;
	public Texture VioletSelectionButton;
	public Texture CharacterSelectionTitle;
	
	// Button Sizes
	private Rect NoirRect;
	private Rect ZakirRect;
	private Rect VioletRect;
	private Rect CharacterSelectionRect;
	
	// Token Textures
	public Texture P1TokenTexture;
	public Texture P2TokenTexture;
	public Texture P1TokenSelectedTexture;
	public Texture P2TokenSelectedTexture;
	
	// Token Rectangles (one for each token in each position)
	private Rect P1TokenRectN;
	private Rect P1TokenRectV;
	private Rect P1TokenRectZ;
	private Rect P2TokenRectN;
	private Rect P2TokenRectV;
	private Rect P2TokenRectZ;
	
	// Keep track of which button each player is selecting
	enum CharState {Noir=1, Violet, Zakir};
	private CharState P1State;
	private CharState P2State;
	
	// Keep track of if each player has selected a character yet
	private bool P1Selected;
	private bool P2Selected;
	
	
	/*******\
	* START *
	\*******/
	void Start()
	{
		// Calculate the button sizes and spacing depending on the screen size
		int buttonWidth = Screen.width * 2/7;
		int buttonHeight = (int) (buttonWidth * 0.7125); // The height is multiplied by 0.7125 to match the aspect ratio of the button to 16:9
		int vertSpacing = (Screen.height - 2*buttonHeight)/3;
		int horzSpacing = (Screen.width - 2*buttonWidth)/3;
	
		// Initialize the buttons' rectangles
		CharacterSelectionRect = new Rect(horzSpacing, vertSpacing, buttonWidth, buttonHeight);
		VioletRect = new Rect(horzSpacing, buttonHeight + 2*vertSpacing, buttonWidth, buttonHeight);
		ZakirRect = new Rect(buttonWidth + 2*horzSpacing, vertSpacing, buttonWidth, buttonHeight);
		NoirRect = new Rect(buttonWidth + 2*horzSpacing, buttonHeight + 2*vertSpacing, buttonWidth, buttonHeight);
		
		// Calculate the token sizes based on the button sizes
		double tokenWidth = (double)P1TokenTexture.width * ((double)buttonWidth / (double)NoirSelectionButton.width);
		double tokenHeight = (double)P1TokenTexture.height * ((double)buttonHeight / (double)NoirSelectionButton.height);
		int P1offset = 1;
		int P2offset = (int)(buttonWidth - tokenWidth);
		
		// Initialize the tokens' rectangles
		P1TokenRectV = new Rect(horzSpacing+P1offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRectZ = new Rect((buttonWidth + 2*horzSpacing)+P1offset, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRectN = new Rect((buttonWidth + 2*horzSpacing)+P1offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRectV = new Rect(horzSpacing+P2offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRectZ = new Rect((buttonWidth + 2*horzSpacing)+P2offset, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRectN = new Rect((buttonWidth + 2*horzSpacing)+P2offset, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		
		// Initialize player selection
		P1State = CharState.Violet;
		P2State = CharState.Violet; 
		
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
		GUI.DrawTexture(CharacterSelectionRect, CharacterSelectionTitle, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(VioletRect, VioletSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(ZakirRect, ZakirSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(NoirRect, NoirSelectionButton, ScaleMode.ScaleToFit, true);
		
		// Draw the tokens in the correct position, adjusted for screen size. Change the token texture depending on if it's selected.
		if (P1State == CharState.Violet)
		{
			if (P1Selected)
				GUI.DrawTexture(P1TokenRectV, P1TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P1TokenRectV, P1TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P1State == CharState.Zakir)
		{
			if (P1Selected)
				GUI.DrawTexture(P1TokenRectZ, P1TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P1TokenRectZ, P1TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else
		{
			if (P1Selected)
				GUI.DrawTexture(P1TokenRectN, P1TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P1TokenRectN, P1TokenTexture, ScaleMode.ScaleToFit, true);
		}
			
		if (P2State == CharState.Violet)
		{
			if (P2Selected)
				GUI.DrawTexture(P2TokenRectV, P2TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P2TokenRectV, P2TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P2State == CharState.Zakir)
		{
			if (P2Selected)
				GUI.DrawTexture(P2TokenRectZ, P2TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P2TokenRectZ, P2TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else
		{
			if (P2Selected)
				GUI.DrawTexture(P2TokenRectN, P2TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P2TokenRectN, P2TokenTexture, ScaleMode.ScaleToFit, true);
		}
	}
	
	/*-******\
	* UPDATE *
	\********/
	void Update()
	{
		// If both players have selected, go to the next scene (Map Selection)
		if (P1Selected == true && P2Selected == true)
		{
			Application.LoadLevel("MapSelectionMenu");
		}
	
		// Update the tokens based on any key input, if the players haven't selected yet
		if (P1Selected == false)
		{
			switch(P1State)
			{
				case CharState.Noir:
					if (Input.GetKeyDown(KeyCode.W))
						P1State = CharState.Zakir;
					else if (Input.GetKeyDown(KeyCode.A))
						P1State = CharState.Violet;
					break;
				case CharState.Violet:
					if (Input.GetKeyDown(KeyCode.W))
						P1State = CharState.Zakir;
					else if (Input.GetKeyDown(KeyCode.D))
						P1State = CharState.Noir;
					break;
				case CharState.Zakir:
					if (Input.GetKeyDown(KeyCode.S))
						P1State = CharState.Noir;
					else if (Input.GetKeyDown(KeyCode.A))
						P1State = CharState.Violet;
					break;					
			}
		}
		// Check for the selection key being pressed
		if (Input.GetKeyUp(KeyCode.Space))
		{
			// Toggle the selection
			if (P1Selected)
				P1Selected = false;
			else
				P1Selected = true;
		}
		
		// Same for P2
		if (P2Selected == false)
		{
			switch(P2State)
			{
				case CharState.Noir:
					if (Input.GetKeyDown(KeyCode.I))
						P2State = CharState.Zakir;
					else if (Input.GetKeyDown(KeyCode.J))
						P2State = CharState.Violet;
					break;
				case CharState.Violet:
					if (Input.GetKeyDown(KeyCode.I))
						P2State = CharState.Zakir;
					else if (Input.GetKeyDown(KeyCode.L))
						P2State = CharState.Noir;
					break;
				case CharState.Zakir:
					if (Input.GetKeyDown(KeyCode.K))
						P2State = CharState.Noir;
					else if (Input.GetKeyDown(KeyCode.J))
						P2State = CharState.Violet;
					break;	
			}
		}
		if (Input.GetKeyUp(KeyCode.Return))
		{
			// Toggle the selection
			if (P2Selected)
				P2Selected = false;
			else
				P2Selected = true;
		}
	}
}
