using System;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[Serializable]
public class Player{
	[SerializeField]
	string name;
	[SerializeField]
	int id;
	[SerializeField]
	int level;
	[SerializeField]
	int exp;
	[SerializeField]
	GridMap gridMap;


	public Player (){
		
	}
}


