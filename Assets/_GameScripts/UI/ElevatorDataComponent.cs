﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDataComponent : MonoBehaviour 
{
	[System.Serializable]
	public struct ElevatorData 
	{
		public string RoomName;
		public bool CanAccess;
		public string SceneName;
	}

	public float PopupOffsetX;
	public float PopupOffsetY;
	public float BoxWidth = 100;
	public float ArrowOffset = 15;
    public Texture2D Arrow;
	//public GUIStyle menuStyle;
    public GUISkin menuSkin;

	public ElevatorData[] DataArray;
}
