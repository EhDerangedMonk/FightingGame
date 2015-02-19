/// <summary>
/// Character Selection Menu.
/// Attaches to main camera.
/// </summary>
using UnityEngine;
using System.Collections;

public class CharacterSelectionMenu : MonoBehaviour {
	
	public Texture NoirSelectionButton;
	public Texture ZakirSelectionButton;
	public Texture VioletSelectionButton;
	public Texture CharacterSelectionTitle;
	
	private Rect NoirRect;
	private Rect ZakirRect;
	private Rect VioletRect;
	private Rect CharacterSelectionRect;
	
	
	/*******\
	* START *
	\*******/
	void Start()
	{
		int buttonWidth = Screen.width * 2/7;
		int buttonHeight = (int) (buttonWidth * 0.7125); // The height is multiplied by 0.7125 to match the aspect ratio of the button to 16:9
		int vertSpacing = (Screen.height - 2*buttonHeight)/3;
		int horzSpacing = (Screen.width - 2*buttonWidth)/3;
		print (horzSpacing.ToString() + "     " + vertSpacing.ToString());
	
		// Initialize the buttons' rectangles
		CharacterSelectionRect = new Rect(horzSpacing, vertSpacing, buttonWidth, buttonHeight);
		VioletRect = new Rect(horzSpacing, buttonHeight + 2*vertSpacing, buttonWidth, buttonHeight);
		ZakirRect = new Rect(buttonWidth + 2*horzSpacing, vertSpacing, buttonWidth, buttonHeight);
		NoirRect = new Rect(buttonWidth + 2*horzSpacing, buttonHeight + 2*vertSpacing, buttonWidth, buttonHeight);
		
	} 
	
	/*******\
	* ONGUI *
	\*******/
	void OnGUI()
	{
		// Draw the buttons, adjusted for screen width
		GUI.DrawTexture(CharacterSelectionRect, CharacterSelectionTitle, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(VioletRect, VioletSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(ZakirRect, ZakirSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(NoirRect, NoirSelectionButton, ScaleMode.ScaleToFit, true);
	}
}
