using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
	//reservation table size
	public int tableSize;
	public Dictionary<Node,int[]> reservationTable;
	public Grid grid;


	void Awake(){
		grid = GameObject.Find ("Grid").GetComponent<Grid> ();
	}

	void Start () {
		
	}

	void Update () {

	}

	public void AstarFindPath(Vector3 startPosition, Vector3 endPosition){
		Node startNode = grid.NodeFromWorldPoint (startPosition);
		Node endNode = grid.NodeFromWorldPoint (endPosition);

		List<Node> closedList = new List<Node>();
		List<Node> openList = new List<Node>();

		openList.Add (startNode);

		while (openList.Count > 0) {
			Node currentNode = openList [0];
			for( int i = 1 ; i < openList.Count;i++){
				if(openList[i].fCost < currentNode.fCost || (openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)){
					currentNode = openList [i]; 
				}
			}
			openList.Remove (currentNode);
			closedList.Add (currentNode);

			if (currentNode == endNode) {
				return;
			}

			foreach (Node neighbor in grid.GetNeighbors(currentNode)) {
				if(!neighbor.Walkable || closedList.Contains(neighbor)){
					continue;
				}

			}

		}


	}

	public int Heuristic(Vector3 startPosition, Vector3 endPosition){
		Node startNode = grid.NodeFromWorldPoint (startPosition);
		Node endNode = grid.NodeFromWorldPoint (endPosition);

		return 0;
	}
}
