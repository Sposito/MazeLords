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
	int id = 0;
	[SerializeField]
	int level = 1;
	[SerializeField]
	int exp = 0;
	[SerializeField]
	GridMap gridMap;

	[SerializeField]
	float energy;
	public float Energy {get{ return energy; }}

	public Player (){
		
	}
	public Player (string name, float energy){
		this.energy = energy;
		this.name = name;
	}

	public static Player LoadCurrentPlayer(){
		return new Player ("Link", 30f);
	}
}