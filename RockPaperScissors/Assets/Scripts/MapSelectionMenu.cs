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
		// If both players have selected, determine which map to load and do so
		if (P1Selected == true && P2Selected == true)
		{
			MapState selected; // Stores whatever the voted map is
			// Determine what the voted map is
			if (P1State == P2State) // If they voted for the time thing
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
			
			
			
			// Right now it will just load whatever player 1 chose
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
					if (Input.GetKeyUp(KeyCode.W))
						P1State = MapState.Trad;
				    else if (Input.GetKeyUp(KeyCode.D))
				    	P1State = MapState.Land;
					break;	
				case MapState.Trad:	
					if (Input.GetKeyUp(KeyCode.S))
						P1State	= MapState.Land;
					else if (Input.GetKeyUp(KeyCode.D))
						P1State = MapState.Btle;
					else if (Input.GetKeyUp(KeyCode.A))
						P1State = MapState.Rand;
					break;
				case MapState.Btle:
					if (Input.GetKeyUp(KeyCode.S))
						P1State = MapState.Lava;
					else if (Input.GetKeyUp(KeyCode.A))
						P1State = MapState.Trad;
					break;	
				case MapState.Land:
					if (Input.GetKeyUp(KeyCode.W))
						P1State = MapState.Trad;
					else if (Input.GetKeyUp(KeyCode.D))
						P1State = MapState.Lava;
					else if (Input.GetKeyUp(KeyCode.A))
						P1State = MapState.Rand;
					break;	
				case MapState.Lava:
					if (Input.GetKeyUp(KeyCode.W))
						P1State = MapState.Btle;
					else if (Input.GetKeyUp(KeyCode.A))
						P1State = MapState.Land;
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
			case MapState.Rand:
				if (Input.GetKeyUp(KeyCode.I))
					P2State = MapState.Trad;
				else if (Input.GetKeyUp(KeyCode.L))
					P2State = MapState.Land;
				break;	
			case MapState.Trad:	
				if (Input.GetKeyUp(KeyCode.K))
					P2State	= MapState.Land;
				else if (Input.GetKeyUp(KeyCode.L))
					P2State = MapState.Btle;
				else if (Input.GetKeyUp(KeyCode.J))
					P2State = MapState.Rand;
				break;
			case MapState.Btle:
				if (Input.GetKeyUp(KeyCode.K))
					P2State = MapState.Lava;
				else if (Input.GetKeyUp(KeyCode.J))
					P2State = MapState.Trad;
				break;	
			case MapState.Land:
				if (Input.GetKeyUp(KeyCode.I))
					P2State = MapState.Trad;
				else if (Input.GetKeyUp(KeyCode.L))
					P2State = MapState.Lava;
				else if (Input.GetKeyUp(KeyCode.J))
					P2State = MapState.Rand;
				break;	
			case MapState.Lava:
				if (Input.GetKeyUp(KeyCode.I))
					P2State = MapState.Btle;
				else if (Input.GetKeyUp(KeyCode.J))
					P2State = MapState.Land;
				break;
			}
		}
		// Check for the selection key being pressed
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
