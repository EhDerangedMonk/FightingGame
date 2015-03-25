/****************************************************************************\
*	Author:		Braden Gibson											     *
*	Purpose:	Attaches to main camera. Allows selection of options through *
*				use of the mouse.										 	 *
\****************************************************************************/
using UnityEngine;
using System.Collections;

// TODO: make the buttons and logo scale to the screen size

public class MainMenu : MonoBehaviour {

    // Button Textures
    public Texture playButtonTexture;
    public Texture controlsButtonTexture;
    public Texture creditsButtonTexture;
    public Texture exitButtonTexture;
    
	// Rectangles that define the buttons' locations
    private Rect playRect; 
    private Rect playRectHover;
   	private Rect controlsRect;
   	private Rect controlsRectHover;
   	private Rect creditsRect;
   	private Rect creditsRectHover;
   	private Rect exitRect; 
   	private Rect exitRectHover;
   	
   	// Flags to determine which button is being hovered over (if any)
   	private bool playHover;
   	private bool controlsHover;
   	private bool creditsHover;
   	private bool exitHover;
   	
   	// Display the logo
   	public Texture logoTexture;
   	private Rect logoRect;
   	
   	// Keep track of if the mouse has focus (if it doesn't then the controller does)
   	private Vector3 lastMousePosition; // determine if the mouse moved from it's last position
   	private bool mouseActive;
   	enum MenuState {Play, Controls, Credits, Exit};
   	private MenuState controllerState;
   	
   	// Player controller inputs 
    private KeyCode P1Enter;
    private string P1yAxis;
	private KeyCode P2Enter;
	private string P2yAxis;
	private KeyCode P3Enter;
	private string P3yAxis;
	private KeyCode P4Enter;
	private string P4yAxis;
	
	private bool canMove; // slows down controller input. It sets when a player uses a controller button and resets when they release it.
	
	// Determine if players 3 and 4 are playing
	private bool P3Playing;
	private bool P4Playing;
    


	/*******\
	* START *
	\*******/
	void Start()
	{
		// Initialize the buttons
		int buttonHeight = (int) (0.15 * Screen.height); 
		int buttonWidth = (int) ((float)playButtonTexture.width * ((float)buttonHeight / (float)playButtonTexture.height)); //Adjust the width to the height
		int vertSpacing = (int) (0.08 * Screen.height);
		int buttonXlocation = ((int)(0.3*Screen.width))-buttonWidth;
		int buttonXlocationHover = ((int)(0.4*Screen.width))-buttonWidth;
		
		playRect = new Rect (buttonXlocation, vertSpacing, buttonWidth, buttonHeight);
		controlsRect = new Rect (buttonXlocation, 2*vertSpacing + buttonHeight, buttonWidth, buttonHeight);
		creditsRect = new Rect (buttonXlocation, 3*vertSpacing + 2*buttonHeight, buttonWidth, buttonHeight);
		exitRect = new Rect (buttonXlocation, 4*vertSpacing + 3*buttonHeight, buttonWidth, buttonHeight);
		
		playRectHover = new Rect (buttonXlocationHover, vertSpacing, buttonWidth, buttonHeight);
		controlsRectHover = new Rect (buttonXlocationHover, 2*vertSpacing + buttonHeight, buttonWidth, buttonHeight);
		creditsRectHover = new Rect (buttonXlocationHover, 3*vertSpacing + 2*buttonHeight, buttonWidth, buttonHeight);
		exitRectHover = new Rect (buttonXlocationHover, 4*vertSpacing + 3*buttonHeight, buttonWidth, buttonHeight);
		
		// Set the flags
		playHover = false;
		controlsHover = false;
		creditsHover = false;
		exitHover = false;
		
		mouseActive = false;
		lastMousePosition = Input.mousePosition;
		controllerState = MenuState.Play;
		
		canMove = true;
		
		// Initialize the logo location
		int logoWidth = (int)(0.35*Screen.width);
		int logoHeight = (int) ((float)logoTexture.height * ((float)logoWidth/(float)logoTexture.width));
		int logoXposition = (int) (0.6*Screen.width);
		
		logoRect = new Rect(logoXposition, vertSpacing, logoWidth, logoHeight);
		
		
		
		// Initialize the controller layouts
		InitializeStorage settings = (InitializeStorage) GameObject.Find("VariableStorage").GetComponent(typeof(InitializeStorage));
		switch ((int)settings.P1Controller)
		{
		case 1:
			P1Enter = KeyCode.Space;
			P1yAxis = "Keyboard1Y";
			break;
		case 2:
			P1Enter = KeyCode.Return;
			P1yAxis = "Keyboard2Y";
			break;
		case 3:
			P1Enter = KeyCode.Joystick1Button0;
			P1yAxis = "LeftJoystick1Y";
			break;
		case 4:
			P1Enter = KeyCode.Joystick1Button0;
			P1yAxis = "LeftJoystick2Y";
			break;
		case 5:
			P1Enter = KeyCode.Joystick1Button0;
			P1yAxis = "LeftJoystick3Y";
			break;
		case 6:
			P1Enter = KeyCode.Joystick1Button0;
			P1yAxis = "LeftJoystick4Y";
			break;
		}
		
		switch ((int)settings.P2Controller)
		{
		case 1:
			P2Enter = KeyCode.Space;
			P2yAxis = "Keyboard1Y";
			break;
		case 2:
			P2Enter = KeyCode.Return;
			P2yAxis = "Keyboard2Y";
			break;
		case 3:
			P2Enter = KeyCode.Joystick1Button0;
			P2yAxis = "LeftJoystick1Y";
			break;
		case 4:
			P2Enter = KeyCode.Joystick1Button0;
			P2yAxis = "LeftJoystick2Y";
			break;
		case 5:
			P2Enter = KeyCode.Joystick1Button0;
			P2yAxis = "LeftJoystick3Y";
			break;
		case 6:
			P2Enter = KeyCode.Joystick1Button0;
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
			P3yAxis = "Keyboard1Y";
			break;
		case 2:
			P3Enter = KeyCode.Return;
			P3yAxis = "Keyboard2Y";
			break;
		case 3:
			P3Enter = KeyCode.Joystick1Button0;
			P3yAxis = "LeftJoystick1Y";
			break;
		case 4:
			P3Enter = KeyCode.Joystick1Button0;
			P3yAxis = "LeftJoystick2Y";
			break;
		case 5:
			P3Enter = KeyCode.Joystick1Button0;
			P3yAxis = "LeftJoystick3Y";
			break;
		case 6:
			P3Enter = KeyCode.Joystick1Button0;
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
			P4yAxis = "Keyboard1Y";
			break;
		case 2:
			P4Enter = KeyCode.Return;
			P4yAxis = "Keyboard2Y";
			break;
		case 3:
			P4Enter = KeyCode.Joystick1Button0;
			P4yAxis = "LeftJoystick1Y";
			break;
		case 4:
			P4Enter = KeyCode.Joystick1Button0;
			P4yAxis = "LeftJoystick2Y";
			break;
		case 5:
			P4Enter = KeyCode.Joystick1Button0;
			P4yAxis = "LeftJoystick3Y";
			break;
		case 6:
			P4Enter = KeyCode.Joystick1Button0;
			P4yAxis = "LeftJoystick4Y";
			break;
		}
	}
	
	
	
	/********\
	* UPDATE *
	\********/
	void Update()
	{
		Vector3 currentMousePosition = Input.mousePosition;
		
		// if the mouse has moved, assign it focus
		if (currentMousePosition.x!=lastMousePosition.x || currentMousePosition.y!=lastMousePosition.y)
			mouseActive = true;
			
		// if the controller made input, remove focus from the mouse
		if (Input.GetAxis(P1yAxis)!=0 || Input.GetAxis(P2yAxis)!=0 || (P3Playing && Input.GetAxis(P3yAxis)!=0) || (P4Playing && Input.GetAxis(P4yAxis)!=0))
		{
			mouseActive = false;
			
			if (canMove)
			{
				// adjust which option is being selected
				if (controllerState == MenuState.Play)
				{
					// down
					if (Input.GetAxis(P1yAxis)>0 || Input.GetAxis(P2yAxis)>0 || (P3Playing && Input.GetAxis(P3yAxis)>0) || (P4Playing && Input.GetAxis(P4yAxis)>0))
					{
						controllerState = MenuState.Controls;
					}
				}
				else if (controllerState == MenuState.Controls)
				{
					// up
					if (Input.GetAxis(P1yAxis)<0 || Input.GetAxis(P2yAxis)<0 || (P3Playing && Input.GetAxis(P3yAxis)<0) || (P4Playing && Input.GetAxis(P4yAxis)<0))
					{
						controllerState = MenuState.Play;
					}
					// down
					if (Input.GetAxis(P1yAxis)>0 || Input.GetAxis(P2yAxis)>0 || (P3Playing && Input.GetAxis(P3yAxis)>0) || (P4Playing && Input.GetAxis(P4yAxis)>0))
					{
						controllerState = MenuState.Credits;
					}
				}
				else if (controllerState == MenuState.Credits)
				{
					// up
					if (Input.GetAxis(P1yAxis)<0 || Input.GetAxis(P2yAxis)<0 || (P3Playing && Input.GetAxis(P3yAxis)<0) || (P4Playing && Input.GetAxis(P4yAxis)<0))
					{
						controllerState = MenuState.Controls;
					}
					// down
					if (Input.GetAxis(P1yAxis)>0 || Input.GetAxis(P2yAxis)>0 || (P3Playing && Input.GetAxis(P3yAxis)>0) || (P4Playing && Input.GetAxis(P4yAxis)>0))
					{
						controllerState = MenuState.Exit;
					}
				}
				else if (controllerState == MenuState.Exit)
				{
					// up
					if (Input.GetAxis(P1yAxis)<0 || Input.GetAxis(P2yAxis)<0 || (P3Playing && Input.GetAxis(P3yAxis)<0) || (P4Playing && Input.GetAxis(P4yAxis)<0))
					{
						controllerState = MenuState.Credits;
					}
				}
			}
			
			canMove = false;
		}
		
		// Reset the controller's ability to move
		if (Input.GetAxis(P1yAxis)==0 && Input.GetAxis(P2yAxis)==0 && ((P3Playing && Input.GetAxis(P3yAxis)==0) || !P3Playing) && ((P4Playing && Input.GetAxis(P4yAxis)==0) || !P4Playing))
		{
			canMove = true;
		}
		
		lastMousePosition = currentMousePosition;
	}
	
	
	
	/*******\
	* ONGUI *
	\*******/
    void OnGUI()
    {
		// Check to see if the mouse is in a button
		if (mouseActive)
		{
			if (playHover == false && playRect.Contains(Event.current.mousePosition))
			{
				playHover = true;
			}
			if (playHover == true && !playRectHover.Contains(Event.current.mousePosition))
			{
				playHover = false;
			}
			
			if (controlsHover == false && controlsRect.Contains (Event.current.mousePosition))
			{
				controlsHover = true;
			}
			if (controlsHover == true && !controlsRectHover.Contains(Event.current.mousePosition))
			{
				controlsHover = false;
			}
			
			if (creditsHover == false && creditsRect.Contains(Event.current.mousePosition))
			{
				creditsHover = true;
			}
			if (creditsHover == true && !creditsRectHover.Contains(Event.current.mousePosition))
			{
				creditsHover = false;
			}
			
			if (exitHover == false && exitRect.Contains(Event.current.mousePosition))
			{
				exitHover = true;
			}
			
			if (exitHover == true && !exitRectHover.Contains(Event.current.mousePosition))
			{
				exitHover = false;
			}
		}
		else // Controller selection
		{
			if (controllerState == MenuState.Play)
			{
				playHover = true;
				controlsHover = false;
				creditsHover = false;
				exitHover = false;
			}
			else if (controllerState == MenuState.Controls)
			{
				playHover = false;
				controlsHover = true;
				creditsHover = false;
				exitHover = false;
			}
			else if (controllerState == MenuState.Credits)
			{
				playHover = false;
				controlsHover = false;
				creditsHover = true;
				exitHover = false;
			}
			else if (controllerState == MenuState.Exit)
			{
				playHover = false;
				controlsHover = false;
				creditsHover = false;
				exitHover = true;
			}
		}
		
		
        // Display the buttons
        if (playHover)
        {
	        if (GUI.Button (playRectHover, playButtonTexture, ""))
	        {
	            print ("Clicked PLAY");
	            
	            // Load the character selection menu
	            Application.LoadLevel("CharacterSelectionMenu");
	        }
	    }
	    else
	    {
			if (GUI.Button (playRect, playButtonTexture, ""))
			{
				print ("Clicked PLAY");
				
				// Load the character selection menu
				Application.LoadLevel("CharacterSelectionMenu");
			}
	    }

		if (controlsHover)
		{
	        if (GUI.Button (controlsRectHover, controlsButtonTexture, ""))
	        {
	            print ("Clicked CONTROLS");
	            
	            Application.LoadLevel("Controls");
	        }
	    }
	    else
	    {
			if (GUI.Button (controlsRect, controlsButtonTexture, ""))
			{
				print ("Clicked CONTROLS");
				
				Application.LoadLevel("Controls");
			}
	    }
	    
		if (creditsHover)
		{
			if (GUI.Button (creditsRectHover, creditsButtonTexture, ""))
			{
				print ("Clicked CREDITS");
				
				// Load the credits scene
				Application.LoadLevel ("Credits");
			}
		}
		else
		{
			if (GUI.Button (creditsRect, creditsButtonTexture, ""))
			{
				print ("Clicked CREDITS");
				
				// Load the credits scene
				Application.LoadLevel ("Credits");
			}
		}

		if (exitHover)
		{
	        if (GUI.Button (exitRectHover, exitButtonTexture, ""))
	        {
	            print ("Clicked EXIT");
	            
	            Application.Quit();
	        }
	    }
	    else
	    {
			if (GUI.Button (exitRect, exitButtonTexture, ""))
			{
				print ("Clicked EXIT");
				
				Application.Quit();
			}
	    }
	    
	    
	    // Draw the logo
	    GUI.DrawTexture(logoRect, logoTexture);
	    
	    
	    
	    // Determine if a controller was used to select an icon
	    if (mouseActive == false)
	    {
	    	if (Input.GetKeyDown(P1Enter) || Input.GetKeyDown(P2Enter) || (P3Playing && Input.GetKeyDown(P3Enter)) || (P4Playing && Input.GetKeyDown(P4Enter)))
	    	{
	    		if (controllerState == MenuState.Play)
	    		{
					print ("Clicked PLAY");
					
					// Load the character selection menu
					Application.LoadLevel("CharacterSelectionMenu");
	    		}
	    		else if (controllerState == MenuState.Controls)
	    		{
					print ("Clicked CONTROLS");
					
					Application.LoadLevel("Controls");
	    		}
	    		else if (controllerState == MenuState.Credits)
	    		{
					print ("Clicked CREDITS");
					
					// Load the credits scene
					Application.LoadLevel ("Credits");
	    		}
	    		else if (controllerState == MenuState.Exit)
	    		{
					print ("Clicked EXIT");
					
					Application.Quit();
	    		}
	    	}
	    }
        
    }
    
}
