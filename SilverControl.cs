using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SilverControl : MonoBehaviour {
	public Grid gridClass;
	public SilverPathfinding pathFindAlg;
	ReservationTable rT;
	[SerializeField]
	public int tableSize;
	public List<Agent> agents;
	public int currentMove;


	public GameObject g1;
	public GameObject g2;
	public GameObject t1;
	public GameObject t2;

	void Start () {
		//set up
	//	tableSize = 10;
		gridClass = new Grid ();
		gridClass.CreateGrid ();
		pathFindAlg= new SilverPathfinding(gridClass,tableSize);
		rT = new ReservationTable(gridClass,tableSize);
		rT.FirstSet();
		agents = new List<Agent>();

		Agent a1 = new Agent (g1, pathFindAlg);
		Agent a2 = new Agent (g2, pathFindAlg);
		a1.SetTarget (t1);
		a2.SetTarget (t2);

		agents.Add (a1);
		agents.Add (a2);

	}

	void Update () {
		currentMove = (int)Time.time % tableSize;
		Step ();

	}

	public void AddAgent(Agent a1){
		agents.Add (a1);
	}

	public void Step(){
		if (currentMove == 0) {
			UpdatePath ();
		} else {
			//move one step
			foreach (Agent a in agents) {
				if (!a.ReachTarget()) {
					if(currentMove<a.path.Count){
						a.student.transform.position = a.path [currentMove].WorldPosition;
					}
				}
			}
		}
	}

	public void UpdatePath(){
		
		rT.Reset ();
		foreach (Agent a in agents) {
			a.UpdatePosition ();
			a.SetPath (pathFindAlg.AllTogetherPathfind(rT,a.currentPosition,a.target));
		}
	}


	//gizmos for dubugging
//	void OnDrawGizmos(){
//		Gizmos.color = Color.cyan;
//		Gizmos.DrawWireCube(Vector3.zero,new Vector3(gridClass.GridWorldSize.x,0,gridClass.GridWorldSize.y));
//
//		if(gridClass.grid != null) {
//			foreach(Node node in gridClass.grid){
//				Gizmos.color = (node.Walkable)?Color.cyan:Color.gray;
//					
//				if(agents[0].path.Contains(node) || agents[1].path.Contains(node)){
//					Gizmos.color = Color.red;
//				}
//
//				Gizmos.DrawWireCube (node.WorldPosition,new Vector3(1,1,1) * (gridClass.NodeRadius*2));
//			}
//
//		}
//	}


}
