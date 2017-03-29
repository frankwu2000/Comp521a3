using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Grid {
	public LayerMask UnwalkableMask;
	public Vector2 GridWorldSize;
	public float NodeRadius;
	public Node[,] grid;

	float NodeDiameter;
	public int GridSizex,GridSizey;

	public Grid(){
		
	}

	public void CreateGrid(){
		NodeRadius = 0.5f;
		GridWorldSize = new Vector2 (51,31);
		NodeDiameter = NodeRadius * 2;
		GridSizex = Mathf.RoundToInt (GridWorldSize.x/NodeDiameter);
		GridSizey = Mathf.RoundToInt (GridWorldSize.y/NodeDiameter);


		grid = new Node[GridSizex,GridSizey];
		Vector3 WorldBottomLeft = Vector3.zero - Vector3.right * GridWorldSize.x/2 - Vector3.forward * GridWorldSize.y/2;

		for(int x = 0 ;x < GridSizex;x++){
			for(int y = 0 ; y < GridSizey;y++){
				Vector3 WorldPoint = WorldBottomLeft + Vector3.right * (x*NodeDiameter+NodeRadius) + Vector3.forward * (y*NodeDiameter+NodeRadius);

				bool Walkable = !(Physics.CheckSphere (WorldPoint, NodeRadius));
				grid [x, y] = new Node(Walkable,WorldPoint,x,y);
			}
		}
	}

//	List<Node> path = new List<Node> ();

//	//gizmos for dubugging
//	void OnDrawGizmos(){
//		Gizmos.color = Color.cyan;
//		Gizmos.DrawWireCube(Vector3.zero,new Vector3(GridWorldSize.x,0,GridWorldSize.y));
//
////		Debug.Log ("pathcount: "+ path.Count);
//		
//
//		if(grid != null) {
//			foreach(Node node in grid){
//				Gizmos.color = (node.Walkable)?Color.cyan:Color.red;
////				if (path.Contains (node)) {
////					Gizmos.color = Color.blue;
////				}
//
//				Gizmos.DrawCube (node.WorldPosition,new Vector3(1,3,1) * (NodeDiameter));
//			}
//
//		}
//	}

	public List<Node> GetNeighbors(Node node){
		List<Node> neighbors = new List<Node> ();

		for(int x = -1 ; x<=1 ; x++){
			for(int y = -1; y<=1 ; y++){
				if(x == 0 && y == 0 ){
					continue;
				}
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				if(checkX >= 0 && checkX <GridSizex &&checkY>= 0 && checkY<GridSizey){
					neighbors.Add (grid[checkX,checkY]);
				}
			}
		}
		return neighbors;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + GridWorldSize.x/2) / GridWorldSize.x;
		float percentY = (worldPosition.z + GridWorldSize.y/2) / GridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((GridSizex-1) * percentX);
		int y = Mathf.RoundToInt((GridSizey-1) * percentY);
		return grid[x,y];
	}




}
