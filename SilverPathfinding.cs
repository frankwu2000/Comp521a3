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

//---------------------pathfinding----------------------//

	public List<Node> PathFinding(List<Node> optimalPath, ReservationTable resT,int tableSize, Grid gridClass, Vector3 StartPosition,Vector3 EndPosition){

		Node currentNode = gridClass.NodeFromWorldPoint (StartPosition);
		Node endNode = gridClass.NodeFromWorldPoint (EndPosition);
		currentNode.hCost = GetDistance (currentNode,endNode);

		List<Node> realPath = new List<Node>();
		for (int time = 0; time < tableSize; time++) {
			if (time > optimalPath.Count - 1) {
				break;
			}
			//to solve head to head 
			resT.Reserve(time,currentNode);
			//-------
			List<Node> notOptimal = new List<Node> ();
			foreach(Node neighbour in gridClass.GetNeighbors(currentNode)){
				neighbour.hCost = GetDistance (neighbour,endNode);

				if (!resT.CheckReserve (time, neighbour) && optimalPath [time]== (neighbour)) {
					currentNode = neighbour;
					notOptimal = new List<Node> ();
					break;
				} else if (!resT.CheckReserve (time, neighbour)) {
					
					notOptimal.Add (neighbour);
				}
			}
			if (notOptimal.Count > 0) {
				
				Node subOptimal = BestNodeFromList (notOptimal,gridClass);
				if(currentNode.hCost > subOptimal.hCost){
					currentNode = subOptimal;
				}
			}
			realPath.Add (currentNode);
			//reserve
			resT.Reserve(time,currentNode);
		}
		return realPath;
//		return optimalPath;
	}


	Node BestNodeFromList(List<Node> ls, Grid gridClass){
		Node best = ls [0];
		foreach (Node n in ls) {
			if(n.hCost < best.hCost){
				best = n;
			}
		}
		return best;
	}



}
