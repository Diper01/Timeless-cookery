using UnityEngine;
using System.Collections;
using UnityEditor;

public class DebugGame : EditorWindow {

	[MenuItem("Debug/Data/DeleteAllData", false, 0)]
	static public void DeleteAllData(){
		PlayerPrefs.DeleteAll();
	} 
	[MenuItem("Debug/Data/X2TimeScale", false, 0)]
	static public void X2TimeScale(){
		Time.timeScale *= 2;
	} 
	[MenuItem("Debug/Data/-X2TimeScale", false, 0)]
	static public void X1Division2TimeScale(){
		Time.timeScale /= 2;
	}  
	[MenuItem("Debug/Data/RestoreTimeScale", false, 0)]
	static public void RestoreTimeScale(){
		Time.timeScale = 1;
	}  
	[MenuItem("Debug/Audio/On", false, 0)]
	static public void TurnOnAudio(){
		AudioListener.volume = 1F;
	}  
	
}
