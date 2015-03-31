/************************************************************************\
*	Author:		Braden Gibson											 *
*	Purpose:	Attaches to main camera. Allows the selection of a map   *
*				via voting. A tie is broken at random.					 *
\************************************************************************/
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
	private KeyCode P1Back;
	private string P1xAxis;
	private string P1yAxis;
	private KeyCode P2Enter;
	private KeyCode P2Back;
	private string P2xAxis;
	private string P2yAxis;
	private KeyCode P3Enter;
	private KeyCode P3Back;
	private string P3xAxis;
	private string P3yAxis;
	private KeyCode P4Enter;
	private KeyCode P4Back;
	private string P4xAxis;
	private string P4yAxis;
	
	// Determine if players 3 and 4 are playing
	private bool P3Playing;
	private bool P4Playing;
	
	// Slow down the selecting by only letting them move once per "axis push"
	private bool P1canMove;
	private bool P2canMove;
	private bool P3canMove;
	private bool P4canMove;
	
	
	
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
		
		// Initialize whether the players can move their tokens
		P1canMove = true;
		P2canMove = true;
		P3canMove = true;
		P4canMove = true;
		
		
		// Initialize the controller layouts
		InitializeStorage settings = (InitializeStorage) GameObject.Find("VariableStorage").GetComponent(typeof(InitializeStorage));
		switch ((int)settings.P1Controller)
		{
			case 1:
				P1Enter = KeyCode.Space;
				P1Back = KeyCode.Escape;
				P1xAxis = "Keyboard1X";
				P1yAxis = "Keyboard1Y";
				break;
			case 2:
				P1Enter = KeyCode.Return;
				P1Back = KeyCode.Escape;
				P1xAxis = "Keyboard2X";
				P1yAxis = "Keyboard2Y";
				break;
			case 3:
				P1Enter = KeyCode.Joystick1Button0;
				P1Back = KeyCode.Joystick1Button1;
				P1xAxis = "LeftJoystick1X";
				P1yAxis = "LeftJoystick1Y";
				break;
			case 4:
				P1Enter = KeyCode.Joystick2Button0;
				P1Back = KeyCode.Joystick2Button1;
				P1xAxis = "LeftJoystick2X";
				P1yAxis = "LeftJoystick2Y";
				break;
			case 5:
				P1Enter = KeyCode.Joystick3Button0;
				P1Back = KeyCode.Joystick3Button1;
				P1xAxis = "LeftJoystick3X";
				P1yAxis = "LeftJoystick3Y";
				break;
			case 6:
				P1Enter = KeyCode.Joystick4Button0;
				P1Back = KeyCode.Joystick4Button1;
				P1xAxis = "LeftJoystick4X";
				P1yAxis = "LeftJoystick4Y";
				break;
		}
		
		switch ((int)settings.P2Controller)
		{
			case 1:
				P2Enter = KeyCode.Space;
				P2Back = KeyCode.Escape;
				P2xAxis = "Keyboard1X";
				P2yAxis = "Keyboard1Y";
				break;
			case 2:
				P2Enter = KeyCode.Return;
				P2Back = KeyCode.Escape;
				P2xAxis = "Keyboard2X";
				P2yAxis = "Keyboard2Y";
				break;
			case 3:
				P2Enter = KeyCode.Joystick1Button0;
				P2Back = KeyCode.Joystick1Button1;
				P2xAxis = "LeftJoystick1X";
				P2yAxis = "LeftJoystick1Y";
				break;
			case 4:
				P2Enter = KeyCode.Joystick2Button0;
				P2Back = KeyCode.Joystick2Button1;
				P2xAxis = "LeftJoystick2X";
				P2yAxis = "LeftJoystick2Y";
				break;
			case 5:
				P2Enter = KeyCode.Joystick3Button0;
				P2Back = KeyCode.Joystick3Button1;
				P2xAxis = "LeftJoystick3X";
				P2yAxis = "LeftJoystick3Y";
				break;
			case 6:
				P2Enter = KeyCode.Joystick4Button0;
				P2Back = KeyCode.Joystick4Button1;
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
				P3Back = KeyCode.Escape;
				P3xAxis = "Keyboard1X";
				P3yAxis = "Keyboard1Y";
				break;
			case 2:
				P3Enter = KeyCode.Return;
				P3Back = KeyCode.Escape;
				P3xAxis = "Keyboard2X";
				P3yAxis = "Keyboard2Y";
				break;
			case 3:
				P3Enter = KeyCode.Joystick1Button0;
				P3Back = KeyCode.Joystick1Button1;
				P3xAxis = "LeftJoystick1X";
				P3yAxis = "LeftJoystick1Y";
				break;
			case 4:
				P3Enter = KeyCode.Joystick2Button0;
				P3Back = KeyCode.Joystick2Button1;
				P3xAxis = "LeftJoystick2X";
				P3yAxis = "LeftJoystick2Y";
				break;
			case 5:
				P3Enter = KeyCode.Joystick3Button0;
				P3Back = KeyCode.Joystick3Button1;
				P3xAxis = "LeftJoystick3X";
				P3yAxis = "LeftJoystick3Y";
				break;
			case 6:
				P3Enter = KeyCode.Joystick4Button0;
				P3Back = KeyCode.Joystick4Button1;
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
				P4Back = KeyCode.Escape;
				P4xAxis = "Keyboard1X";
				P4yAxis = "Keyboard1Y";
				break;
			case 2:
				P4Enter = KeyCode.Return;
				P4Back = KeyCode.Escape;
				P4xAxis = "Keyboard2X";
				P4yAxis = "Keyboard2Y";
				break;
			case 3:
				P4Enter = KeyCode.Joystick1Button0;
				P4Back = KeyCode.Joystick1Button1;
				P4xAxis = "LeftJoystick1X";
				P4yAxis = "LeftJoystick1Y";
				break;
			case 4:
				P4Enter = KeyCode.Joystick2Button0;
				P4Back = KeyCode.Joystick2Button1;
				P4xAxis = "LeftJoystick2X";
				P4yAxis = "LeftJoystick2Y";
				break;
			case 5:
				P4Enter = KeyCode.Joystick3Button0;
				P4Back = KeyCode.Joystick3Button1;
				P4xAxis = "LeftJoystick3X";
				P4yAxis = "LeftJoystick3Y";
				break;
			case 6:
				P4Enter = KeyCode.Joystick4Button0;
				P4Back = KeyCode.Joystick4Button1;
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
		// Determine if the players can move their tokens again
		if (Input.GetAxis(P1xAxis) == 0 && Input.GetAxis(P1yAxis) == 0)
			P1canMove = true;
		if (Input.GetAxis(P2xAxis) == 0 && Input.GetAxis(P2yAxis) == 0)
			P2canMove = true;
		if (P3Playing)
		{
			if (Input.GetAxis(P3xAxis) == 0 && Input.GetAxis(P3yAxis) == 0)
				P3canMove = true;
		}
		if (P4Playing)
		{
			if (Input.GetAxis(P4xAxis) == 0 && Input.GetAxis(P4yAxis) == 0)
				P4canMove = true;	
		}
			
			
		// If both players have selected, determine which map to load and do so
		if (P1Selected == true && P2Selected == true && ((P3Playing && P3Selected == true) || !P3Playing) && ((P4Playing && P4Selected == true) || !P4Playing))
		{
			MapState selected = MapState.Trad; // Stores whatever the voted map is (give it a random starting value)
			
			
			// Determine what the voted map is
			int numLand = 0; // number of votes for Landslide
			int numLava = 0; // number of votes for Lava
			int numBtle = 0; // Battlefield
			int numTrad = 0; // Traditional
			int numRand = 0; // Random
			
			// Count the votes
			if (P1State == MapState.Land)
				numLand++;
			else if (P1State == MapState.Lava)
				numLava++;
			else if (P1State == MapState.Btle)
				numBtle++;
			else if (P1State == MapState.Trad)
				numTrad++;
			else
				numRand++;
				
			if (P2State == MapState.Land)
				numLand++;
			else if (P2State == MapState.Lava)
				numLava++;
			else if (P2State == MapState.Btle)
				numBtle++;
			else if (P2State == MapState.Trad)
				numTrad++;
			else
				numRand++;
			
			if (P3Playing)
			{
				if (P3State == MapState.Land)
					numLand++;
				else if (P3State == MapState.Lava)
					numLava++;
				else if (P3State == MapState.Btle)
					numBtle++;
				else if (P3State == MapState.Trad)
					numTrad++;
				else
					numRand++;
			}
				
			if (P4Playing)
			{
				if (P4State == MapState.Land)
					numLand++;
				else if (P4State == MapState.Lava)
					numLava++;
				else if (P4State == MapState.Btle)
					numBtle++;
				else if (P4State == MapState.Trad)
					numTrad++;
				else
					numRand++;
			}
			
			
			// Determine the winner based on the votes
			int maxVotes = Mathf.Max(numBtle, numLand, numLava, numRand, numTrad);  // The largest number of votes
			
			int btleWin = 0; // Determine if the map was a winner. If it's a 0, it's not up for selection. Otherwise assign it a number.
			int tradWin = 0;
			int landWin = 0;
			int lavaWin = 0;
			int randWin = 0;
			
			int numWinners = 0; // Keep track of the number of winning maps
			
			if (numBtle == maxVotes)
			{
				btleWin = numWinners+1;
				numWinners++;
			}
			if (numTrad == maxVotes)
			{
				tradWin = numWinners+1;
				numWinners++;
			}
			if (numLand == maxVotes)
			{
				landWin = numWinners+1;
				numWinners++;
			}
			if (numLava == maxVotes)
			{
				lavaWin = numWinners+1;
				numWinners++;
			}
			if (numRand == maxVotes)
			{
				randWin = numWinners+1;
				numWinners++;
			}
			
			
			// Pick a map
			if (numWinners == 4) // if there was a 4-way tie
			{
				int rndMap = Random.Range (1,5); // pick a number from 1 to 4 (maps with a value of 0 can't be selected)
				
				if (rndMap == btleWin)
					selected = MapState.Btle;
				else if (rndMap == tradWin)
					selected = MapState.Trad;
				else if (rndMap == landWin)
					selected = MapState.Land;
				else if (rndMap == lavaWin)
					selected = MapState.Lava;
				else if (rndMap == randWin)
					selected = MapState.Rand;
			}
			else if (numWinners == 3) // if there was a 3-way tie
			{
				int rndMap = Random.Range (1,4); // pick a number from 1 to 3 (maps with a value of 0 can't be selected)
				
				if (rndMap == btleWin)
					selected = MapState.Btle;
				else if (rndMap == tradWin)
					selected = MapState.Trad;
				else if (rndMap == landWin)
					selected = MapState.Land;
				else if (rndMap == lavaWin)
					selected = MapState.Lava;
				else if (rndMap == randWin)
					selected = MapState.Rand;
			}
			else if (numWinners == 2) // if there was a 2-way tie
			{
				int rndMap = Random.Range (1,3); // pick a number from 1 to 2 (maps with a value of 0 can't be selected)
				
				if (rndMap == btleWin)
					selected = MapState.Btle;
				else if (rndMap == tradWin)
					selected = MapState.Trad;
				else if (rndMap == landWin)
					selected = MapState.Land;
				else if (rndMap == lavaWin)
					selected = MapState.Lava;
				else if (rndMap == randWin)
					selected = MapState.Rand;
			}
			else if (numWinners == 1) // if there was a clear winner
			{
				if (btleWin == 1)
					selected = MapState.Btle;
				else if (tradWin == 1)
					selected = MapState.Trad;
				else if (landWin == 1)
					selected = MapState.Land;
				else if (lavaWin == 1)
					selected = MapState.Lava;
				else if (randWin == 1)
					selected = MapState.Rand;
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
		
		// Update the tokens based on any key input, if the players haven't selected yet and they can move the tokens again
		if (P1Selected == false && P1canMove == true)
		{
			switch(P1State)
			{
				case MapState.Rand:
					if (Input.GetAxis(P1yAxis) < 0) // up
					{
						P1State = MapState.Trad;
						P1canMove = false;
					}
				    else if (Input.GetAxis(P1xAxis) > 0) // right
				    {
						P1State = MapState.Land;
						P1canMove = false;
					}
					break;	
				case MapState.Trad:	
					if (Input.GetAxis(P1yAxis) > 0) // down
					{
						P1State	= MapState.Land;
						P1canMove = false;
					}
					else if (Input.GetAxis(P1xAxis) > 0) // right\
					{
						P1State = MapState.Btle;
						P1canMove = false;
					}
					else if (Input.GetAxis(P1xAxis) < 0) // left
					{
						P1State = MapState.Rand;
						P1canMove = false;
					}
					break;
				case MapState.Btle:
					if (Input.GetAxis(P1yAxis) > 0) // down
					{
						P1State = MapState.Lava;
						P1canMove = false;
					}
					else if (Input.GetAxis(P1xAxis) < 0) // left
					{
						P1State = MapState.Trad;
						P1canMove = false;
					}
					break;	
				case MapState.Land:
					if (Input.GetAxis(P1yAxis) < 0) // up
					{
						P1State = MapState.Trad;
						P1canMove = false;
					}
					else if (Input.GetAxis(P1xAxis) > 0) // right
					{
						P1State = MapState.Lava;
						P1canMove = false;
					}
					else if (Input.GetAxis(P1xAxis) < 0) // left
					{
						P1State = MapState.Rand;
						P1canMove = false;
					}
					break;	
				case MapState.Lava:
					if (Input.GetAxis(P1yAxis) < 0) // up
					{
						P1State = MapState.Btle;
						P1canMove = false;
					}
					else if (Input.GetAxis(P1xAxis) < 0) // left
					{
						P1State = MapState.Land;
						P1canMove = false;
					}
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
		
		if (P2Selected == false && P2canMove == true)
		{
			switch(P2State)
			{
				case MapState.Rand:
					if (Input.GetAxis(P2yAxis) < 0) // up
					{
						P2State = MapState.Trad;
						P2canMove = false;
					}
					else if (Input.GetAxis(P2xAxis) > 0) // right
					{
						P2State = MapState.Land;
						P2canMove = false;
					}
					break;	
				case MapState.Trad:	
					if (Input.GetAxis(P2yAxis) > 0) // down
					{
						P2State	= MapState.Land;
						P2canMove = false;
					}
					else if (Input.GetAxis(P2xAxis) > 0) // right
					{
						P2State = MapState.Btle;
						P2canMove = false;
					}
					else if (Input.GetAxis(P2xAxis) < 0) // left
					{
						P2State = MapState.Rand;
						P2canMove = false;
					}
					break;
				case MapState.Btle:
					if (Input.GetAxis(P2yAxis) > 0) // down
					{
						P2State = MapState.Lava;
						P2canMove = false;
					}
					else if (Input.GetAxis(P2xAxis) < 0) // left
					{
						P2State = MapState.Trad;
						P2canMove = false;
					}
					break;	
				case MapState.Land:
					if (Input.GetAxis(P2yAxis) < 0) // up
					{
						P2State = MapState.Trad;
						P2canMove = false;
					}
					else if (Input.GetAxis(P2xAxis) > 0) // right
					{
						P2State = MapState.Lava;
						P2canMove = false;
					}
					else if (Input.GetAxis(P2xAxis) < 0) // left
					{
						P2State = MapState.Rand;
						P2canMove = false;
					}
					break;	
				case MapState.Lava:
					if (Input.GetAxis(P2yAxis) < 0) // up
					{
						P2State = MapState.Btle;
						P2canMove = false;
					}
					else if (Input.GetAxis(P2xAxis) < 0) // left
					{
						P2State = MapState.Land;
						P2canMove = false;
					}
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
		
		if (P3Playing && P3canMove == true)
		{
			if (P3Selected == false)
			{
				switch(P3State)
				{
					case MapState.Rand:
						if (Input.GetAxis(P3yAxis) < 0) // up
						{
							P3State = MapState.Trad;
							P3canMove = false;
						}
						else if (Input.GetAxis(P3xAxis) > 0) // right
						{
							P3State = MapState.Land;
							P3canMove = false;
						}
						break;	
					case MapState.Trad:	
						if (Input.GetAxis(P3yAxis) > 0) // down
						{
							P3State	= MapState.Land;
							P3canMove = false;
						}
						else if (Input.GetAxis(P3xAxis) > 0) // right
						{
							P3State = MapState.Btle;
							P3canMove = false;
						}
						else if (Input.GetAxis(P3xAxis) < 0) // left
						{
							P3State = MapState.Rand;
							P3canMove = false;
						}
						break;
					case MapState.Btle:
						if (Input.GetAxis(P3yAxis) > 0) // down
						{
							P3State = MapState.Lava;
							P3canMove = false;
						}
						else if (Input.GetAxis(P3xAxis) < 0) // left
						{
							P3State = MapState.Trad;
							P3canMove = false;
						}
						break;	
					case MapState.Land:
						if (Input.GetAxis(P3yAxis) < 0) // up
						{
							P3State = MapState.Trad;
							P3canMove = false;
						}
						else if (Input.GetAxis(P3xAxis) > 0) // right
						{
							P3State = MapState.Lava;
							P3canMove = false;
						}
						else if (Input.GetAxis(P3xAxis) < 0) // left
						{
							P3State = MapState.Rand;
							P3canMove = false;
						}
						break;	
					case MapState.Lava:
						if (Input.GetAxis(P3yAxis) < 0) // up
						{
							P3State = MapState.Btle;
							P3canMove = false;
						}
						else if (Input.GetAxis(P3xAxis) < 0) // left
						{
							P3State = MapState.Land;
							P3canMove = false;
						}
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
		
		if (P4Playing && P4canMove == true)
		{
			if (P4Selected == false)
			{
				switch(P4State)
				{
					case MapState.Rand:
						if (Input.GetAxis(P4yAxis) < 0) // up
						{
							P4State = MapState.Trad;
							P4canMove = false;
						}
						else if (Input.GetAxis(P4xAxis) > 0) // right
						{
							P4State = MapState.Land;
							P4canMove = false;
						}
						break;	
					case MapState.Trad:	
						if (Input.GetAxis(P4yAxis) > 0) // down
						{
							P4State	= MapState.Land;
							P4canMove = false;
						}
						else if (Input.GetAxis(P4xAxis) > 0) // right
						{
							P4State = MapState.Btle;
							P4canMove = false;
						}
						else if (Input.GetAxis(P4xAxis) < 0) // left
						{
							P4State = MapState.Rand;
							P4canMove = false;
						}
						break;
					case MapState.Btle:
						if (Input.GetAxis(P4yAxis) > 0) // down
						{
							P4State = MapState.Lava;
							P4canMove = false;
						}
						else if (Input.GetAxis(P4xAxis) < 0) // left
						{
							P4State = MapState.Trad;
							P4canMove = false;
						}
						break;	
					case MapState.Land:
						if (Input.GetAxis(P4yAxis) < 0) // up
						{
							P4State = MapState.Trad;
							P4canMove = false;
						}
						else if (Input.GetAxis(P4xAxis) > 0) // right
						{
							P4State = MapState.Lava;
							P4canMove = false;
						}
						else if (Input.GetAxis(P4xAxis) < 0) // left
						{
							P4State = MapState.Rand;
							P4canMove = false;
						}
						break;	
					case MapState.Lava:
						if (Input.GetAxis(P4yAxis) < 0) // up
						{
							P4State = MapState.Btle;
							P4canMove = false;
						}
						else if (Input.GetAxis(P4xAxis) < 0) // left
						{
							P4State = MapState.Land;
							P4canMove = false;
						}
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
		
		
		// Check for the back button being pressed
		if (Input.GetKeyUp(P1Back) || Input.GetKeyUp(P2Back) || (P3Playing && Input.GetKeyUp(P3Back)) || (P4Playing && Input.GetKeyUp(P4Back)))
			Application.LoadLevel("CharacterSelectionMenu");
	}
}
