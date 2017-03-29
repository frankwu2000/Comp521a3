using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
	//reservation table size
	//public int tableSize;
//	public Dictionary<Node,int[]> reservationTable;
	public Grid grid;

	public Transform seeker, target;

	void Awake(){
		grid = gameObject.GetComponent<Grid> ();
	}

	void Start () {
		
	}

	void Update () {
		AstarFindPath (seeker.position, target.position);
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
				RetracePath (startNode,endNode);
				return;
			}

			foreach (Node neighbor in grid.GetNeighbors(currentNode)) {
				if(!neighbor.Walkable || closedList.Contains(neighbor)){
					continue;
				}
				int newMovementCost = currentNode.gCost + GetDistance (currentNode,neighbor);
				if(newMovementCost < neighbor.gCost || !openList.Contains(neighbor)){
					neighbor.gCost = newMovementCost;
					neighbor.hCost = GetDistance (neighbor, endNode);
					neighbor.parentNode = currentNode;

					if(!openList.Contains(neighbor)){
						openList.Add (neighbor);
					}
				}
			}
		}
	}

	public void RetracePath (Node startNode, Node endNode){
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;
		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parentNode;		
		}
		path.Reverse();

		//grid.path = path;
	}

	int GetDistance(Node node1, Node node2){
		int distX = Mathf.Abs (node1.gridX - node2.gridX);
		int distY = Mathf.Abs (node1.gridY - node2.gridY);
		if (distX > distY) {
			return 14 * distY + 10 *(distX - distY);
		}
		return 14 * distX + 10 *(distY - distX);
	}
}
