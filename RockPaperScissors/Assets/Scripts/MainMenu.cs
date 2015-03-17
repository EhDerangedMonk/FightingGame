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
		
		// Initialize the logo location
		int logoWidth = (int)(0.35*Screen.width);
		int logoHeight = (int) ((float)logoTexture.height * ((float)logoWidth/(float)logoTexture.width));
		int logoXposition = (int) (0.6*Screen.width);
		
		logoRect = new Rect(logoXposition, vertSpacing, logoWidth, logoHeight);
	}
	
	
	
	/*******\
	* ONGUI *
	\*******/
    void OnGUI()
    {
		// Check to see if the mouse is in a button
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
        
    }
    
}
