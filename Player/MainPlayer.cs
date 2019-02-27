﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour {

	public Transform follow;
	public Texture2D cursor;

	void Start () {
		Debug.Log("REmember set cursor in MainPlayer");
		Cursor.SetCursor(cursor,new Vector2(16,16),CursorMode.ForceSoftware);
	}
	
	void Update () {
		Vector3 pos = follow.position;
		pos.y = 0;
		transform.position = pos;

		if (Input.GetKeyUp(KeyCode.R))
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label(new Rect(100, 20, 250, 25), "Press R to restart");
		GUI.Label(new Rect(100, 40, 250, 25), "Press F to toggle view mode");
		GUI.Label(new Rect(100, 60, 250, 25), "Press Space to toggle target lock mode");


	}


}
