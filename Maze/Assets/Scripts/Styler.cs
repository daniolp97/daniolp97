using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Styler : MonoBehaviour
{
	public GUIStyle showPathStyle;
	public GUIStyle teleportStyle;
    public GUIStyle levitationStyle;
	public GUIStyle buyStyle;
	public GUIStyle goldAmount;
	public GUIStyle goldAdd;
	public GUIStyle playLevel;
	public GUIStyle restartLevel;
	public GUIStyle exitGame;
	public GUIStyle backStyle;
	public GUIStyle descriptionStyle;
	public GUIStyle loadingStyle;
	public GUIStyle hintsAmount;
	public GUIStyle hintsLevel;
	public GUIStyle deleteStyle;
	public GUIStyle yesStyle;
	public GUIStyle noStyle;
	public GUIStyle notifStyle;
	public GUIStyle skipStyle;
	public GUIStyle greyAlphaStyle;
	
	private int fontSize;
	
	[HideInInspector] public string pathDescription;
	[HideInInspector] public string teleportDescription;
	[HideInInspector] public string levitationDescription;
	
	void Update()
	{
		float sW = Screen.width;
		float sH = Screen.height;
		int fontSize = (int)Mathf.Floor(sW / 83f);
		SetFontSize(fontSize);
		SetDesc();
	}
	
	void SetFontSize(int newSize)
	{
		showPathStyle.fontSize = newSize;
		teleportStyle.fontSize = newSize;
		levitationStyle.fontSize = newSize;
		buyStyle.fontSize = newSize;
		goldAmount.fontSize = newSize;
		goldAdd.fontSize = newSize - 1;
		playLevel.fontSize = newSize;
		restartLevel.fontSize = newSize;
		exitGame.fontSize = newSize;
		backStyle.fontSize = newSize;
		descriptionStyle.fontSize = newSize;
		hintsAmount.fontSize = newSize;
		hintsLevel.fontSize = newSize;
		loadingStyle.fontSize = newSize;
		deleteStyle.fontSize = newSize;
		notifStyle.fontSize = newSize + 2;
	}
	
	void SetDesc()
	{
		pathDescription = "Pather is finding the shortest path form player to maze exit, \nis will show you part of the way, patcher level will increase how much of the way will it show.";
		teleportDescription = "Teleport, just like it sounds is teleporting player to random cell \nthat is 75% (default, teleport level decreasing it) of distance from player to maze exit.";
		levitationDescription = "Levitation allows you to move over destroyed floor, default is under 10 seconds, \nlevel increasing time. Be aware to not step over destroyed floor when levitation is switching off.";
	}
	
}
