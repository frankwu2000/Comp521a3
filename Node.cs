using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
	public int gCost;
	public int hCost;
	public int gridX;
	public int gridY;
	public Node parentNode;

	public bool Walkable;
	public Vector3 WorldPosition;
	public float TimeLayer;

	public Node(bool walkable, Vector3 WorldPosition, int _gridX, int _gridY){
		this.Walkable = walkable;
		this.WorldPosition = WorldPosition;
		this.gridX = _gridX;
		this.gridY = _gridY;
	}

	public Node(bool walkable, Vector3 WorldPosition){
		this.Walkable = walkable;
		this.WorldPosition = WorldPosition;
	}

	public int fCost{
		get{
			return gCost + hCost;
		}
	}

	public bool NodeEquals (Node obj)
	{
		if(obj.WorldPosition.x == WorldPosition.x &&
			obj.WorldPosition.y == WorldPosition.y &&
			obj.WorldPosition.z == WorldPosition.z){
			return true;
		}
		return false;
	}

}
