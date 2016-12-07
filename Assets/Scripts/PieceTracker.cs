using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PieceTracker  {
	[SerializeField]
	int basePiece = 0;
	[SerializeField]
	int starPoint = 0;
	[SerializeField]
	int endPoint = 0;
	[SerializeField]
	int chest= 0;


	public bool hasBasicSetup(){
		return chest > 0 && starPoint > 0 && endPoint > 0;
	}

	public void Add(Pieces piece){
		switch (piece) {
		case Pieces.Base:
			basePiece++;
			break;
		case Pieces.StartPoint:
			starPoint++;
			break;
		case Pieces.EndPoint:
			endPoint++;
			break;
		case Pieces.Chest:
			chest++;
			break;
		default:
			break;
			
		}
	}

	public void Remove(Pieces piece){
		switch (piece) {
		case Pieces.Base:
			basePiece--;
			break;
		case Pieces.StartPoint:
			starPoint--;
			break;
		case Pieces.EndPoint:
			endPoint--;
			break;
		case Pieces.Chest:
			chest--;
			break;
		default:
			break;

		}
	}
	public int Count(Pieces piece){
		switch (piece) {
		case Pieces.Base:
			return basePiece;
		case Pieces.StartPoint:
			return starPoint;
		case Pieces.EndPoint:
			return endPoint;
		case Pieces.Chest:
			return chest;
		default:
			return -1;

		}
	}

}
