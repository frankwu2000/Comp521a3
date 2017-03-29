using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SilverControl : MonoBehaviour {
	public Grid gridClass;
	public SilverPathfinding pathFingAlg;
	[SerializeField]
	public List<GameObject> agents;
	public Dictionary<int,List<Node>> reservationTable;
	public int table_size;

	public Transform ag1;
	public Transform ag2;
	public Transform tg1;
	public Transform tg2;

	public List<Node> path1;
	public List<Node> path2;

	void Start () {
		pathFingAlg= new SilverPathfinding();
		gridClass = new Grid ();
		gridClass.CreateGrid ();
//		gridClass = gameObject.GetComponent<Grid>();
//		Debug.Log (gridClass.grid.ToString());
		reservationTable = new Dictionary<int, List<Node>>();
		table_size = 10;

		//load up reservation table
		for(int i = 0 ; i < table_size ; i++){
			List<Node> temp_list = new List<Node>();
			foreach(Node n in gridClass.grid){
				temp_list.Add (n);
			}
			reservationTable.Add(i,temp_list);
		}

		//agents = new List<GameObject.Transform>();
		//agents.Add (ag1);
		//agents.Add (ag2);


	}
	
	void Update () {
		Node LastNode1 = pathFingAlg.SilverPathAssess(gridClass,ag1.position,tg1.position);
		path1 = pathFingAlg.RetracePath(gridClass,ag1.position,LastNode1.WorldPosition);

		Node LastNode2 = pathFingAlg.SilverPathAssess(gridClass,ag2.position,tg2.position);
		path2 = pathFingAlg.RetracePath(gridClass,ag2.position,LastNode2.WorldPosition);

	}

	//gizmos for dubugging
	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(Vector3.zero,new Vector3(gridClass.GridWorldSize.x,0,gridClass.GridWorldSize.y));

		if(gridClass.grid != null) {
			foreach(Node node in gridClass.grid){
				Gizmos.color = (node.Walkable)?Color.cyan:Color.gray;
					if (path1.Contains (node)) {
						Gizmos.color = Color.blue;
					}
					if (path2.Contains (node)) {
					Gizmos.color = Color.red;
					}

				Gizmos.DrawCube (node.WorldPosition,new Vector3(1,2,1) * (gridClass.NodeRadius*2));
			}

		}
	}


}
