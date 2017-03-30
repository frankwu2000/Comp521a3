using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SilverControl : MonoBehaviour {
	public Grid gridClass;
	public SilverPathfinding pathFindAlg;
	[SerializeField]
	public List<GameObject> agents;
	public Dictionary<int,List<Node>> reservationTable;
	public int tableSize;

	public Transform ag1;
	public Transform ag2;
	public Transform tg1;
	public Transform tg2;

	public List<Node> path1;
	public List<Node> path2;

	public List<GameObject> agents;

	ReservationTable rT;

	void Start () {
		tableSize = 5;

		pathFindAlg= new SilverPathfinding();

		gridClass = new Grid ();
		gridClass.CreateGrid ();
		rT = new ReservationTable(gridClass,tableSize);


		rT.FirstSet();
//
//		Node LastNode1 = pathFindAlg.SilverPathAssess(gridClass,ag1.position,tg1.position);
//		List<Node> Optimalpath1 = pathFindAlg.RetracePath(gridClass,ag1.position,LastNode1.WorldPosition);
//		path1 = pathFindAlg.PathFinding (Optimalpath1, rT, tableSize, gridClass, ag1.position,tg1.position);
//
//		Node LastNode2 = pathFindAlg.SilverPathAssess(gridClass,ag2.position,tg2.position);
//		List<Node> Optimalpath2 = pathFindAlg.RetracePath(gridClass,ag2.position,LastNode2.WorldPosition);
//		path2 = pathFindAlg.PathFinding (Optimalpath2, rT , tableSize ,gridClass,ag2.position,tg1.position);
//
		agents = new List<GameObject>();


	}
	
	void Update () {
		rT.Reset ();

		Node LastNode1 = pathFindAlg.SilverPathAssess(gridClass,ag1.position,tg1.position);
		List<Node> Optimalpath1 = pathFindAlg.RetracePath(gridClass,ag1.position,LastNode1.WorldPosition);
		path1 = pathFindAlg.PathFinding (Optimalpath1, rT, tableSize, gridClass, ag1.position,tg1.position);

		Node LastNode2 = pathFindAlg.SilverPathAssess(gridClass,ag2.position,tg2.position);
		List<Node> Optimalpath2 = pathFindAlg.RetracePath(gridClass,ag2.position,LastNode2.WorldPosition);
		path2 = pathFindAlg.PathFinding (Optimalpath2, rT , tableSize ,gridClass,ag2.position,tg1.position);


//		Node LastNode1 = pathFindAlg.SilverPathAssess(gridClass,ag1.position,tg1.position);
//		path1 = pathFindAlg.RetracePath(gridClass,ag1.position,LastNode1.WorldPosition);
//
//		Node LastNode2 = pathFindAlg.SilverPathAssess(gridClass,ag2.position,tg2.position);
//		path2 = pathFindAlg.RetracePath(gridClass,ag2.position,LastNode2.WorldPosition);

	}

	//gizmos for dubugging
	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(Vector3.zero,new Vector3(gridClass.GridWorldSize.x,0,gridClass.GridWorldSize.y));

		if(gridClass.grid != null) {
			foreach(Node node in gridClass.grid){
				Gizmos.color = (node.Walkable)?Color.cyan:Color.gray;
					
				if(path1.Contains (node) && path2.Contains(node)){
					Gizmos.color = Color.black;
					}
				if (path1.Contains (node) && !path2.Contains(node)) {
						Gizmos.color = Color.blue;
					}
				if (path2.Contains (node)&&!path1.Contains(node)) {
					Gizmos.color = Color.red;
					}
//				for (int i = 0; i < tableSize; i++) {
//					if(rT.CheckReserve(i,node)){
//						Gizmos.color = Color.black;
//					}
//				}



				//Gizmos.DrawCube (node.WorldPosition,new Vector3(1,2,1) * (gridClass.NodeRadius*2));
			}

		}
	}


}
