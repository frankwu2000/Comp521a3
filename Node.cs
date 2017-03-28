using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
	public int gCost;
	public int hCost;
	public int gridX;
	public int gridY;

	public bool Walkable;
	public Vector3 WorldPosition;
	public float TimeLayer;

	public Node(bool walkable, Vector3 WorldPosition, int _gridX, int _gridY){
		this.Walkable = walkable;
		this.WorldPosition = WorldPosition;
		this.gridX = _gridX;
		this.gridY = _gridY;
	}

	public Node(bool walkable, Vector3 WorldPosition,float TimeLayer){
		this.Walkable = walkable;
		this.WorldPosition = WorldPosition;
		this.TimeLayer = TimeLayer;
	}

	public int fCost{
		return gCost + hCost;
	}
}
