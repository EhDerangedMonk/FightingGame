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
	enum CharState {Violet=1, Zakir, Noir};
	private CharState P1State;
	private CharState P2State;
	private CharState P3State;
	private CharState P4State;
	
	// Keep track of if each player has selected a character yet
	private bool P1Selected;
	private bool P2Selected;
	private bool P3Selected;
	private bool P4Selected;
	
	// How each player selects their character
	private KeyCode P1Enter;
	private string P1xAxis;
	private string P1yAxis;
	private KeyCode P2Enter;
	private string P2xAxis;
	private string P2yAxis;
	private KeyCode P3Enter;
	private string P3xAxis;
	private string P3yAxis;
	private KeyCode P4Enter;
	private string P4xAxis;
	private string P4yAxis;
	
	// Determine if players 3 and 4 are playing
	private bool P3Playing;
	private bool P4Playing;
	
	
	
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
		
		
		// Initialize the controller layouts
		InitializeStorage settings = (InitializeStorage) GameObject.Find("VariableStorage").GetComponent(typeof(InitializeStorage));
		switch ((int)settings.P1Controller)
		{
			case 1:
				P1Enter = KeyCode.Space;
				P1xAxis = "Keyboard1X";
				P1yAxis = "Keyboard1Y";
				break;
		case 2:
				P1Enter = KeyCode.Return;
				P1xAxis = "Keyboard2X";
				P1yAxis = "Keyboard2Y";
				break;
		case 3:
				P1Enter = KeyCode.Joystick1Button0;
				P1xAxis = "LeftJoystick1X";
				P1yAxis = "LeftJoystick1Y";
				break;
		case 4:
				P1Enter = KeyCode.Joystick2Button0;
				P1xAxis = "LeftJoystick2X";
				P1yAxis = "LeftJoystick2Y";
				break;
		case 5:
				P1Enter = KeyCode.Joystick3Button0;
				P1xAxis = "LeftJoystick3X";
				P1yAxis = "LeftJoystick3Y";
				break;
		case 6:
				P1Enter = KeyCode.Joystick4Button0;
				P1xAxis = "LeftJoystick4X";
				P1yAxis = "LeftJoystick4Y";
				break;
		}
		
		switch ((int)settings.P2Controller)
		{
			case 1:
				P2Enter = KeyCode.Space;
				P2xAxis = "Keyboard1X";
				P2yAxis = "Keyboard1Y";
				break;
		case 2:
				P2Enter = KeyCode.Return;
				P2xAxis = "Keyboard2X";
				P2yAxis = "Keyboard2Y";
				break;
		case 3:
				P2Enter = KeyCode.Joystick1Button0;
				P2xAxis = "LeftJoystick1X";
				P2yAxis = "LeftJoystick1Y";
				break;
		case 4:
				P2Enter = KeyCode.Joystick2Button0;
				P2xAxis = "LeftJoystick2X";
				P2yAxis = "LeftJoystick2Y";
				break;
		case 5:
				P2Enter = KeyCode.Joystick3Button0;
				P2xAxis = "LeftJoystick3X";
				P2yAxis = "LeftJoystick3Y";
				break;
		case 6:
				P2Enter = KeyCode.Joystick4Button0;
				P2xAxis = "LeftJoystick4X";
				P2yAxis = "LeftJoystick4Y";
				break;
		}
		
		P3Playing = true;
		switch ((int)settings.P3Controller)
		{
			case 0:
				P3Playing = false;
				break;
		case 1:
				P3Enter = KeyCode.Space;
				P3xAxis = "Keyboard1X";
				P3yAxis = "Keyboard1Y";
				break;
		case 2:
				P3Enter = KeyCode.Return;
				P3xAxis = "Keyboard2X";
				P3yAxis = "Keyboard2Y";
				break;
		case 3:
				P3Enter = KeyCode.Joystick1Button0;
				P3xAxis = "LeftJoystick1X";
				P3yAxis = "LeftJoystick1Y";
				break;
		case 4:
				P3Enter = KeyCode.Joystick2Button0;
				P3xAxis = "LeftJoystick2X";
				P3yAxis = "LeftJoystick2Y";
				break;
		case 5:
				P3Enter = KeyCode.Joystick3Button0;
				P3xAxis = "LeftJoystick3X";
				P3yAxis = "LeftJoystick3Y";
				break;
		case 6:
				P3Enter = KeyCode.Joystick4Button0;
				P3xAxis = "LeftJoystick4X";
				P3yAxis = "LeftJoystick4Y";
				break;
		}
		
		P4Playing = true;
		switch ((int)settings.P4Controller)
		{
			case 0:
				P4Playing = false;
				break;
			case 1:
				P4Enter = KeyCode.Space;
				P4xAxis = "Keyboard1X";
				P4yAxis = "Keyboard1Y";
				break;
		case 2:
				P4Enter = KeyCode.Return;
				P4xAxis = "Keyboard2X";
				P4yAxis = "Keyboard2Y";
				break;
		case 3:
				P4Enter = KeyCode.Joystick1Button0;
				P4xAxis = "LeftJoystick1X";
				P4yAxis = "LeftJoystick1Y";
				break;
		case 4:
				P4Enter = KeyCode.Joystick2Button0;
				P4xAxis = "LeftJoystick2X";
				P4yAxis = "LeftJoystick2Y";
				break;
		case 5:
				P4Enter = KeyCode.Joystick3Button0;
				P4xAxis = "LeftJoystick3X";
				P4yAxis = "LeftJoystick3Y";
				break;
		case 6:
				P4Enter = KeyCode.Joystick4Button0;
				P4xAxis = "LeftJoystick4X";
				P4yAxis = "LeftJoystick4Y";
				break;
		}
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
		
		if (P3Playing)
		{
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
		}
		
		if (P4Playing)
		{
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
	}
	
	/********\
	* UPDATE *
	\********/
	void Update()
	{
		// If all players have selected, go to the next scene (Map Selection)
		if (P1Selected && P2Selected && ((P3Playing && P3Selected) || !P3Playing) && ((P4Playing && P4Selected) || !P4Playing))
		{
			InitializeStorage storage = (InitializeStorage) GameObject.Find("VariableStorage").GetComponent(typeof(InitializeStorage));
			storage.P1Character = (InitializeStorage.CharacterSelection)((int)P1State);
			storage.P2Character = (InitializeStorage.CharacterSelection)((int)P2State);
			if (P3Playing == true)
				storage.P3Character = (InitializeStorage.CharacterSelection)((int)P3State);
			if (P4Playing == true)
				storage.P4Character = (InitializeStorage.CharacterSelection)((int)P4State);
					
					
			Application.LoadLevel("MapSelectionMenu");
		}
	
		// Update the tokens based on any key input, if the players haven't selected yet
		if (P1Selected == false)
		{
			switch(P1State)
			{
				case CharState.Noir:
					if (Input.GetAxis(P1yAxis) < 0) // up
						P1State = CharState.Zakir;
					else if (Input.GetAxis(P1xAxis) < 0) // left
						P1State = CharState.Violet;
					break;
				case CharState.Violet:
					if (Input.GetAxis(P1yAxis) < 0) // up
						P1State = CharState.Zakir;
					else if (Input.GetAxis(P1xAxis) > 0) // right
						P1State = CharState.Noir;
					break;
				case CharState.Zakir:
					if (Input.GetAxis(P1yAxis) > 0) // down
						P1State = CharState.Noir;
					else if (Input.GetAxis(P1xAxis) < 0) // left
						P1State = CharState.Violet;
					break;					
			}
		}
		// Check for the selection key being pressed
		if (Input.GetKeyUp(P1Enter))
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
					if (Input.GetAxis(P2yAxis) < 0) // up
						P2State = CharState.Zakir;
					else if (Input.GetAxis(P2xAxis) < 0) // left
						P2State = CharState.Violet;
					break;
				case CharState.Violet:
					if (Input.GetAxis(P2yAxis) < 0) // up
						P2State = CharState.Zakir;
					else if (Input.GetAxis(P2xAxis) > 0) // right
						P2State = CharState.Noir;
					break;
				case CharState.Zakir:
					if (Input.GetAxis(P2yAxis) > 0) // down
						P2State = CharState.Noir;
					else if (Input.GetAxis(P2xAxis) < 0) // left
						P2State = CharState.Violet;
					break;	
			}
		}
		if (Input.GetKeyUp(P2Enter))
		{
			// Toggle the selection
			if (P2Selected)
				P2Selected = false;
			else
				P2Selected = true;
		}
		
		// Same for P3, if P3 is playing
		if (P3Playing)
		{
			if (P3Selected == false)
			{
				switch(P3State)
				{
				case CharState.Noir:
					if (Input.GetAxis(P3yAxis) < 0) // up
						P3State = CharState.Zakir;
					else if (Input.GetAxis(P3xAxis) < 0) // left
						P3State = CharState.Violet;
					break;
				case CharState.Violet:
					if (Input.GetAxis(P3yAxis) < 0) // up
						P3State = CharState.Zakir;
					else if (Input.GetAxis(P3xAxis) > 0) // right
						P3State = CharState.Noir;
					break;
				case CharState.Zakir:
					if (Input.GetAxis(P3yAxis) > 0) // down
						P3State = CharState.Noir;
					else if (Input.GetAxis(P3xAxis) < 0) // left
						P3State = CharState.Violet;
					break;	
				}
			}
			if (Input.GetKeyUp(P3Enter))
			{
				// Toggle the selection
				if (P3Selected)
					P3Selected = false;
				else
					P3Selected = true;
			}
		}
		
		// Same for P4, if P4 is playing
		if (P4Playing)
		{
			if (P4Selected == false)
			{
				switch(P4State)
				{
				case CharState.Noir:
					if (Input.GetAxis(P4yAxis) < 0) // up
						P4State = CharState.Zakir;
					else if (Input.GetAxis(P4xAxis) < 0) // left
						P4State = CharState.Violet;
					break;
				case CharState.Violet:
					if (Input.GetAxis(P4yAxis) < 0) // up
						P4State = CharState.Zakir;
					else if (Input.GetAxis(P4xAxis) > 0) // right
						P4State = CharState.Noir;
					break;
				case CharState.Zakir:
					if (Input.GetAxis(P4yAxis) > 0) // down
						P4State = CharState.Noir;
					else if (Input.GetAxis(P4xAxis) < 0) // left
						P4State = CharState.Violet;
					break;	
				}
			}
			if (Input.GetKeyUp(P4Enter))
			{
				// Toggle the selection
				if (P4Selected)
					P4Selected = false;
				else
					P4Selected = true;
			}
		}
	}
}
