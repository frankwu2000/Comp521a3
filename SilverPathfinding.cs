using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverPathfinding {
	//public Transform seeker, target;
	//public Dictionary<int,List<Node>> reservationTable;
//	public Node lastNode;
	//public List<Node> tempPath;

	public Node SilverPathAssess(Grid gridClass, Vector3 startPosition, Vector3 endPosition){


		Node startNode = gridClass.NodeFromWorldPoint (startPosition);
		Node endNode = gridClass.NodeFromWorldPoint (endPosition);

		List<Node> closedList = new List<Node> ();
		List<Node> openList = new List<Node> ();

		//add startnode to openSet
		openList.Add (startNode);
		Node currentNode = openList [0];
			while(openList.Count > 0 ) {
			currentNode = openList [0];
			for (int i = 1; i < openList.Count; i++) {
				if (openList [i].fCost < currentNode.fCost || (openList [i].fCost == currentNode.fCost && openList [i].hCost < currentNode.hCost)) {
					currentNode = openList [i]; 
				}
			}
			openList.Remove (currentNode);
			closedList.Add (currentNode);

			if (currentNode == endNode) {
				return endNode;
			}

			foreach (Node neighbor in gridClass.GetNeighbors(currentNode)) {
				if(!neighbor.Walkable || closedList.Contains(neighbor)){
					continue;
				}

				int newMovementCost = currentNode.gCost + GetDistance (currentNode, neighbor);
				if (newMovementCost < neighbor.gCost || !openList.Contains (neighbor)) {
					neighbor.gCost = newMovementCost;
					neighbor.hCost = GetDistance (neighbor, endNode);
					neighbor.parentNode = currentNode;

					if (!openList.Contains (neighbor)) {
						openList.Add (neighbor);

					}

				}
			}
		}

		return currentNode;
	}

	public List<Node> RetracePath(Grid gridClass, Vector3 startPosition, Vector3 endPosition){

		Node startNode = gridClass.NodeFromWorldPoint (startPosition);
		Node endNode = gridClass.NodeFromWorldPoint (endPosition);

		List<Node> path = new List<Node>();

		Node currentNode = endNode;
		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parentNode;		
		}
		path.Reverse();
		foreach (Node n in gridClass.grid) {
			n.parentNode = null;
		}
		return path;
	}

	int GetDistance(Node node1, Node node2){
		int distX = Mathf.Abs (node1.gridX - node2.gridX);
		int distY = Mathf.Abs (node1.gridY - node2.gridY);
		if (distX > distY) {
			return 14 * distY + 10 *(distX - distY);
		}
		return 14 * distX + 10 *(distY - distX);
	}

//	bool CheckListWalkable(Dictionary <int,List<Node>> reserT,int time,Node neighbor){
//		
//		List<Node> templist = reserT[time];
//		foreach (Node n in templist) {
//			if (n == neighbor) {
//				return n.Walkable;
//			}
//		}
//		Debug.Log ("should not reach here!");
//		return false;
//	}
//


}
