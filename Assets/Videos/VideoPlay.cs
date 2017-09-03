using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlay : MonoBehaviour {
	//电影纹理
	public MovieTexture movTexture;

	void Awake()
	{
		movTexture.Play ();
	}
	void OnGUI()
	{
		//绘制电影纹理
		GUI.DrawTexture (new Rect (0,0, Screen.width, Screen.height),movTexture,ScaleMode.StretchToFill);  
	}

}