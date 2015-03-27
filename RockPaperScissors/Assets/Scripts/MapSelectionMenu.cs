/************************************************************************\
*	Author:		Braden Gibson											 *
*	Purpose:	Attaches to main camera. Allows the selection of a map   *
*				via voting. A tie is broken at random.					 *
\************************************************************************/
using UnityEngine;
using System.Collections;

// TODO: Add support for up to 4 players

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
	public Texture P3TokenTexture;
	public Texture P4TokenTexture;
	public Texture P1TokenSelectedTexture;
	public Texture P2TokenSelectedTexture;
	public Texture P3TokenSelectedTexture;
	public Texture P4TokenSelectedTexture;
	
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
	private Rect P3TokenRect_Trad;
	private Rect P3TokenRect_Btle;
	private Rect P3TokenRect_Land;
	private Rect P3TokenRect_Lava;
	private Rect P3TokenRect_Rand;
	private Rect P4TokenRect_Trad;
	private Rect P4TokenRect_Btle;
	private Rect P4TokenRect_Land;
	private Rect P4TokenRect_Lava;
	private Rect P4TokenRect_Rand;
	
	// Keep track of which button each player is selecting
	enum MapState {Trad=1, Btle, Land, Lava, Rand};	
	private MapState P1State;
	private MapState P2State;
	private MapState P3State;
	private MapState P4State;
	
	// Keep track of if each player has selected a map yet
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
		int xOffset1 = 1;
		int xOffset2 = (int)(buttonWidth - tokenWidth);
		int yOffset = (int)(buttonHeight - tokenHeight);
		
		// Inititalize the tokens' rectangles
		P1TokenRect_Trad = new Rect((buttonWidth + 2*horzSpacing)+xOffset1, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRect_Btle = new Rect((2*buttonWidth + 3*horzSpacing)+xOffset1, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRect_Land = new Rect((buttonWidth + 2*horzSpacing)+xOffset1, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRect_Lava = new Rect((2*buttonWidth + 3*horzSpacing)+xOffset1, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P1TokenRect_Rand = new Rect(horzSpacing+xOffset1, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		
		P2TokenRect_Trad = new Rect((buttonWidth + 2*horzSpacing)+xOffset2, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRect_Btle = new Rect((2*buttonWidth + 3*horzSpacing)+xOffset2, vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRect_Land = new Rect((buttonWidth + 2*horzSpacing)+xOffset2, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRect_Lava = new Rect((2*buttonWidth + 3*horzSpacing)+xOffset2, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		P2TokenRect_Rand = new Rect(horzSpacing+xOffset2, buttonHeight + 2*vertSpacing, (int)tokenWidth, (int)tokenHeight);
		
		P3TokenRect_Trad = new Rect((buttonWidth + 2*horzSpacing)+xOffset1, vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		P3TokenRect_Btle = new Rect((2*buttonWidth + 3*horzSpacing)+xOffset1, vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		P3TokenRect_Land = new Rect((buttonWidth + 2*horzSpacing)+xOffset1, buttonHeight + 2*vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		P3TokenRect_Lava = new Rect((2*buttonWidth + 3*horzSpacing)+xOffset1, buttonHeight + 2*vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		P3TokenRect_Rand = new Rect(horzSpacing+xOffset1, buttonHeight + 2*vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		
		P4TokenRect_Trad = new Rect((buttonWidth + 2*horzSpacing)+xOffset2, vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		P4TokenRect_Btle = new Rect((2*buttonWidth + 3*horzSpacing)+xOffset2, vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		P4TokenRect_Land = new Rect((buttonWidth + 2*horzSpacing)+xOffset2, buttonHeight + 2*vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		P4TokenRect_Lava = new Rect((2*buttonWidth + 3*horzSpacing)+xOffset2, buttonHeight + 2*vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		P4TokenRect_Rand = new Rect(horzSpacing+xOffset2, buttonHeight + 2*vertSpacing + yOffset, (int)tokenWidth, (int)tokenHeight);
		
		// Initialize the player selection
		P1State = MapState.Trad;
		P2State = MapState.Trad;
		P3State = MapState.Trad;
		P4State = MapState.Trad;
		
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
		
		
		if (P3Playing)
		{
			if (P3State == MapState.Trad)
			{
				if (P3Selected)
					GUI.DrawTexture(P3TokenRect_Trad, P3TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P3TokenRect_Trad, P3TokenTexture, ScaleMode.ScaleToFit, true);
			}
			else if (P3State == MapState.Btle)
			{
				if (P3Selected)
					GUI.DrawTexture(P3TokenRect_Btle, P3TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P3TokenRect_Btle, P3TokenTexture, ScaleMode.ScaleToFit, true);
			}
			else if (P3State == MapState.Land)
			{
				if (P3Selected)
					GUI.DrawTexture(P3TokenRect_Land, P3TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P3TokenRect_Land, P3TokenTexture, ScaleMode.ScaleToFit, true);
			}
			else if (P3State == MapState.Lava)
			{
				if (P3Selected)
					GUI.DrawTexture(P3TokenRect_Lava, P3TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P3TokenRect_Lava, P3TokenTexture, ScaleMode.ScaleToFit, true);
			}
			else
			{
				if (P3Selected)
					GUI.DrawTexture(P3TokenRect_Rand, P3TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P3TokenRect_Rand, P3TokenTexture, ScaleMode.ScaleToFit, true);
			}
		}
		
		
		if (P4Playing)
		{
			if (P4State == MapState.Trad)
			{
				if (P4Selected)
					GUI.DrawTexture(P4TokenRect_Trad, P4TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P4TokenRect_Trad, P4TokenTexture, ScaleMode.ScaleToFit, true);
			}
			else if (P4State == MapState.Btle)
			{
				if (P4Selected)
					GUI.DrawTexture(P4TokenRect_Btle, P4TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P4TokenRect_Btle, P4TokenTexture, ScaleMode.ScaleToFit, true);
			}
			else if (P4State == MapState.Land)
			{
				if (P4Selected)
					GUI.DrawTexture(P4TokenRect_Land, P4TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P4TokenRect_Land, P4TokenTexture, ScaleMode.ScaleToFit, true);
			}
			else if (P4State == MapState.Lava)
			{
				if (P4Selected)
					GUI.DrawTexture(P4TokenRect_Lava, P4TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P4TokenRect_Lava, P4TokenTexture, ScaleMode.ScaleToFit, true);
			}
			else
			{
				if (P4Selected)
					GUI.DrawTexture(P4TokenRect_Rand, P4TokenSelectedTexture, ScaleMode.ScaleToFit, true);
				else
					GUI.DrawTexture(P4TokenRect_Rand, P4TokenTexture, ScaleMode.ScaleToFit, true);
			}
		}
	}
	
	
	
	/********\
	* UPDATE *
	\********/
	void Update()
	{
		// If both players have selected, determine which map to load and do so
		if (P1Selected == true && P2Selected == true)
		{
			MapState selected; // Stores whatever the voted map is
			// Determine what the voted map is
			if (P1State == P2State) // If they voted for the same thing
			{
				selected = P1State;
			}
			else // They voted for different things
			{
				int rnd = Random.Range (1,3); // pick player 1 or player 2
				if (rnd == 1)
					selected = P1State;
				else
					selected = P2State;
			}
						
			// Load the map based on the winner
			int randomSelection = 0;
			if (selected == MapState.Rand) // Assign a value to the random selection, otherwise leave it as 0
			{
				randomSelection = Random.Range(1,5); // Creates a random number between 1 and 4
			}
				
			if (selected == MapState.Trad || randomSelection == 1)
				Application.LoadLevel("Traditional Fighter Map");
			else if (selected == MapState.Btle || randomSelection == 2)
				Application.LoadLevel("Smash Bros Battlefield Map");
			else if (selected == MapState.Land || randomSelection == 3)
				Application.LoadLevel("Landslide Map");
			else
				Application.LoadLevel("Lava Map");
		}
		
		// Update the tokens based on any key input, if the players haven't selected yet
		if (P1Selected == false)
		{
			switch(P1State)
			{
				case MapState.Rand:
					if (Input.GetAxis(P1yAxis) < 0) // up
						P1State = MapState.Trad;
				    else if (Input.GetAxis(P1xAxis) > 0) // right
				    	P1State = MapState.Land;
					break;	
				case MapState.Trad:	
					if (Input.GetAxis(P1yAxis) > 0) // down
						P1State	= MapState.Land;
					else if (Input.GetAxis(P1xAxis) > 0) // right
						P1State = MapState.Btle;
					else if (Input.GetAxis(P1xAxis) < 0) // left
						P1State = MapState.Rand;
					break;
				case MapState.Btle:
					if (Input.GetAxis(P1yAxis) > 0) // down
						P1State = MapState.Lava;
					else if (Input.GetAxis(P1xAxis) > 0) // right
						P1State = MapState.Trad;
					break;	
				case MapState.Land:
					if (Input.GetAxis(P1yAxis) < 0) // up
						P1State = MapState.Trad;
					else if (Input.GetAxis(P1xAxis) > 0) // right
						P1State = MapState.Lava;
					else if (Input.GetAxis(P1xAxis) < 0) // left
						P1State = MapState.Rand;
					break;	
				case MapState.Lava:
					if (Input.GetAxis(P1yAxis) < 0) // up
						P1State = MapState.Btle;
					else if (Input.GetAxis(P1xAxis) > 0) // right
						P1State = MapState.Land;
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
		
		if (P2Selected == false)
		{
			switch(P2State)
			{
			case MapState.Rand:
				if (Input.GetAxis(P2yAxis) < 0) // up
					P2State = MapState.Trad;
				else if (Input.GetAxis(P2xAxis) > 0) // right
					P2State = MapState.Land;
				break;	
			case MapState.Trad:	
				if (Input.GetAxis(P2yAxis) > 0) // down
					P2State	= MapState.Land;
				else if (Input.GetAxis(P2xAxis) > 0) // right
					P2State = MapState.Btle;
				else if (Input.GetAxis(P2xAxis) < 0) // left
					P2State = MapState.Rand;
				break;
			case MapState.Btle:
				if (Input.GetAxis(P2yAxis) > 0) // down
					P2State = MapState.Lava;
				else if (Input.GetAxis(P2xAxis) > 0) // right
					P2State = MapState.Trad;
				break;	
			case MapState.Land:
				if (Input.GetAxis(P2yAxis) < 0) // up
					P2State = MapState.Trad;
				else if (Input.GetAxis(P2xAxis) > 0) // right
					P2State = MapState.Lava;
				else if (Input.GetAxis(P2xAxis) < 0) // left
					P2State = MapState.Rand;
				break;	
			case MapState.Lava:
				if (Input.GetAxis(P2yAxis) < 0) // up
					P2State = MapState.Btle;
				else if (Input.GetAxis(P2xAxis) > 0) // right
					P2State = MapState.Land;
				break;
			}
		}
		// Check for the selection key being pressed
		if (Input.GetKeyUp(P2Enter))
		{
			// Toggle the selection
			if (P2Selected)
				P2Selected = false;
			else
				P2Selected = true;
		}
		
		if (P3Playing)
		{
			if (P3Selected == false)
			{
				switch(P3State)
				{
				case MapState.Rand:
					if (Input.GetAxis(P3yAxis) < 0) // up
						P3State = MapState.Trad;
					else if (Input.GetAxis(P3xAxis) > 0) // right
						P3State = MapState.Land;
					break;	
				case MapState.Trad:	
					if (Input.GetAxis(P3yAxis) > 0) // down
						P3State	= MapState.Land;
					else if (Input.GetAxis(P3xAxis) > 0) // right
						P3State = MapState.Btle;
					else if (Input.GetAxis(P3xAxis) < 0) // left
						P3State = MapState.Rand;
					break;
				case MapState.Btle:
					if (Input.GetAxis(P3yAxis) > 0) // down
						P3State = MapState.Lava;
					else if (Input.GetAxis(P3xAxis) > 0) // right
						P3State = MapState.Trad;
					break;	
				case MapState.Land:
					if (Input.GetAxis(P3yAxis) < 0) // up
						P3State = MapState.Trad;
					else if (Input.GetAxis(P3xAxis) > 0) // right
						P3State = MapState.Lava;
					else if (Input.GetAxis(P3xAxis) < 0) // left
						P3State = MapState.Rand;
					break;	
				case MapState.Lava:
					if (Input.GetAxis(P3yAxis) < 0) // up
						P3State = MapState.Btle;
					else if (Input.GetAxis(P3xAxis) > 0) // right
						P3State = MapState.Land;
					break;
				}
			}
			// Check for the selection key being pressed
			if (Input.GetKeyUp(P3Enter))
			{
				// Toggle the selection
				if (P3Selected)
					P3Selected = false;
				else
					P3Selected = true;
			}
		}
		
		if (P4Playing)
		{
			if (P4Selected == false)
			{
				switch(P4State)
				{
				case MapState.Rand:
					if (Input.GetAxis(P4yAxis) < 0) // up
						P4State = MapState.Trad;
					else if (Input.GetAxis(P4xAxis) > 0) // right
						P4State = MapState.Land;
					break;	
				case MapState.Trad:	
					if (Input.GetAxis(P4yAxis) > 0) // down
						P4State	= MapState.Land;
					else if (Input.GetAxis(P4xAxis) > 0) // right
						P4State = MapState.Btle;
					else if (Input.GetAxis(P4xAxis) < 0) // left
						P4State = MapState.Rand;
					break;
				case MapState.Btle:
					if (Input.GetAxis(P4yAxis) > 0) // down
						P4State = MapState.Lava;
					else if (Input.GetAxis(P4xAxis) > 0) // right
						P4State = MapState.Trad;
					break;	
				case MapState.Land:
					if (Input.GetAxis(P4yAxis) < 0) // up
						P4State = MapState.Trad;
					else if (Input.GetAxis(P4xAxis) > 0) // right
						P4State = MapState.Lava;
					else if (Input.GetAxis(P4xAxis) < 0) // left
						P4State = MapState.Rand;
					break;	
				case MapState.Lava:
					if (Input.GetAxis(P4yAxis) < 0) // up
						P4State = MapState.Btle;
					else if (Input.GetAxis(P4xAxis) > 0) // right
						P4State = MapState.Land;
					break;
				}
			}
			// Check for the selection key being pressed
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
