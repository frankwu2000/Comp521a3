using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SilverControl : MonoBehaviour {
	public bool debug_mode;
	public Grid gridClass;
	public SilverPathfinding pathFindAlg;
	ReservationTable rT;
	[SerializeField]
	public int tableSize;
	public List<Agent> agents;
	public int currentMove;

	public Dictionary<Transform,Vector3> children;


	void Start () {
		//set up
		//tableSize = 20;
		gridClass = new Grid ();
		gridClass.CreateGrid ();
		pathFindAlg= new SilverPathfinding(gridClass,tableSize);
		rT = new ReservationTable(gridClass,tableSize);
		rT.FirstSet();
		agents = new List<Agent>();
		children = new Dictionary<Transform, Vector3> ();
		foreach (Transform child in transform)
		{
			Agent temp = new Agent (child);
			temp.SetTarget (child.GetComponent<StudentBehaviour>().targetPostion);
			agents.Add (temp);
		}

	}
		

	void Update () {
		for (int i = 0; i < agents.Count; i++) {
			if (agents [i].target != transform.GetChild (i).GetComponent<StudentBehaviour> ().targetPostion) {
				agents [i].SetTarget (transform.GetChild (i).GetComponent<StudentBehaviour> ().targetPostion);
			}
		}

		currentMove = (int)Time.time % tableSize;
		Step ();


	}

	public void Step(){
		if (currentMove == 0) {
			UpdatePath ();
		} else if(agents.Count>0){
			//move one step
			foreach (Agent a in agents) {
				if (!a.ReachTarget ()) {
					if (currentMove < a.path.Count) {
						Rigidbody rb = a.student.GetComponent<Rigidbody> ();
						rb.MovePosition (a.path [currentMove].WorldPosition);
						//	a.student.position = a.path [currentMove].WorldPosition;
					}
				} 
			}
		}
	}

	public void UpdatePath(){
		
		rT.Reset ();
		foreach (Agent a in agents) {
			a.SetPath (pathFindAlg.AllTogetherPathfind(rT,a.student.position,a.target));
		}
	}


	//gizmos for dubugging

	void OnDrawGizmos(){
		if(debug_mode){
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube(Vector3.zero,new Vector3(gridClass.GridWorldSize.x,0,gridClass.GridWorldSize.y));

			if(gridClass.grid != null) {
				foreach(Node node in gridClass.grid){
					Gizmos.color = (node.Walkable)?Color.cyan:Color.gray;

					foreach(Agent a in agents){
						if(a.path.Contains(node)){
							Gizmos.color = Color.red;
						}
					}

					Gizmos.DrawWireCube (node.WorldPosition,new Vector3(0.2f,0.2f,0.2f));
				}

			}
		}
	}


}
