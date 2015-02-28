/****************************************************************************\
*	Author:		Braden Gibson											     *
*	Purpose:	Attaches to main camera. Allows selection of options through *
*				use of the mouse.										 	 *
\****************************************************************************/
using UnityEngine;
using System.Collections;

// TODO: make the buttons and logo scale to the screen size

public class MainMenu : MonoBehaviour {

    public float ButtonLocationX; // Horizontal offset of the button, in pixels
    public float PlayButtonLocationY; // Vertical location by screen percentage
    public float OptionsButtonLocationY;
    public float ExitButtonLocationY;
    
    public Texture playButtonTexture;
    public Texture optionsButtonTexture;
    public Texture exitButtonTexture;
    
	// Rectangles that define the buttons' locations
    private Rect playRect; 
    private Rect playRectHover;
   	private Rect optionsRect;
   	private Rect optionsRectHover;
   	private Rect exitRect; 
   	private Rect exitRectHover;
   	
   	// Flags to determine which button is being hovered over (if any)
   	private bool playHover;
   	private bool optionsHover;
   	private bool exitHover;
    


	/*******\
	* START *
	\*******/
	void Start()
	{
		// Initialize the buttons
		playRect = new Rect (ButtonLocationX, Screen.height * PlayButtonLocationY, playButtonTexture.width, playButtonTexture.height);
		optionsRect = new Rect (ButtonLocationX, Screen.height * OptionsButtonLocationY, optionsButtonTexture.width, optionsButtonTexture.height);
		exitRect = new Rect (ButtonLocationX, Screen.height * ExitButtonLocationY, optionsButtonTexture.width, optionsButtonTexture.height);
		
		playRectHover = new Rect (ButtonLocationX+100, Screen.height * PlayButtonLocationY, playButtonTexture.width, playButtonTexture.height);
		optionsRectHover = new Rect (ButtonLocationX+100, Screen.height * OptionsButtonLocationY, optionsButtonTexture.width, optionsButtonTexture.height);
		exitRectHover = new Rect (ButtonLocationX+100, Screen.height * ExitButtonLocationY, optionsButtonTexture.width, optionsButtonTexture.height);
		
		// Set the flags
		playHover = false;
		optionsHover = false;
		exitHover = false;
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
		if (optionsHover == false && optionsRect.Contains (Event.current.mousePosition))
		{
			optionsHover = true;
		}
		if (optionsHover == true && !optionsRectHover.Contains(Event.current.mousePosition))
		{
			optionsHover = false;
		}
		if (exitHover == false && exitRect.Contains(Event.current.mousePosition))
		{
			exitHover = true;
		}
		if (exitHover == true && !exitRectHover.Contains (Event.current.mousePosition))
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

		if (optionsHover)
		{
	        if (GUI.Button (optionsRectHover, optionsButtonTexture, ""))
	        {
	            print ("Clicked OPTIONS");
	            
				// Load the credits (for now) TODO: add a dedicated credits button
				Application.LoadLevel("Credits");
	        }
	    }
	    else
	    {
			if (GUI.Button (optionsRect, optionsButtonTexture, ""))
			{
				print ("Clicked OPTIONS");
				
				// Load the credits (for now) 
				Application.LoadLevel("Credits");
			}
	    }

		if (exitHover)
		{
	        if (GUI.Button (exitRectHover, exitButtonTexture, ""))
	        {
	            print ("Clicked EXIT");
	            // TODO: Make the exit button quit the application
	        }
	    }
	    else
	    {
			if (GUI.Button (exitRect, exitButtonTexture, ""))
			{
				print ("Clicked EXIT");
			}
	    }
        
        
        
        
    }
    
    
}
