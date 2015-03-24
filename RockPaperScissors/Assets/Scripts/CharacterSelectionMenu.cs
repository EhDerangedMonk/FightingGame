/*******************************************************************************\
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
	public Texture P3TokenTexture;
	public Texture P4TokenTexture;
	public Texture P1TokenSelectedTexture;
	public Texture P2TokenSelectedTexture;
	public Texture P3TokenSelectedTexture;
	public Texture P4TokenSelectedTexture;
	
	// Token Rectangles (one for each token in each position)
	private Rect P1TokenRectN;
	private Rect P1TokenRectV;
	private Rect P1TokenRectZ;
	private Rect P2TokenRectN;
	private Rect P2TokenRectV;
	private Rect P2TokenRectZ;
	private Rect P3TokenRectN;
	private Rect P3TokenRectV;
	private Rect P3TokenRectZ;
	private Rect P4TokenRectN;
	private Rect P4TokenRectV;
	private Rect P4TokenRectZ;
	
	// Keep track of which button each player is selecting
	enum CharState {Noir=1, Violet, Zakir};
	private CharState P1State;
	private CharState P2State;
	private CharState P3State;
	private CharState P4State;
	
	// Keep track of if each player has selected a character yet
	private bool P1Selected;
	private bool P2Selected;
	private bool P3Selected;
	private bool P4Selected;
	
	
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
		int tokenWidth = (int)(P1TokenTexture.width * ((double)buttonWidth / (double)NoirSelectionButton.width));
		int tokenHeight = (int)(P1TokenTexture.height * ((double)buttonHeight / (double)NoirSelectionButton.height));
		int P1xOffset = 1;
		int P2xOffset = (int)(buttonWidth - tokenWidth);
		int P3xOffset = 1;
		int P3yOffset = (int)(buttonHeight - tokenHeight);
		int P4xOffset = (int)(buttonWidth - tokenWidth);
		int P4yOffset = (int)(buttonHeight - tokenHeight);
		
		// Initialize the tokens' rectangles
		P1TokenRectV = new Rect(horzSpacing + P1xOffset, buttonHeight + 2*vertSpacing, tokenWidth, tokenHeight);
		P1TokenRectZ = new Rect((buttonWidth + 2*horzSpacing)+P1xOffset, vertSpacing, tokenWidth, tokenHeight);
		P1TokenRectN = new Rect((buttonWidth + 2*horzSpacing)+P1xOffset, buttonHeight + 2*vertSpacing, tokenWidth, tokenHeight);
		
		P2TokenRectV = new Rect(horzSpacing + P2xOffset, buttonHeight + 2*vertSpacing, tokenWidth, tokenHeight);
		P2TokenRectZ = new Rect((buttonWidth + 2*horzSpacing) + P2xOffset, vertSpacing, tokenWidth, tokenHeight);
		P2TokenRectN = new Rect((buttonWidth + 2*horzSpacing) + P2xOffset, buttonHeight + 2*vertSpacing, tokenWidth, tokenHeight);
		
		P3TokenRectV = new Rect(horzSpacing + P3xOffset, buttonHeight + 2*vertSpacing + P3yOffset, tokenWidth, tokenHeight);
		P3TokenRectZ = new Rect((buttonWidth + 2*horzSpacing) + P3xOffset, vertSpacing + P3yOffset, tokenWidth, tokenHeight);
		P3TokenRectN = new Rect((buttonWidth + 2*horzSpacing) + P3xOffset, buttonHeight + 2*vertSpacing + P3yOffset, tokenWidth, tokenHeight);
		
		P4TokenRectV = new Rect(horzSpacing + P4xOffset, buttonHeight + 2*vertSpacing + P4yOffset, tokenWidth, tokenHeight);
		P4TokenRectZ = new Rect((buttonWidth + 2*horzSpacing) + P4xOffset, vertSpacing + P4yOffset, tokenWidth, tokenHeight);
		P4TokenRectN = new Rect((buttonWidth + 2*horzSpacing) + P4xOffset, buttonHeight + 2*vertSpacing + P4yOffset, tokenWidth, tokenHeight);
		
		// Initialize player selection
		P1State = CharState.Violet;
		P2State = CharState.Violet; 
		P3State = CharState.Violet;
		P4State = CharState.Violet;
		
		// Initialize whether the players have selected to false
		P1Selected = false;
		P2Selected = false;
		P3Selected = false;
		P4Selected = false;
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
		
		if (P3State == CharState.Violet)
		{
			if (P3Selected)
				GUI.DrawTexture(P3TokenRectV, P3TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P3TokenRectV, P3TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P3State == CharState.Zakir)
		{
			if (P3Selected)
				GUI.DrawTexture(P3TokenRectZ, P3TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P3TokenRectZ, P3TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else
		{
			if (P3Selected)
				GUI.DrawTexture(P3TokenRectN, P3TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P3TokenRectN, P3TokenTexture, ScaleMode.ScaleToFit, true);
		}
		
		if (P4State == CharState.Violet)
		{
			if (P4Selected)
				GUI.DrawTexture(P4TokenRectV, P4TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P4TokenRectV, P4TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else if (P4State == CharState.Zakir)
		{
			if (P4Selected)
				GUI.DrawTexture(P4TokenRectZ, P4TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P4TokenRectZ, P4TokenTexture, ScaleMode.ScaleToFit, true);
		}
		else
		{
			if (P4Selected)
				GUI.DrawTexture(P4TokenRectN, P4TokenSelectedTexture, ScaleMode.ScaleToFit, true);
			else
				GUI.DrawTexture(P4TokenRectN, P4TokenTexture, ScaleMode.ScaleToFit, true);
		}
	}
	
	/*-******\
	* UPDATE *
	\********/
	void Update()
	{
		// If all players have selected, go to the next scene (Map Selection)
		if (P1Selected == true && P2Selected == true && P3Selected == true && P4Selected == true)
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
