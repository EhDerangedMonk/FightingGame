/// <summary>
/// Main menu.
/// Attaches to main camera.
/// </summary>
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public float ButtonLocationX;
    public float PlayButtonLocationY;
    public float OptionsButtonLocationY;
    public float ExitButtonLocationY;
    
    public Texture playButtonTexture;
    public Texture optionsButtonTexture;
    public Texture exitButtonTexture;

    void OnGUI()
    {
        // Display the buttons
        if (GUI.Button (new Rect (ButtonLocationX, Screen.height * PlayButtonLocationY, playButtonTexture.width, playButtonTexture.height), playButtonTexture, ""))
        {
            print ("Clicked PLAY");
            
            // Load the map selection menu
            Application.LoadLevel("CharacterSelectionMenu");
        }

        if (GUI.Button (new Rect (ButtonLocationX, Screen.height * OptionsButtonLocationY, optionsButtonTexture.width, optionsButtonTexture.height), optionsButtonTexture, ""))
        {
            print ("Clicked OPTIONS");
        }

        if (GUI.Button (new Rect (ButtonLocationX, Screen.height * ExitButtonLocationY, optionsButtonTexture.width, optionsButtonTexture.height), exitButtonTexture, ""))
        {
            print ("Clicked EXIT");
        }
    }
}
